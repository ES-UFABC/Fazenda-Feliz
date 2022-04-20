using FazendaFeliz.ApplicationCore.Business;
using FazendaFeliz.ApplicationCore.Interfaces.Repository;
using FazendaFeliz.ApplicationCore.Interfaces.Service;
using FazendaFeliz.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FazendaFeliz.Web.Controllers
{
    [Authorize]
    [Route("reclamacoes")]
    public class ReclamacaoController : Controller
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IAnuncioRepository _anuncioRepository;
        public readonly IIdentityService _identityService;
        public readonly IReclamacaoRepository _reclamacaoRepository;
        public Usuario usuarioLogado;

        public ReclamacaoController(IUsuarioRepository usuarioRepository, IAnuncioRepository anuncioRepository, IReclamacaoRepository reclamacaoRepository, IIdentityService identityService)
        {
            _usuarioRepository = usuarioRepository;
            _anuncioRepository = anuncioRepository;
            _reclamacaoRepository = reclamacaoRepository;
            _identityService = identityService;
        }

        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            usuarioLogado = _usuarioRepository.ObterPorEmail(_identityService.ObterEmail());
        }

        [HttpGet("/anuncio/reclamar/{idAnuncio}")]
        public async Task<IActionResult> ReclamarAnuncio(int idAnuncio)
        {
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            return View("/Views/Reclamacoes/ReclamarAnuncio.cshtml", anuncio);
        }

        [HttpPost("/anuncio/reclamar/{idAnuncio}")]
        public async Task<IActionResult> CriarReclamacao([FromBody] Reclamacao reclamacaoData)
        {
            //inserir relação MxN

            reclamacaoData.Id_Usuario = usuarioLogado.Id;
            reclamacaoData.Nome = usuarioLogado.Nome;
            _reclamacaoRepository.Add(reclamacaoData);
            await _reclamacaoRepository.SaveChanges();
            return Json(1);
        }

        [HttpGet("/reclamacoes")]
        public async Task<IActionResult> ReclamacoesProdutor()
        {
            var reclamacoes = await _reclamacaoRepository.ObterReclamacoesProdutor(usuarioLogado.Id);
            return View("/Views/Reclamacoes/ReclamacoesProdutor.cshtml", reclamacoes);
        }

    }
}