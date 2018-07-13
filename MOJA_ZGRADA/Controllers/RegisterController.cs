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
    public class RegisterController : ControllerBase    //For Admin's initial registration of building+tenants
    {
        private readonly MyDbContext _context;
        private UserManager<Account> _userManager;
        private RoleManager<MyRoleManager> _roleManager;

        public RegisterController(UserManager<Account> userManager, MyDbContext context, RoleManager<MyRoleManager> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: api/Register
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("PostAsync")]
        public async Task<IActionResult> PostAsync([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var building = new Building
            {
                Nickname = registerModel.Nickname,
                Date_Of_Creation = registerModel.Date_Of_Creation,
                Address = registerModel.Address,
                Number_Of_Apartments = registerModel.Number_Of_Apartments,
                Number_Of_Tenants = registerModel.Number_Of_Tenants,
                Number_Of_Parking_Places = registerModel.Number_Of_Parking_Places,
                Number_Of_Basements = registerModel.Number_Of_Basements,
                Number_Of_Entrances = registerModel.Number_Of_Entrances,
                Number_Of_Floors = registerModel.Number_Of_Floors,
                Special_Apartments_Annotation = registerModel.Special_Apartments_Annotation
            };



            try
            {
                for (int i = 1; i <= registerModel.Number_Of_Tenants; i++)
                {
                    var meta_User_Name = registerModel.Address + "Stan" + i;

                    var Array = meta_User_Name.Split(" ");

                    var User_Name = string.Join("", Array);

                    var Password = User_Name + "#";

                    var tenant = new Tenant
                    {
                        UserName = User_Name
                    };

                    CreatedAtAction("GetTenant", new { id = tenant.UserName }, tenant);
                    _context.Tenants.Add(tenant);

                    var user = new Account
                    {
                        UserName = User_Name,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    IdentityResult result = await _userManager.CreateAsync(user, Password);

                    await _userManager.AddToRoleAsync(user, "Tenant");
                }

                CreatedAtAction("GetBuilding", new { id = registerModel.Address }, building);
                _context.Buildings.Add(building);
                await _context.SaveChangesAsync();
                


                return Ok(building);

            }
            catch (Exception ex)
            {
                return NotFound(new { ex });
            }

        }

        // PUT: api/Register/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }










        //Only for incode calling [PostAsync]
        public async Task<IActionResult> GetBuilding([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Building = await _context.Buildings.FindAsync(id);

            if (Building == null)
            {
                return NotFound();
            }

            return Ok(Building);
        }

        //Only for incode calling [PostAsync]
        public async Task<IActionResult> GetTenant([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Tenant = await _context.Tenants.FindAsync(id);

            if (Tenant == null)
            {
                return NotFound();
            }

            return Ok(Tenant);
        }

    }
}

