using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;
using MOJA_ZGRADA.Model;
using MOJA_ZGRADA.Static;

namespace MOJA_ZGRADA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MessagesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public IEnumerable<Message> GetMessages() //Get all Admin Messages from db
        {
            return _context.Messages;
        }

        // GET: api/Messages/Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute] int id) //Get Admin Message with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // GET: api/Messages/Tenant/{Tenant_Id}
        [HttpGet("{Tenant_Id}")]
        [Route("Tenant/{Tenant_Id}")]
        public async Task<IActionResult> GetMessageByTenantId([FromRoute] int Tenant_Id) //Get Admin Messages by specific Tenant_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.Where(t => t.Tenant_Id == Tenant_Id).ToListAsync();

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }
        
        // GET: api/Messages/Admin/{Admin_Id}
        [HttpGet("{Admin_Id}")]
        [Route("Admin/{Admin_Id}")]
        public async Task<IActionResult> GetMessageByAdminId([FromRoute] int Admin_Id) //Get Admin Messages with specific Admin_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.Where(t => t.Admin_Id == Admin_Id).ToListAsync();

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }
        
        /*
        // PUT: api/Messages/Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage([FromRoute] int id, [FromBody] Message message) //Update Message with specific Id (leave or remove???)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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
        */

        // POST: api/Messages
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] MessageModel messageModel) //Create a new Message issued by Admin
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = new Message();
            var messageTenant = new MessageTenant();

            PropertiesComparison.CompareAndForward(message, messageModel);
            PropertiesComparison.CompareAndForward(messageTenant, messageModel);

            message.Message_Issued_DateTime = DateTime.Now;
            messageTenant.Message_Issued_DateTime = DateTime.Now;

            try
            {
                _context.Messages.Add(message);
                _context.MessageTenants.Add(messageTenant);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetMessage", new { id = message.Id }, message);
                CreatedAtAction("GetMessageTenant", new { id = messageTenant.Id }, message);
                
                return Ok(message);
            }
            catch(Exception ex)
            {
                return NotFound(new { ex });
            }

        }

        // DELETE: api/Messages/Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id) //Delete Admin Message with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }





        //Only for incode calling
        public async Task<IActionResult> GetMessageTenant([FromRoute] int id) //Get MessageTenant with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.MessageTenants.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}