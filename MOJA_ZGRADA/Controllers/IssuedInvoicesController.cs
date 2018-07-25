﻿using System;
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

        // GET: api/IssuedInvoices
        [HttpGet]
        public IEnumerable<Issued_Invoice> GetIssued_Invoices() //Get all Issued_Invoices
        {
            return _context.Issued_Invoices;
        }

        // GET: api/IssuedInvoices/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssued_Invoice([FromRoute] int id) //Get Issued_Invoice with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var issued_Invoice = await _context.Issued_Invoices.FindAsync(id);

            if (issued_Invoice == null)
            {
                return NotFound();
            }

            return Ok(issued_Invoice);
        }

        // PUT: api/IssuedInvoices/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssued_Invoice([FromRoute] int id, [FromBody] Issued_Invoice issued_Invoice) //Update Issued_Invoice with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != issued_Invoice.Invoice_Id)
            {
                return BadRequest();
            }

            _context.Entry(issued_Invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Issued_InvoiceExists(id))
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

        // POST: api/IssuedInvoices
        [HttpPost]
        public async Task<IActionResult> PostIssued_Invoice([FromBody] IssuedInvoiceModel issued_Invoice_model) //Create a new Issued_Invoice
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

            var InvoiceType = _context.Invoices.Where(i=>i.Id==issued_Invoice_model.Invoice_Id).Select(i => i.Invoice_Type).FirstOrDefault();

            Invoice inv = _context.Invoices.Where(i => i.Invoice_Type == InvoiceType).FirstOrDefault();
            
            var building = _context.Buildings.Where(i => i.Id == issued_Invoice_model.Building_Id).FirstOrDefault();

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
                        foreach (var tenant in _context.Tenants)
                        {
                            if (tenant.Building_Id == issued_Invoice_model.Building_Id)
                            {
                                Issued_Invoice.Tenant_Id = tenant.Id;
                                Issued_Invoice.Issued_Invoice_Amount = PoStanaru*tenant.Number_Of_Occupants;

                                Issued_Invoice_Tenant.Tenant_Id = tenant.Id;
                                Issued_Invoice_Tenant.Issued_Invoice_Amount = PoStanaru * tenant.Number_Of_Occupants;

                                _context.Issued_Invoices.Add(Issued_Invoice);
                                _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                                _context.SaveChanges();
                                
                            }
                        }
                        
                        break;
                    }
                case "Po Metru Kvadratnom":
                    {
                        var PoMetruKvadratnom = inv.Invoice_Payment_Per_Square_Meter;
                        foreach (var tenant in _context.Tenants)
                        {
                            if (tenant.Building_Id == issued_Invoice_model.Building_Id)
                            {
                                Issued_Invoice.Tenant_Id = tenant.Id;
                                Issued_Invoice.Issued_Invoice_Amount = PoMetruKvadratnom * tenant.Quadrature;

                                Issued_Invoice_Tenant.Tenant_Id = tenant.Id;
                                Issued_Invoice_Tenant.Issued_Invoice_Amount = PoMetruKvadratnom * tenant.Quadrature;

                                _context.Issued_Invoices.Add(Issued_Invoice);
                                _context.IssuedInvoiceTenants.Add(Issued_Invoice_Tenant);

                                _context.SaveChanges();

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
                if (Issued_InvoiceExists(Issued_Invoice.Invoice_Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            
            return Ok( new {_context.Issued_Invoices});
        }

        // DELETE: api/IssuedInvoices/?tenant_id={tenant_id}&invoice_id={invoice_id}
        [HttpDelete]
        public async Task<IActionResult> DeleteIssued_Invoice(int tenant_id, int invoice_id) //Delete Issued_Invoice with specific Id
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



        private bool Issued_InvoiceExists(int id)
        {
            return _context.Issued_Invoices.Any(e => e.Invoice_Id == id);
        }

    }
}