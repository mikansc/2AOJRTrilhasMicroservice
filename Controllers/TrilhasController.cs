using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using TrilhasMicroservice.Repositories;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrilhasController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public TrilhasController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trilha>>> GetTrilhas()
        {
            return await _context.Trilhas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trilha>> GetTrilha(int id)
        {
            var Trilha = await _context.Trilhas.FirstOrDefaultAsync(x => x.Id == id);

            if (Trilha == null)
            {
                return NotFound();
            }

            return Trilha;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrilha(int id, Trilha trilha)
        {
            if (id != trilha.Id)
            {
                return BadRequest();
            }

            _context.MarkAsModified(trilha);
            try
            {
                await _context.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrilhaExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Trilha>> PostTrilha(Trilha trilha)
        {
            if (trilha.Id != default(int))
                return BadRequest("Para atualizar uma entidade utilize PUT. Caso deseja salvar uma entidade, não especifique um ID");

            _context.Add(trilha);
            await _context.Commit();

            return CreatedAtAction("PostTrilha", new { id = trilha.Id }, trilha);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrilha(int id)
        {
            var Trilha = await _context.Trilhas.FirstOrDefaultAsync(x => x.Id == id);
            if (Trilha == null)
            {
                return NotFound();
            }

            _context.Remove(Trilha);
            await _context.Commit();

            return new OkResult();
        }

        private bool TrilhaExists(int id)
        {
            return _context.Trilhas.Any(e => e.Id == id);
        }
    }
}
