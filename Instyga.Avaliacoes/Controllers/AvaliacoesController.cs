using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instyga.Avaliacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Avaliacoes.Models;
using AvaliacoesMicroservice.Repositories;

namespace Avaliacoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacoesController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly IAvaliacoeservice service;

        public AvaliacoesController(IUnitOfWork context, IAvaliacoeservice service)
        {
            _context = context;
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> GetAvaliacoes()
        {
            return await _context.Avaliacoes.ToListAsync();
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
        public async Task<ActionResult<Avaliacao>> PutAvaliacao(int id, Avaliacao Avaliacao)
        {
            var result = await service.Atualizar(id, Avaliacao);

            return FromServiceResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(Avaliacao Avaliacao)
        {
            var result = await service.Incluir(Avaliacao);
       
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
        public async Task<ActionResult> DeleteAvaliacao(int id)
        {
            var Avaliacao = await _context.Avaliacoes.FirstOrDefaultAsync(x => x.Id == id);
            if (Avaliacao == null)
            {
                return NotFound();
            }

            _context.Remove(Avaliacao);
            await _context.Commit();

            return new OkResult();
        }
    }
}
