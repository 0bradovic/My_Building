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
    public class MessageTenantsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MessageTenantsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/MessageTenants
        [HttpGet]
        public IEnumerable<MessageTenant> GetMessageTenants() //Get all Tenant Messages from db
        {
            return _context.MessageTenants;
        }

        // GET: api/MessageTenants/Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageTenant([FromRoute] int id) //Get Tenant Message with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messageTenant = await _context.MessageTenants.FindAsync(id);

            if (messageTenant == null)
            {
                return NotFound();
            }

            return Ok(messageTenant);
        }
        
        // GET: api/MessageTenants/Tenant/{Tenant_Id}
        [HttpGet("{Tenant_Id}")]
        [Route("Tenant/{Tenant_Id}")]
        public async Task<IActionResult> GetMessageTenantByTenantId([FromRoute] int Tenant_Id) //Get Tenant Messages by specific Tenant_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.MessageTenants.Where(t => t.Tenant_Id == Tenant_Id).ToListAsync();

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // GET: api/MessageTenant/Admin/{Admin_Id}
        [HttpGet("{Admin_Id}")]
        [Route("Admin/{Admin_Id}")]
        public async Task<IActionResult> GetMessageTenantByAdminId([FromRoute] int Admin_Id) //Get Tenant Messages with specific Admin_Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.MessageTenants.Where(t => t.Admin_Id == Admin_Id).ToListAsync();

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        //No PUT for messages
        /*
        // PUT: api/MessageTenants/Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageTenant([FromRoute] int id, [FromBody] MessageTenant messageTenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageTenant.Id)
            {
                return BadRequest();
            }

            _context.Entry(messageTenant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageTenantExists(id))
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
        */


        // POST: api/MessageTenants
        [HttpPost]
        public async Task<IActionResult> PostMessageTenant([FromBody] MessageModel messageModel) //Create a new Message issued by Tenant
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = new Message();
            var messageTenant = new MessageTenant();

            PropertiesComparison.CompareAndForward(message, messageModel);
            PropertiesComparison.CompareAndForward(messageTenant, messageModel);

            message.Message_Issued_DateTime = DateTime.Now;
            messageTenant.Message_Issued_DateTime = DateTime.Now;

            try
            {
                _context.Messages.Add(message);
                _context.MessageTenants.Add(messageTenant);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetMessage", new { id = message.Id }, message);
                CreatedAtAction("GetMessageTenant", new { id = messageTenant.Id }, message);

                return Ok(messageTenant);
            }
            catch (Exception ex)
            {
                return NotFound(new { ex });
            }

        }

        // DELETE: api/MessageTenants/Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageTenant([FromRoute] int id) //Delete Tenant Message with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messageTenant = await _context.MessageTenants.FindAsync(id);
            if (messageTenant == null)
            {
                return NotFound();
            }

            _context.MessageTenants.Remove(messageTenant);
            await _context.SaveChangesAsync();

            return Ok(messageTenant);
        }




        //Only for incode calling
        public async Task<IActionResult> GetMessage([FromRoute] int id) //Get Admin Message with specific Id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }
        
        private bool MessageTenantExists(int id)
        {
            return _context.MessageTenants.Any(e => e.Id == id);
        }
    }
}