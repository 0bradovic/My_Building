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
using Microsoft.EntityFrameworkCore;

namespace MOJA_ZGRADA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase    //For Admin's initial registration of building + tenants
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

        //POST: api/Register/Initial
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Initial")]
        public async Task<IActionResult> Initial([FromBody] RegisterModel registerModel)  //Admin's initial registration of building + tenants *PROBLEM: NE VRACA NISTA U POSTMAN?!*
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
                //check if building with inserted address already exist
                var check = _context.Buildings.Where(chk => chk.Address == registerModel.Address).SingleOrDefault();
                if (check != null)
                {
                    var error = $"Building with address " + registerModel.Address + " already exist";
                    return NotFound(new { error });
                }


                //New Building entity
                CreatedAtAction("GetBuilding", new { id = registerModel.Address }, building);
                _context.Buildings.Add(building);
                await _context.SaveChangesAsync();

                var Building_Id = _context.Buildings.Where(b => b.Address == registerModel.Address).Select(i => i.Id).FirstOrDefault();


                for (int i = 1; i <= registerModel.Number_Of_Apartments; i++)
                {
                    //UserName template: Address+Stan+BrStana
                    //Password template: Address+Stan+BrStana+#
                    var meta_User_Name = registerModel.Address + "Stan" + i;
                    var Array = meta_User_Name.Split(" ");
                    var User_Name = string.Join("", Array);
                    var Password = User_Name + "#";

                    var user = new Account
                    {
                        UserName = User_Name,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    //new tenant account
                    IdentityResult result = await _userManager.CreateAsync(user, Password);

                    if (!result.Succeeded)
                    {
                        return NotFound(result);
                    }

                    var tenant = new Tenant
                    {
                        UserName = User_Name,
                        Apartment_Number = i,
                        Building_Id = Building_Id
                        //Address = registerModel.Address
                    };

                    //new tenant
                    CreatedAtAction("GetTenant", new { id = tenant.UserName }, tenant);

                    _context.Tenants.Add(tenant);

                    await _userManager.AddToRoleAsync(user, "Tenant");
                }

                //New Handles entity
                var handles = new Handles
                {
                    Building_Id = building.Id,
                    Admin_Id = registerModel.Admin_Id,
                    Admin = GetAdmin(registerModel.Admin_Id),
                    Building = building
                };
                _context.Handleses.Add(handles);
                await _context.SaveChangesAsync();

                return Ok(building);  //***NE VRACA NISTA U POSTMAN!?***

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }

        }

        //POST: api/Register/NewTenant
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Tenant/New")]
        public async Task<IActionResult> NewTenant([FromBody] TenantModel tenantModel)  //For adding a new tenant
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var building = _context.Buildings.Where(b => b.Address == tenantModel.Address).Select(i => i.Id).FirstOrDefault();

            if (building == 0)
            {
                return NotFound(tenantModel.Address);
            }

            try
            {
                //get number of last apartment
                int maxApartmentNumber = _context.Buildings.Where(t => t.Address == tenantModel.Address).Select(i => i.Number_Of_Apartments).DefaultIfEmpty(0).Max();

                //UserName template: Address+Stan+BrStana
                //Password template: Address+Stan+BrStana+#
                var meta_User_Name = tenantModel.Address + "Stan" + (maxApartmentNumber + 1).ToString();
                var Array = meta_User_Name.Split(" ");
                var User_Name = string.Join("", Array);
                var Password = User_Name + "#";

                var user = new Account
                {
                    UserName = User_Name,
                    First_Name = tenantModel.First_Name,
                    Last_Name = tenantModel.Last_Name,
                    PhoneNumber = tenantModel.PhoneNumber,
                    Email = tenantModel.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //new tenant account
                IdentityResult result = await _userManager.CreateAsync(user, Password);

                if (!result.Succeeded)
                {
                    return NotFound(result);
                }

                var tenant = new Tenant
                {
                    UserName = User_Name,
                    First_Name = tenantModel.First_Name,
                    Last_Name = tenantModel.Last_Name,
                    Email = tenantModel.Email,

                    Date_Of_Birth = tenantModel.Date_Of_Birth,
                    PhoneNumber = tenantModel.PhoneNumber,
                    JMBG = tenantModel.JMBG,
                    Floor_Number = tenantModel.Floor_Number,
                    Number_Of_Occupants = tenantModel.Number_Of_Occupants,

                    Building_Id = building,

                    Apartment_Number = maxApartmentNumber + 1
                };

                //new tenant
                CreatedAtAction("GetTenant", new { id = tenant.UserName }, tenant);
                _context.Tenants.Add(tenant);
                await _userManager.AddToRoleAsync(user, "Tenant");

                //increase number of apartments for building
                var apartmentIncrement = _context.Buildings.SingleOrDefault(b => b.Id == building);
                if (result != null)
                {
                    apartmentIncrement.Number_Of_Apartments = maxApartmentNumber + 1;
                    _context.SaveChanges();
                }

                return Ok(tenant);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        //POST: api/Register/NewBuilding
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Building/New")]
        public async Task<IActionResult> NewBuilding([FromBody] RegisterModel registerModel)    //For adding a new building *PROBLEM: NE VRACA NISTA U POSTMAN?!*
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
                //check if building with inserted address already exist
                var check = _context.Buildings.Where(chk => chk.Address == registerModel.Address).SingleOrDefault();
                if (check != null)
                {
                    var error = $"Building with address " + registerModel.Address + " already exist";
                    return NotFound(new { error });
                }


                //New Building entity
                CreatedAtAction("GetBuilding", new { id = registerModel.Address }, building);
                _context.Buildings.Add(building);

                //New Handles entity
                var handles = new Handles
                {
                    Building_Id = building.Id,
                    Admin_Id = registerModel.Admin_Id,
                    Admin = GetAdmin(registerModel.Admin_Id),
                    Building = building
                };
                _context.Handleses.Add(handles);
                await _context.SaveChangesAsync();



                return Ok(building); //***NE VRACA NISTA U POSTMAN!?***
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.InnerException.Message });
            }
        }

        //DELETE: api/Register/Tenant/Delete/Id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Route("Tenant/Delete/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> TenantDelete([FromRoute]int id)    //Delete Tenant with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Tenant = _context.Tenants.Find(id);

            if (Tenant == null)
            {
                return NotFound(id);
            }

            var Account = await _userManager.FindByNameAsync(Tenant.UserName);

            try
            {
                _context.Tenants.Remove(Tenant);
                var result = await _userManager.DeleteAsync(Account);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        //DELETE: api/Register/Building/Delete/Id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Route("Building/Delete/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> BuildingDelete([FromRoute]int id)  //Delete Building with specific Id *PREMESTITI U BUILDING CONTROLLER*
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Building = _context.Buildings.Find(id);

            if (Building == null)
            {
                return NotFound(id);
            }
            
            try
            {
                _context.Buildings.Remove(Building);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        //Only for incode calling
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

        //Only for incode calling
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

        //Only for incode calling
        public Admin GetAdmin(int id)
        {
            var Admin =  _context.Admins.Find(id);
            
            return Admin;

        }
    }
}

