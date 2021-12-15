using BoligWebApi.Exeptions;
using BoligWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoligWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class KontoController : ControllerBase
    {
        private readonly BoligWebContext _context;

        public KontoController(BoligWebContext context)
        {
            _context = context;
        }

        // GET: api/Kontos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Konto>>> GetKontos()
        {
            return await _context.Kontos.ToListAsync();
        }

        // GET: api/Kontos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Konto>> GetKonto(int id)
        {
            if (id <= 0)
            {
                throw new NotPosstiveNumberExeption("Id of konto must not be zero or negative ");

            }
            var konto = await _context.Kontos.FindAsync(id);

            if (konto == null)
            {
                return NotFound();
            }


            return konto;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKonto(int id, Konto konto)
        {
            if (id != konto.Id)
            {
                return BadRequest();
            }

            _context.Entry(konto).State = EntityState.Modified;

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
        public async Task<ActionResult<Konto>> PostKonto([FromBody] Konto konto)
        {
            _context.Kontos.Add(konto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = konto.Id }, konto);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Konto>> DeleteKonto(int id)
        {
            if (id <= 0)
            {
                throw new NotPosstiveNumberExeption("Id of konto must not be zero or negative ");
            }

            var konto = await _context.Kontos.FindAsync(id);
            if (konto == null)
            {
                return NotFound();
            }

            _context.Kontos.Remove(konto);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
