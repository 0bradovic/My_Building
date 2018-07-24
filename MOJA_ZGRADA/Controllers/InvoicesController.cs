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
    public class InvoicesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InvoicesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public IEnumerable<Invoice> GetInvoices() //Get all invoices
        {
            return _context.Invoices;
        }

        // GET: api/Invoices/Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice([FromRoute] int id) //Get Invoice with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/Invoices/Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice([FromRoute] int id, [FromBody] Invoice invoice) //Update Invoice with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        [HttpPost]
        public async Task<IActionResult> PostInvoice([FromBody] Invoice invoice) //Create a new Invoice
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InvType = _context.Invoices.Where(i => i.Invoice_Name == invoice.Invoice_Name).Select(t => t.Id).FirstOrDefault();

            if (InvType != 0)
            {
                var error = @"Invoice with this Name already exist!";
                return NotFound(new { error });
            }
            if(invoice.Building_Id==0)
            {
                var error = @"Building Id must be inserted!";
                return NotFound(new { error });
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoices/Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] int id) //Delete Invoice with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return Ok(invoice);
        }



        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}