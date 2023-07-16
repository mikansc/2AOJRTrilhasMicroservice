using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instyga.Trilhas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trilhas.Models;
using TrilhasMicroservice.Repositories;

namespace Trilhas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrilhasController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly ITrilhaService service;

        public TrilhasController(IUnitOfWork context, ITrilhaService service)
        {
            _context = context;
            this.service = service;
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
        public async Task<ActionResult<Trilha>> PutTrilha(int id, Trilha trilha)
        {
            var result = await service.Atualizar(id, trilha);

            return FromServiceResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Trilha>> PostTrilha(Trilha trilha)
        {
            var result = await service.Incluir(trilha);
       
            return FromServiceResult(result);
        }

        private ActionResult<T> FromServiceResult<T>(ServiceResult<T> result) where T : Model
        {
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
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
    }
}
