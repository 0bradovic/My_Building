using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;

namespace MOJA_ZGRADA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BuildingsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IEnumerable<Building> GetBuildings() //Return List of all buildings
        {
            return _context.Buildings;
        }

        // GET: api/Buildings/Id
        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> GetBuilding([FromRoute] int id) //Return building with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var building = await _context.Buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return Ok(building);
        }

        // PUT: api/Buildings/Id
        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> PutBuilding([FromRoute] int id, [FromBody] Building building) //Update building with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != building.Id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> PostBuilding([FromBody] Building building) //Insert a new building
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if building with inserted address already exist
            var check = _context.Buildings.Where(chk => chk.Address == building.Address).SingleOrDefault();
            if (check != null)
            {
                var error = $"Building with address " + building.Address + " already exist";
                return NotFound(new { error });
            }

            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.Id }, building);
        }

        // DELETE: api/Buildings/Id
        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> DeleteBuilding([FromRoute] int id) //Delete building with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();

            return Ok(building);
        }

        // POST: api/Buildings/Assign/Building_Id
        [HttpPost("{buildingId}")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        [Route("Assign/{buildingId}")]
        public async Task<IActionResult> AssignBuilding([FromRoute] int buildingId, [FromBody] int adminId) //Assign admin for building handling
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //New Handles entity
                var handles = new Handles
                {
                    Building_Id = buildingId,
                    Admin_Id = adminId,
                    Admin = GetAdmin(adminId),
                    Building = BuildingById(buildingId)
                };
                _context.Handleses.Add(handles);
                await _context.SaveChangesAsync();

                return Ok(handles);
            }
            catch(Exception ex)
            {
                return NotFound(new { ex });
            }
                
        }

        // GET: api/Buildings/Unassigned
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin")]
        [Route("Unassigned")]
        public IEnumerable<Building> UnassignedBuildings() //Returns buildings with unassigned admin
        {
            var buildings = _context.Buildings.Where(c => !_context.Handleses.Select(b => b.Building_Id).Contains(c.Id));

            return buildings;
        }



        private bool BuildingExists(int id)
        {
            return _context.Buildings.Any(e => e.Id == id);
        }


        //Only for incode calling
        public Building BuildingById(int id)
        {
            var Building =  _context.Buildings.Find(id);
            
            return Building;
        }

        //Only for incode calling
        public Admin GetAdmin(int id)
        {
            var Admin = _context.Admins.Find(id);

            return Admin;

        }
    }
}