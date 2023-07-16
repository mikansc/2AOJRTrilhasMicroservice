using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instyga.Notificacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notificacoes.Models;
using NotificacoesMicroservice.Repositories;

namespace Notificacoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacoesController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly INotificacoesService service;

        public NotificacoesController(IUnitOfWork context, INotificacoesService service)
        {
            _context = context;
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacao>>> GetNotificacoes()
        {
            return await _context.Notificacoes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacao>> GetNotificacao(int id)
        {
            var Notificacao = await _context.Notificacoes.FirstOrDefaultAsync(x => x.Id == id);

            if (Notificacao == null)
            {
                return NotFound();
            }

            return Notificacao;
        }

        [HttpPost]
        public async Task<ActionResult<Notificacao>> PostNotificacao(Notificacao Notificacao)
        {
            var result = await service.Incluir(Notificacao);
       
            return FromServiceResult(result);
        }

        private ActionResult<T> FromServiceResult<T>(ServiceResult<T> result) where T : Model
        {
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
