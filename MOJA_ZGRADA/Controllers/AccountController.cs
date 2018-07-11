using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;
using MOJA_ZGRADA.Model;

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
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [Route("Create/Admin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminModel adminModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Creating new AspNetUser entity (IdentityUser)
            var user = new Account
            {
                First_Name = adminModel.First_Name,
                Last_Name = adminModel.Last_Name,
                UserName = adminModel.UserName,
                PhoneNumber = adminModel.PhoneNumber,
                Email = adminModel.Email
            };

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
                    Date_Of_Birth = adminModel.Date_Of_Birth,
                    PhoneNumber = adminModel.PhoneNumber,
                    JMBG = adminModel.JMBG,
                    Address = adminModel.Address
                };
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

        //Only for incode calling [CreateAdmin]
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