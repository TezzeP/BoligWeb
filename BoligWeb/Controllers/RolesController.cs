using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoligWebApi.Exeptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoligWebApi.Models;

namespace BoligWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RolesController : ControllerBase
    {
        private readonly BoligWebContext _context;

        public RolesController(BoligWebContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Konto>>> GetRoles()
        {
            var results = await _context.Roles.ToListAsync();
            return results;
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Konto>> GetRole(int id)
        {
            if (id <= 0)
            {
                throw new NotPosstiveNumberExeption("Id of role must not be zero or negative ");

            }
                var role = await _context.Roles.FindAsync(id);

                if (role == null)
            {
                return NotFound();
            }


            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Konto role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
               
            }

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Konto>> PostRole([FromBody]Konto role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Konto>> DeleteRole(int id)
        {
            if (id <= 0)
            {
                throw new NotPosstiveNumberExeption("Id of role must not be zero or negative ");
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }



    }
}
