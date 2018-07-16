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

namespace MOJA_ZGRADA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _context;
        private UserManager<Account> _userManager;
        private RoleManager<MyRoleManager> _roleManager;

        public ClaimsPrincipal User;

        public AccountController(UserManager<Account> userManager, MyDbContext context, RoleManager<MyRoleManager> roleManager, ClaimsPrincipal _user)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _user = User;
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
                return NotFound(new { ex });
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        [Route("admTabela")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAdminTable([FromRoute] string id, [FromBody] AdminModel adminModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != adminModel.UserName)
            //{
            //    return BadRequest();
            //}




            //Update AspNetUsers
            Account u;//=  _userManager.GetByIdAsync();
            //u.First_Name = adminModel.First_Name;
            //u.Last_Name = adminModel.Last_Name;
            //u.Email = adminModel.Email;
            
            

            //Update tbl_Admin
            var adm = _context.Admins.Find(id);
            adm.First_Name = adminModel.First_Name;
            adm.Last_Name = adminModel.Last_Name;
            adm.Email = adminModel.Email;
            adm.Date_Of_Birth = adminModel.Date_Of_Birth;
            adm.PhoneNumber = adminModel.PhoneNumber;
            adm.JMBG = adminModel.JMBG;
            adm.Address = adminModel.Address;

            try
            {

                //_userManager.UpdateAsync(u);

                _context.Entry(adm).State = EntityState.Modified;
            
                _context.SaveChanges();

                return Ok(adm);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        [Route("accTabela")]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateAccountTable([FromRoute] string id, [FromBody] AdminModel adminModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminModel.UserName)
            {
                return BadRequest();
            }

            var user = new Account
            {
                UserName = adminModel.UserName
            };

            //_userManager.ErrorDescriber.DuplicateEmail

            var adm = new Admin
            {
                UserName = adminModel.UserName
            };

            
            _context.Entry(adm).State = EntityState.Modified;

            _context.Entry(user).State = EntityState.Modified;
            
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
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