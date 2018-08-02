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
    public class TenantsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TenantsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Tenants
        [HttpGet]
        public IEnumerable<Tenant> GetTenants() //Get all Tenants from db
        {
            return _context.Tenants;
        }

        // GET: api/Tenants/Building/{Building_Id}
        [HttpGet("Building_Id")]
        [Route("Building/{Building_Id}")]
        public IEnumerable<Tenant> GetTenantsByBuildingId([FromRoute] int Building_Id) //Get all Tenants from specific Building(Id)
        {
            return _context.Tenants.Where(t => t.Building_Id == Building_Id).ToList();
        }

        // GET: api/Tenants/Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenant([FromRoute] int id) //Get Tenant with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
            {
                return NotFound();
            }

            return Ok(tenant);
        }

        // PUT: api/Tenants/Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenant([FromRoute] int id, [FromBody] TenantModel tenantModel) //Update Tenant with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ten = await _context.Tenants.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (ten==null)
            {
                return BadRequest();
            }
            
            PropertiesComparison.CompareAndForward(ten, tenantModel);

            _context.Entry(ten).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                BuildingTenantNumber b = new BuildingTenantNumber(_context);

                await b.CountAndUpdateAsync(tenantModel.Building_Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(id))
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
        

        //POST, DELETE, and change Pass for Tenant in RegisterController

        /*
        // POST: api/Tenants
        [HttpPost]
        public async Task<IActionResult> PostTenant([FromBody] Tenant tenant) //Create a new Tenant
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTenant", new { id = tenant.Id }, tenant);
        }
        

        // DELETE: api/Tenants/Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant([FromRoute] int id) //Delete Tenant with specific Id, and his account in AspNetUsers table
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenant = await _context.Tenants.FindAsync(id);
            var tenantAcc = await _context.Accounts.Where(i => i.UserName == tenant.UserName).FirstOrDefaultAsync();
            if (tenant == null)
            {
                return NotFound();
            }

            _context.Tenants.Remove(tenant);
            _context.Accounts.Remove(tenantAcc);
            await _context.SaveChangesAsync();

            return Ok(tenant);
        }
        */

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }
    }
}