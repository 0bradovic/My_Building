using Microsoft.EntityFrameworkCore;
using MOJA_ZGRADA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Static
{
    public class BuildingTenantNumber
    {
        private readonly MyDbContext _context;

        public BuildingTenantNumber(MyDbContext context)
        {
            _context = context;
        }

        public async Task CountAndUpdateAsync(int Building_Id) //Update building number of tenants by counting number of occupants for each tenant profile
        {
            int BuildingTenantNumber = 0;

            foreach (var Tenant in await _context.Tenants.Where(b => b.Building_Id == Building_Id).ToListAsync()) 
            {
                BuildingTenantNumber += Tenant.Number_Of_Occupants;
            }

            var Building = await _context.Buildings.Where(i => i.Id == Building_Id).FirstOrDefaultAsync();

            Building.Number_Of_Tenants = BuildingTenantNumber;

            _context.Entry(Building).State = EntityState.Modified;
                
            await _context.SaveChangesAsync();
        }
    }
}
