using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;
using MOJA_ZGRADA.Model;
using System.Web.Http;
using System.Reflection;
using MOJA_ZGRADA.Static;

namespace MOJA_ZGRADA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _context;
        private UserManager<Account> _userManager;
        private RoleManager<MyRoleManager> _roleManager;
        
        public AccountController(UserManager<Account> userManager, MyDbContext context, RoleManager<MyRoleManager> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        //REDOSLED JE BITAN : Http -> Authorize -> Route

        //POST: api/Account/Admin/Create
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Admin/Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminCreate([FromBody] AdminModel adminModel)  //Admin Registration
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Account
            {
                First_Name = adminModel.First_Name,
                Last_Name = adminModel.Last_Name,
                UserName = adminModel.UserName,
                PhoneNumber = adminModel.PhoneNumber,
                Email = adminModel.Email
            };

            try
            {
                //Creating new AspNetUser entity (IdentityUser)
                IdentityResult result = await _userManager.CreateAsync(user, adminModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                    //Creating new tbl_Admin entity
                    var adm = new Admin
                    {
                        First_Name = adminModel.First_Name,
                        Last_Name = adminModel.Last_Name,
                        Email = adminModel.Email,
                        PhoneNumber = adminModel.PhoneNumber,
                        UserName = adminModel.UserName
                    };

                    //adding new Admin entity
                    CreatedAtAction("GetAdmin", new { id = adminModel.UserName }, adm);
                    _context.Admins.Add(adm);
                    await _context.SaveChangesAsync();

                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (SqlException ex)
            {
                return NotFound(ex);
            }
        }

        //PUT: api/Account/Admin/Update/Personal
        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Admin/Update/Personal")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminUpdatePersonal([FromQuery] int id, [FromBody] AdminAccountModel adminModel)    //Update Admin's Personal Info (focus on tbl_Admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //Update tbl_Admin
            var admId = _context.Admins.Where(t => t.Id == id).Select(i => i.Id).FirstOrDefault();
            var adm = _context.Admins.Find(admId);

            if(adm==null)
            {
                return NotFound(id);
            }

            var oldUser = adm.UserName;

            PropertiesComparison.CompareAndForward(adm, adminModel);
            //adm.First_Name = adminModel.First_Name;
            //adm.Last_Name = adminModel.Last_Name;
            //adm.Email = adminModel.Email;
            //adm.Date_Of_Birth = adminModel.Date_Of_Birth;
            //adm.PhoneNumber = adminModel.PhoneNumber;
            //adm.JMBG = adminModel.JMBG;
            //adm.Address = adminModel.Address;


            //Update AspNetUsers
            Account u = _userManager.FindByNameAsync(oldUser).Result;


            PropertiesComparison.CompareAndForward(u, adminModel);
            //u.First_Name = adminModel.First_Name;
            //u.Last_Name = adminModel.Last_Name;
            //u.Email = adminModel.Email;



            //_userManager.ErrorDescriber.DuplicateEmail

            try
            {
                //Execute updates
                var result = await _userManager.UpdateAsync(u);
                if (!result.Succeeded)
                {
                    return NotFound(result);
                }
                _context.Entry(adm).State = EntityState.Modified;
                _context.SaveChanges();
                
                return Ok(adm);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(ex);
            }
        }
        
        //PUT: api/Account/Admin/Update/Account
        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Admin/Update/Account")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminUpdateAccount([FromQuery] int id, [FromBody] AdminPersonalModel adminModel)    //Update Admin's account info (Usr and Pass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Update tbl_Admin
            var admId = _context.Admins.Where(t => t.Id == id).Select(i => i.Id).FirstOrDefault();
            var adm = _context.Admins.Find(admId);
            
            if (adm == null)
            {
                return NotFound(id);
            }

            var oldUser = adm.UserName;

            PropertiesComparison.CompareAndForward(adm, adminModel);
            //adm.UserName = adminModel.UserName;


            //Update AspNetUsers
            Account u = _userManager.FindByNameAsync(oldUser).Result;
            PropertiesComparison.CompareAndForward(u, adminModel);
            //u.UserName = adminModel.UserName;

            if (u == null)
            {
                return NotFound(adminModel);
            }
            u.PasswordHash = _userManager.PasswordHasher.HashPassword(u, adminModel.Password);
            
            try
            {
                //Execute updates
                var result = await _userManager.UpdateAsync(u);
                if (!result.Succeeded)
                {
                    return NotFound(result);
                }
                _context.Entry(adm).State = EntityState.Modified;
                _context.SaveChanges();


                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(ex);
            }
        }
        
        //DELETE: api/Account/Admin/Delete
        [HttpDelete]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Admin/Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDelete([FromQuery]int id)  //Delete Admin with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Admin = _context.Admins.Find(id);

            if(Admin==null)
            {
                return NotFound(id);
            }

            var Account = await _userManager.FindByNameAsync(Admin.UserName);
            
            try
            {
                _context.Admins.Remove(Admin);
                var result = await _userManager.DeleteAsync(Account);
                _context.SaveChanges();
                
                return Ok();

            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }

        }

        //GET: api/Account/Admin/All
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Admin/All")]
        public List<Admin> AdminAll()  //Return list of all admins from tbl_admin   
        {
            var AllAdmins =  _context.Admins.ToList();
            
            return AllAdmins;
        }

        //GET: api/Account/Admin/Get
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Admin/Get")]
        public Admin AdminGet([FromQuery] int Id)   //Return Admin with specific Id
        {
            var Admin = _context.Admins.Find(Id);
            
            return Admin;
        }


        //Only for incode calling [AdminCreate]
        public async Task<IActionResult> GetAdmin([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Admin = await _context.Admins.FindAsync(id);

            if (Admin == null)
            {
                return NotFound();
            }

            return Ok(Admin);
        }
    }
}