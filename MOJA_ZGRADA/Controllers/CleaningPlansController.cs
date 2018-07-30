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
    public class CleaningPlansController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CleaningPlansController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/CleaningPlans
        [HttpGet]
        public IEnumerable<Cleaning_Plan> GetCleaning_Plans() //Get all Cleaning_Plans from db
        {
            return _context.Cleaning_Plans;
        }

        // GET: api/CleaningPlans/Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCleaning_Plan([FromRoute] int id) //Get Cleaning_Plan with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cleaning_Plan = await _context.Cleaning_Plans.FindAsync(id);

            if (cleaning_Plan == null)
            {
                return NotFound();
            }

            return Ok(cleaning_Plan);
        }

        // GET: api/CleaningPlans/Building/{Building_Id}
        [HttpGet("{Building_Id}")]
        [Route("Building/{Building_Id}")]
        public async Task<IActionResult> GetCleaning_Plans_By_BuildingId([FromRoute] int Building_Id) //Get Cleaning_Plans with specific Building_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cleaning_Plans = await _context.Cleaning_Plans.Where(b => b.Building_Id == Building_Id).ToListAsync();

            if (cleaning_Plans == null)
            {
                return NotFound();
            }

            return Ok(cleaning_Plans);
        }

        // PUT: api/CleaningPlans/Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaning_Plan([FromRoute] int id, [FromBody] Cleaning_Plan cleaning_Plan) //Update Cleaning_Plan with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cleaning_Plan.Id)
            {
                return BadRequest();
            }

            _context.Entry(cleaning_Plan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cleaning_PlanExists(id))
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

        // POST: api/CleaningPlans
        [HttpPost]
        public async Task<IActionResult> PostCleaning_Plan([FromBody] Cleaning_Plan cleaning_Plan) //Create a new Cleaning_Plan
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cleaning_Plans.Add(cleaning_Plan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaning_Plan", new { id = cleaning_Plan.Id }, cleaning_Plan);
        }

        // DELETE: api/CleaningPlans/Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCleaning_Plan([FromRoute] int id) //Delete Cleaning_Plan with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cleaning_Plan = await _context.Cleaning_Plans.FindAsync(id);
            if (cleaning_Plan == null)
            {
                return NotFound();
            }

            _context.Cleaning_Plans.Remove(cleaning_Plan);
            await _context.SaveChangesAsync();

            return Ok(cleaning_Plan);
        }




        private bool Cleaning_PlanExists(int id)
        {
            return _context.Cleaning_Plans.Any(e => e.Id == id);
        }
    }
}