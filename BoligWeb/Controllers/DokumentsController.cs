using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoligWebApi.Models;

namespace BoligWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentsController : ControllerBase
    {
        private readonly BoligWebContext _context;

        public DokumentsController(BoligWebContext context)
        {
            _context = context;
        }

        // GET: api/Dokuments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dokument>>> GetFiles()
        {
            return await _context.Dokuments.ToListAsync();
        }

        // GET: api/Dokuments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dokument>> GetFile(int id)
        {
            var file = await _context.Dokuments.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        // PUT: api/Dokuments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(int id, Dokument dokument)
        {
            if (id != dokument.DocumentId)
            {
                return BadRequest();
            }

            _context.Entry(dokument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
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

        // POST: api/Dokuments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostFile(IFormFile files)
        {

            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtensio
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);


                    var objdDokuments = new Dokument()
                    {
                        DocumentId = 0,
                        Name = newFileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objdDokuments.DataFiles = target.ToArray();
                    }

                    _context.Dokuments.Add(objdDokuments);
                    _context.SaveChanges();

                }
            }
            //    _context.Dokuments.Add(file);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction(nameof(GetFile), new { id = file.DocumentId }, file);


            return Ok();
            
        }



        // DELETE: api/Dokuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _context.Dokuments.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.Dokuments.Remove(file);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FileExists(int id)
        {
            return _context.Dokuments.Any(e => e.DocumentId == id);
        }
    }
}
