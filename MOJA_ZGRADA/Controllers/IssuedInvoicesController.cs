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
    public class IssuedInvoicesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public IssuedInvoicesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/IssuedInvoices/All
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> Get_All_Issued_Invoices() //Get all Issued_Invoices from db
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Selected_Issued_Invoice = await _context.Issued_Invoices
                .Join(_context.Invoices,
                      emp => emp.Invoice_Id,
                      per => per.Id,
                      (emp, per) => new { emp, per })
                .Join(_context.Tenants,
                       o => o.emp.Tenant_Id,
                       sal => sal.Id,
                       (emp1, sal) => new { emp1, sal })
                .ToListAsync();

            if (Selected_Issued_Invoice == null)
            {
                return NotFound();
            }


            return Ok(Selected_Issued_Invoice);
        }

        // GET: api/IssuedInvoices/?tenant_id={tenant_id}&invoice_id={invoice_id}
        [HttpGet]
        public async Task<IActionResult> GetIssued_Invoice(int tenant_id, int invoice_id) //Get Issued_Invoice with specific pair of Tenant_Id + Invoice_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var Selected_Issued_Invoice = await _context.Issued_Invoices.Where(i => (i.Invoice_Id == invoice_id && i.Tenant_Id == tenant_id))
                 .Join(_context.Invoices.Where(i => i.Id == invoice_id),
                       emp => emp.Invoice_Id,
                       per => per.Id,
                       (emp, per) => new { emp, per })
                 .Join(_context.Tenants.Where(t=>t.Id==tenant_id),
                       o => o.emp.Tenant_Id,
                       sal => sal.Id,
                       (emp1, sal) => new { emp1, sal })
                .ToListAsync();

            if (Selected_Issued_Invoice == null)
            {
                return NotFound();
            }

            return Ok(Selected_Issued_Invoice);
        }

        // GET: api/IssuedInvoices/Get/{InvoiceName}
        [HttpGet("Invoice_Name")]
        [Route("Get/{Invoice_Name}")]
        public async Task<IActionResult> GetIssued_Invoice_By_InvoiceName([FromRoute] string Invoice_Name) //Get Issued_Invoice with specific Invoice_Name
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Issued_Invoice> IIlist = new List<Issued_Invoice>();

            foreach (var issuedInvoice in await _context.Issued_Invoices.ToListAsync())
            {
                if (issuedInvoice == null)
                {
                    continue;
                }

                IIlist.Add(issuedInvoice);
            }

            return Ok(IIlist);
        }

        // GET: api/IssuedInvoices/Admin/{Admin_Id}
        [HttpGet("Admin_Id")]
        [Route("Admin/{Admin_Id}")]
        public async Task<IActionResult> GetIssued_Invoice_By_Admin_Id([FromRoute] int Admin_Id) //Get Issued_Invoice with specific Admin_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Issued_Invoice> IIlist = new List<Issued_Invoice>();

            foreach (var issuedInvoice in await _context.Issued_Invoices.Where(c => _context.Invoices.Where(i => i.Admin_Id == Admin_Id).Select(b => b.Id).Contains(c.Invoice_Id)).ToListAsync())
            {
                if (issuedInvoice == null)
                {
                    continue;
                }

                IIlist.Add(issuedInvoice);
            }

            return Ok(IIlist);
        }

        // GET: api/IssuedInvoices/Building/{Building_Id}
        [HttpGet("Building_Id")]
        [Route("Building/{Building_Id}")]
        public async Task<IActionResult> GetIssued_Invoice_By_Building_Id([FromRoute] int Building_Id) //Get Issued_Invoice with specific Building_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Issued_Invoice> IIlist = new List<Issued_Invoice>();

            foreach (var tenant in await _context.Tenants.Where(i=>i.Building_Id==Building_Id).ToListAsync())
            {
                foreach (var issinv in await _context.Issued_Invoices.Where(i => i.Tenant_Id == tenant.Id).ToListAsync())
                {
                    if (issinv == null)
                    {
                        continue;
                    }

                    IIlist.Add(issinv);
                }
            }

            return Ok(IIlist);
        }

        // PUT: api/IssuedInvoices/?tenant_id={tenant_id}&invoice_id={invoice_id}
        [HttpPut]
        public async Task<IActionResult> PutIssued_Invoice(int tenant_id, int invoice_id, [FromBody] IssuedInvoicePutModel issued_Invoice_Model) //Update Issued_Invoice with specific pair of Tenant_Id + Invoice_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var IIexist = await _context.Issued_Invoices.Where(i => (i.Invoice_Id == invoice_id && i.Tenant_Id == tenant_id)).FirstOrDefaultAsync();

            if (IIexist == null)
            {
                var error = $"Issued Invoice with " + invoice_id + " invoice Id, and " + tenant_id + " tenant Id doesn't exist!";
                return NotFound(new { error });
            }

            var IIexistTenant = await _context.IssuedInvoiceTenants.Where(i => (i.Invoice_Id == invoice_id && i.Tenant_Id == tenant_id)).FirstOrDefaultAsync();

            //make new issued invoice for tenant if he deleted
            if (IIexistTenant == null) 
            {
                var Issued_Invoice_Tenant = new IssuedInvoiceTenant();
                PropertiesComparison.CompareAndForward(Issued_Invoice_Tenant, issued_Invoice_Model);
                Issued_Invoice_Tenant.Issued_Invoice_Creation_DateTime = IIexist.Issued_Invoice_Creation_DateTime;
                Invoice inv = await _context.Invoices.Where(i => i.Id == invoice_id).FirstOrDefaultAsync();
                var InvoiceType = await _context.Invoices.Where(i => i.Id == invoice_id).Select(i => i.Invoice_Type).FirstOrDefaultAsync();
                var building = await _context.Buildings.Where(i => i.Id == issued_Invoice_Model.Building_Id).FirstOrDefaultAsync();
                Issued_Invoice_Tenant.Issued_Invoice_Name = IIexist.Issued_Invoice_Name;
                
                Issued_Invoice_Tenant.Invoice_Id = invoice_id;
                Issued_Invoice_Tenant.Tenant_Id = tenant_id;
                IIexist.Issued_Invoice_Amount = IIexist.Issued_Invoice_Amount;
                            
                _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                await _context.SaveChangesAsync();
                            
            }
            

            var iiAdmin = await _context.Issued_Invoices.Where(i => (i.Invoice_Id == invoice_id && i.Tenant_Id == tenant_id)).FirstOrDefaultAsync();


            PropertiesComparison.CompareAndForward(iiAdmin, IIexist);
            PropertiesComparison.CompareAndForward(iiAdmin, issued_Invoice_Model);

            _context.Entry(iiAdmin).State = EntityState.Modified;

            var iiTenant = await _context.IssuedInvoiceTenants.Where(i => (i.Invoice_Id == invoice_id && i.Tenant_Id == tenant_id)).FirstOrDefaultAsync();

            PropertiesComparison.CompareAndForward(iiTenant, IIexist);
            PropertiesComparison.CompareAndForward(iiTenant, issued_Invoice_Model);
            _context.Entry(iiTenant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Issued_InvoiceExists(tenant_id, invoice_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(iiAdmin);
        }

        // POST: api/IssuedInvoices
        [HttpPost]
        public async Task<IActionResult> PostIssued_Invoice_By_Building_Id([FromBody] IssuedInvoiceModel issued_Invoice_model) //Create Issued Invoice for each tenant in specified Building_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Issued_Invoice = new Issued_Invoice();
            var Issued_Invoice_Tenant = new IssuedInvoiceTenant();

            PropertiesComparison.CompareAndForward(Issued_Invoice, issued_Invoice_model);
            PropertiesComparison.CompareAndForward(Issued_Invoice_Tenant, issued_Invoice_model);

            Issued_Invoice.Issued_Invoice_Creation_DateTime = DateTime.Now;
            Issued_Invoice_Tenant.Issued_Invoice_Creation_DateTime = DateTime.Now;

            var InvoiceType = await _context.Invoices.Where(i=>i.Id==issued_Invoice_model.Invoice_Id).Select(i => i.Invoice_Type).FirstOrDefaultAsync();

            Invoice inv = await _context.Invoices.Where(i => i.Id == issued_Invoice_model.Invoice_Id).FirstOrDefaultAsync();
            
            var building = await _context.Buildings.Where(i => i.Id == issued_Invoice_model.Building_Id).FirstOrDefaultAsync();

            Issued_Invoice.Issued_Invoice_Name = inv.Invoice_Name;
            Issued_Invoice_Tenant.Issued_Invoice_Name = inv.Invoice_Name;

            switch (InvoiceType)
            {
                case "Po Stanu":
                    {
                        var PoStanuCena = issued_Invoice_model.Issued_Invoice_Amount_Total / building.Number_Of_Apartments;
                        foreach(var tenant in _context.Tenants.ToList())
                        {
                            if(tenant.Building_Id==issued_Invoice_model.Building_Id)
                            {
                                Issued_Invoice.Tenant_Id = tenant.Id;
                                Issued_Invoice.Issued_Invoice_Amount = PoStanuCena;

                                Issued_Invoice_Tenant.Tenant_Id = tenant.Id;
                                Issued_Invoice_Tenant.Issued_Invoice_Amount = PoStanuCena;

                                _context.Issued_Invoices.Add(Issued_Invoice);
                                _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);
                                
                                await _context.SaveChangesAsync();

                            }
                        }
                        
                        break;
                    }
                case "Po Stanaru":
                    {

                        var PoStanaru = issued_Invoice_model.Issued_Invoice_Amount_Total / building.Number_Of_Tenants;
                        foreach (var tenant in _context.Tenants.ToList())
                        {
                            if (tenant.Building_Id == issued_Invoice_model.Building_Id)
                            {
                                Issued_Invoice.Tenant_Id = tenant.Id;
                                Issued_Invoice.Issued_Invoice_Amount = PoStanaru*tenant.Number_Of_Occupants;

                                Issued_Invoice_Tenant.Tenant_Id = tenant.Id;
                                Issued_Invoice_Tenant.Issued_Invoice_Amount = PoStanaru * tenant.Number_Of_Occupants;

                                _context.Issued_Invoices.Add(Issued_Invoice);
                                _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                                await _context.SaveChangesAsync();

                            }
                        }
                        
                        break;
                    }
                case "Po Metru Kvadratnom":
                    {
                        var PoMetruKvadratnom = inv.Invoice_Payment_Per_Square_Meter;
                        foreach (var tenant in _context.Tenants.ToList())
                        {
                            if (tenant.Building_Id == issued_Invoice_model.Building_Id)
                            {
                                Issued_Invoice.Tenant_Id = tenant.Id;
                                Issued_Invoice.Issued_Invoice_Amount = PoMetruKvadratnom * tenant.Quadrature;

                                Issued_Invoice_Tenant.Tenant_Id = tenant.Id;
                                Issued_Invoice_Tenant.Issued_Invoice_Amount = PoMetruKvadratnom * tenant.Quadrature;

                                _context.Issued_Invoices.Add(Issued_Invoice);
                                _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                                await _context.SaveChangesAsync();

                            }
                        }
                        
                        break;
                    }
                default:break;
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Issued_InvoiceExists(Issued_Invoice.Tenant_Id, Issued_Invoice.Invoice_Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            var ret = _context.Issued_Invoices.Where(i => i.Invoice_Id == issued_Invoice_model.Invoice_Id).ToList();
            return Ok( new { ret });
        }

        // POST: api/IssuedInvoices/Single/?tenant_id={tenant_id}&invoice_id={invoice_id}
        [HttpPost]
        [Route("Single")]
        public async Task<IActionResult> PostIssued_Invoice_By_Tenant_Id(int tenant_Id, int invoice_Id, [FromBody] IssuedInvoiceModel issued_Invoice_Model) //Create a new Issued_Invoice
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Issued_Invoice = new Issued_Invoice();
            var Issued_Invoice_Tenant = new IssuedInvoiceTenant();

            PropertiesComparison.CompareAndForward(Issued_Invoice, issued_Invoice_Model);
            PropertiesComparison.CompareAndForward(Issued_Invoice_Tenant, issued_Invoice_Model);

            Issued_Invoice.Issued_Invoice_Creation_DateTime = DateTime.Now;
            Issued_Invoice_Tenant.Issued_Invoice_Creation_DateTime = DateTime.Now;

            var InvoiceType = await _context.Invoices.Where(i => i.Id == invoice_Id).Select(i => i.Invoice_Type).FirstOrDefaultAsync();

            Invoice inv = await _context.Invoices.Where(i => i.Id == invoice_Id).FirstOrDefaultAsync();

            var building = await _context.Buildings.Where(i => i.Id == issued_Invoice_Model.Building_Id).FirstOrDefaultAsync();

            Issued_Invoice.Issued_Invoice_Name = inv.Invoice_Name;
            Issued_Invoice_Tenant.Issued_Invoice_Name = inv.Invoice_Name;

            switch (InvoiceType)
            {
                case "Po Stanu":
                    {
                        var PoStanuCena = issued_Invoice_Model.Issued_Invoice_Amount_Total / building.Number_Of_Apartments;
                        
                        Issued_Invoice.Tenant_Id = tenant_Id;
                        Issued_Invoice.Invoice_Id = invoice_Id;
                        Issued_Invoice.Issued_Invoice_Amount = PoStanuCena;

                        Issued_Invoice_Tenant.Tenant_Id = tenant_Id;
                        Issued_Invoice_Tenant.Invoice_Id = invoice_Id;
                        Issued_Invoice_Tenant.Issued_Invoice_Amount = PoStanuCena;

                        _context.Issued_Invoices.Add(Issued_Invoice);
                        _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                        await _context.SaveChangesAsync();
                    
                        break;
                    }
                case "Po Stanaru":
                    {
                        var tenant = await _context.Tenants.Where(i => i.Id == tenant_Id).FirstOrDefaultAsync();

                        var PoStanaru = issued_Invoice_Model.Issued_Invoice_Amount_Total / building.Number_Of_Tenants;
                        
                        Issued_Invoice.Tenant_Id = tenant_Id;
                        Issued_Invoice.Invoice_Id = invoice_Id;
                        Issued_Invoice.Issued_Invoice_Amount = PoStanaru * tenant.Number_Of_Occupants;

                        Issued_Invoice_Tenant.Tenant_Id = tenant_Id;
                        Issued_Invoice_Tenant.Invoice_Id = invoice_Id;
                        Issued_Invoice_Tenant.Issued_Invoice_Amount = PoStanaru * tenant.Number_Of_Occupants;

                        _context.Issued_Invoices.Add(Issued_Invoice);
                        _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                        await _context.SaveChangesAsync();
                        
                        break;
                    }
                case "Po Metru Kvadratnom":
                    {
                        var tenant = await _context.Tenants.Where(i => i.Id == tenant_Id).FirstOrDefaultAsync();

                        var PoMetruKvadratnom = inv.Invoice_Payment_Per_Square_Meter;
                        
                        Issued_Invoice.Tenant_Id = tenant_Id;
                        Issued_Invoice.Invoice_Id = invoice_Id;
                        Issued_Invoice.Issued_Invoice_Amount = PoMetruKvadratnom * tenant.Quadrature;

                        Issued_Invoice_Tenant.Tenant_Id = tenant_Id;
                        Issued_Invoice_Tenant.Invoice_Id = invoice_Id;
                        Issued_Invoice_Tenant.Issued_Invoice_Amount = PoMetruKvadratnom * tenant.Quadrature;

                        _context.Issued_Invoices.Add(Issued_Invoice);
                        _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                        await _context.SaveChangesAsync();
                        
                        break;
                    }
                default:
                    {
                        Issued_Invoice.Tenant_Id = tenant_Id;
                        Issued_Invoice.Invoice_Id = invoice_Id;
                        Issued_Invoice.Issued_Invoice_Amount = issued_Invoice_Model.Issued_Invoice_Amount_Total;

                        Issued_Invoice_Tenant.Tenant_Id = tenant_Id;
                        Issued_Invoice_Tenant.Invoice_Id = invoice_Id;
                        Issued_Invoice_Tenant.Issued_Invoice_Amount = issued_Invoice_Model.Issued_Invoice_Amount_Total;

                        _context.Issued_Invoices.Add(Issued_Invoice);
                        _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                        await _context.SaveChangesAsync();
                        
                        break;
                    }

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Issued_InvoiceExists(Issued_Invoice.Tenant_Id, Issued_Invoice.Invoice_Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            var ret = _context.Issued_Invoices.Where(i => (i.Invoice_Id == invoice_Id && i.Tenant_Id == tenant_Id)).ToList();
            return Ok(new { ret });
        }
        
        // DELETE: api/IssuedInvoices/?tenant_id={tenant_id}&invoice_id={invoice_id}
        [HttpDelete]
        public async Task<IActionResult> DeleteIssued_Invoice(int tenant_id, int invoice_id) //Delete Issued_Invoice with specific pair of Tenant_Id + Invoice_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var issued_Invoice = _context.Issued_Invoices.Where(i => (i.Invoice_Id == invoice_id && i.Tenant_Id == tenant_id)).FirstOrDefault();
            if (issued_Invoice == null)
            {
                return NotFound();
            }

            _context.Issued_Invoices.Remove(issued_Invoice);
            await _context.SaveChangesAsync();

            return Ok(issued_Invoice);
        }

        // DELETE: api/IssuedInvoices/{InvoiceName}
        [HttpDelete("Invoice_Name")]
        [Route("{Invoice_Name}")]
        public async Task<IActionResult> DeleteIssued_Invoice_By_InvoiceName([FromRoute] string Invoice_Name) //Delete Issued_Invoice with specific InvoiceName
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var issuedInvoice in await _context.Issued_Invoices.Where(i => i.Issued_Invoice_Name == Invoice_Name).ToListAsync())
            {
                if (issuedInvoice == null)
                {
                    continue;
                }

                _context.Issued_Invoices.Remove(issuedInvoice);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }



        private bool Issued_InvoiceExists(int tenant_id, int invoice_id)
        {
            return _context.Issued_Invoices.Any(i => (i.Invoice_Id == invoice_id && i.Tenant_Id == tenant_id));
        }

    }
}