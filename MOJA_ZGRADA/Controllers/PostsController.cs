using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;
using MOJA_ZGRADA.Model;
using MOJA_ZGRADA.Static;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace MOJA_ZGRADA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly MyDbContext _context;
        private SafeFileHandle filePath;

        public PostsController(MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> GetPosts() //Get all posts from db
        {
            return _context.Posts;
        }

        // GET: api/Posts/Admin/Admin_Id
        [HttpGet("Admin_Id")]
        [Route("Admin/{Admin_Id}")]
        public IEnumerable<Post> GetPostsByAdminId([FromRoute] int Admin_Id) //Get all posts by specific Admin_Id
        {
            var Posts = _context.Posts.Where(p => p.Admin_Id == Admin_Id).ToList();

            return Posts;
        }

        // GET: api/Posts/Building/Building_Id
        [HttpGet("Building_Id")]
        [Route("Building/{Building_Id}")]
        public IEnumerable<Post> GetPostsByBuildingId([FromRoute] int Building_Id) //Get all posts by specific Building_Id
        {
            var Posts = _context.Posts.Where(p => p.Building_Id == Building_Id).ToList();

            return Posts;
        }
        
        // GET: api/Posts/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id) //Get post with specific id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] int id, [FromBody] PostModel postModel) //Update post with specific id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Post = _context.Posts.Where(p => p.Id == id).FirstOrDefault();

            PropertiesComparison.CompareAndForward(Post, postModel);

            Post.Post_Update_DateTime = DateTime.Now;

            _context.Entry(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] Post post) //Create new post   //***IN DEV***
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.Post_Creation_DateTime = DateTime.Now;

            try
            {
                //Image
                post.Post_Image = Request.Form.Files[0];
                var Image = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                string extension = Path.GetExtension(post.Post_Picture_URL);
                var filePath = Path.Combine(Image, DateTime.Now.ToString("yymmssfff") + extension);
                var fileName = DateTime.Now.ToString("yymmssfff") + extension;

                post.Post_Picture_FileName = fileName;
                post.Post_Picture_URL = filePath;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await post.Post_Image.CopyToAsync(stream);
                }


                //Pdf
                post.Post_PDF = Request.Form.Files[1];
                var Pdf = Path.Combine(_hostingEnvironment.WebRootPath, "PDFs");
                string extension2 = Path.GetExtension(post.Post_PDF_URL);
                var filePath2 = Path.Combine(Pdf, DateTime.Now.ToString("yymmssfff") + extension2);
                var fileName2 = DateTime.Now.ToString("yymmssfff") + extension;

                post.Post_PDF_FileName = fileName2;
                post.Post_PDF_URL = filePath2;

                using (var stream = new FileStream(filePath2, FileMode.Create))
                {
                    await post.Post_PDF.CopyToAsync(stream);
                }

                

                //Update db
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPost", new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return NotFound(new { ex });
            }
            
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id) //Delete post with specific id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }



        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}