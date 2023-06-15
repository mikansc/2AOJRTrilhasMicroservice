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
    public class AvaliacoesController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public AvaliacoesController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> GetAvaliacoesQuery([FromQuery] string trilha)
        {
            if (string.IsNullOrEmpty(trilha))
                return await _context.Avaliacoes.ToListAsync();

            var t = await _context.Trilhas.FirstOrDefaultAsync(x => x.Nome == trilha);

            if(t == null) return new List<Avaliacao>();

            return await _context.Avaliacoes.Where(x => x.TrilhaId == t.Id).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(int id)
        {
            var Avaliacao = await _context.Avaliacoes.FirstOrDefaultAsync(x => x.Id == id);

            if (Avaliacao == null)
            {
                return NotFound();
            }

            return Avaliacao;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvaliacao(int id, Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return BadRequest();
            }

            _context.MarkAsModified(avaliacao);

            try
            {
                await _context.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvaliacaoExists(id))
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
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao.Id != default(int))
                return BadRequest("Para atualizar uma entidade utilize PUT. Caso deseja salvar uma entidade, não especifique um ID");

            _context.Add(avaliacao);
            await _context.Commit();

            return CreatedAtAction("PostAvaliacao", new { id = avaliacao.Id },avaliacao);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAvaliacao(int id)
        {
            var avaliacao = await _context.Avaliacoes.FirstOrDefaultAsync(x => x.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            _context.Remove(avaliacao);
            await _context.Commit();

            return new OkResult();
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
