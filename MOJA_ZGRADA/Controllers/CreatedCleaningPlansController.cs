using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;

namespace MOJA_ZGRADA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatedCleaningPlansController : ControllerBase //*************IN DEV************** (change default key usage to composite key)
    {
        private readonly MyDbContext _context;

        public CreatedCleaningPlansController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/CreatedCleaningPlans
        [HttpGet]
        public IEnumerable<Created_Cleaning_Plan> GetCreated_Cleaning_Plans() //Get all Created_Cleaning_Plans from db
        {
            return _context.Created_Cleaning_Plans;
        }

        // GET: api/CreatedCleaningPlans/CleaningPlan/{CleaningPlan_Id}
        [HttpGet("{CleaningPlan_Id}")]
        [Route("CleaningPlan/{CleaningPlan_Id}")]
        public async Task<IActionResult> GetCreated_Cleaning_Plan_by_CleaningPlan_Id([FromRoute] int CleaningPlan_Id) //Get Created_Cleaning_Plan with specific CleaningPlan_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created_Cleaning_Plans = await _context.Created_Cleaning_Plans.Where(id => id.Cleaning_Plan_Id == CleaningPlan_Id).ToListAsync();

            if (created_Cleaning_Plans == null)
            {
                return NotFound();
            }

            return Ok(created_Cleaning_Plans);
        }

        // GET: api/CreatedCleaningPlans/Tenant/{Tenant_Id}
        [HttpGet("{Tenant_Id}")]
        [Route("Tenant/{Tenant_Id}")]
        public async Task<IActionResult> GetCreated_Cleaning_Plan_by_Tenant_Id([FromRoute] int Tenant_Id) //Get Created_Cleaning_Plan with specific Tenant_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created_Cleaning_Plans = await _context.Created_Cleaning_Plans.Where(id => id.Tenant_Id == Tenant_Id).ToListAsync();

            if (created_Cleaning_Plans == null)
            {
                return NotFound();
            }

            return Ok(created_Cleaning_Plans);
        }

        // GET: api/CreatedCleaningPlans/Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreated_Cleaning_Plan([FromRoute] int id) //***Get Created_Cleaning_Plan with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created_Cleaning_Plan = await _context.Created_Cleaning_Plans.FindAsync(id);

            if (created_Cleaning_Plan == null)
            {
                return NotFound();
            }

            return Ok(created_Cleaning_Plan);
        }

        // PUT: api/CreatedCleaningPlans/Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreated_Cleaning_Plan([FromRoute] int id, [FromBody] Created_Cleaning_Plan created_Cleaning_Plan) //***Update Created_Cleaning_Plan with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != created_Cleaning_Plan.Cleaning_Plan_Id)
            {
                return BadRequest();
            }

            _context.Entry(created_Cleaning_Plan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Created_Cleaning_PlanExists(id))
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

        // POST: api/CreatedCleaningPlans
        [HttpPost]
        public async Task<IActionResult> PostCreated_Cleaning_Plan([FromBody] Created_Cleaning_Plan created_Cleaning_Plan) //***Create a new Created_Cleaning_Plan
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Created_Cleaning_Plans.Add(created_Cleaning_Plan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Created_Cleaning_PlanExists(created_Cleaning_Plan.Cleaning_Plan_Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCreated_Cleaning_Plan", new { id = created_Cleaning_Plan.Cleaning_Plan_Id }, created_Cleaning_Plan);
        }

        // DELETE: api/CreatedCleaningPlans/Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreated_Cleaning_Plan([FromRoute] int id) //***Delete Created_Cleaning_Plan with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created_Cleaning_Plan = await _context.Created_Cleaning_Plans.FindAsync(id);
            if (created_Cleaning_Plan == null)
            {
                return NotFound();
            }

            _context.Created_Cleaning_Plans.Remove(created_Cleaning_Plan);
            await _context.SaveChangesAsync();

            return Ok(created_Cleaning_Plan);
        }



        //***
        private bool Created_Cleaning_PlanExists(int id)
        {
            return _context.Created_Cleaning_Plans.Any(e => e.Cleaning_Plan_Id == id);
        }
    }
}