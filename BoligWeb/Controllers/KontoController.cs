using BoligWebApi.Exeptions;
using BoligWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoligWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]


    public class KontoController : ControllerBase
    {
        private readonly BoligWebContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public KontoController(BoligWebContext context, UserManager<IdentityUser> userManager, 
                                                        SignInManager<IdentityUser> signInManager )
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
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
        public async Task<IActionResult> PutKonto(string email, Konto konto)
        {
            if (email != konto.Email)
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
                if (!KontoExists(email))
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
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = konto.Email, Email = konto.Email };
                var result = await userManager.CreateAsync(user, konto.Password);
                 if (result.Succeeded)
                {
                    Console.WriteLine(result);
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index","Home");
                }
                 foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return konto;
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

        private bool KontoExists(string email)
        {
            return _context.Kontos.Any(e => e.Email == email );
        }
    }
}
