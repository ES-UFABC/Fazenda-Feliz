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
    [Route("anuncio")]
    public class AnuncioController : Controller
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IAnuncioRepository _anuncioRepository;
        public readonly IIdentityService _identityService;
        public readonly IReclamacaoRepository _reclamacaoRepository;
        public Usuario usuarioLogado;

        public AnuncioController(IUsuarioRepository usuarioRepository, IAnuncioRepository anuncioRepository, IReclamacaoRepository reclamacaoRepository, IIdentityService identityService)
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

        public async Task<IActionResult> Index(string tipo, string s)
        {
            if (_identityService.ObterRole() == "Produtor")
            {
                List<Anuncio> anuncios = await _anuncioRepository.ObterAnunciosProdutor(usuarioLogado.Id);
                return View("/Views/Anuncio/AnunciosProdutor.cshtml", anuncios);
            }
            else
            {
                List<AnuncioCompleto> anuncios = await _anuncioRepository.ObterTodosCompletos(usuarioLogado.Id, tipo, s);
                return View("/Views/Anuncio/AnunciosConsumidor.cshtml", anuncios);
            }
        }

        [HttpGet("criar")]
        public IActionResult CriarAnuncio()
        {
            return View();
        }

        [HttpGet("editar/{idAnuncio}")]
        public async Task<IActionResult> EditarAnuncio(int idAnuncio)
        {
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            
            return View("EditarAnuncio", anuncio);
        }

        [HttpPost("ocultar")]
        public async Task<IActionResult> OcultarAnuncio([FromBody]int idAnuncio)
        {
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            if(anuncio.Oculto)
            {
                anuncio.Oculto = false;
            }
            else
            {
                anuncio.Oculto = true;
            }
            await _anuncioRepository.SaveChanges();

            //return View();
            return Json(1);
        }

        [HttpPost("excluir")]
        public async Task<IActionResult> ExcluirAnuncio([FromBody]int idAnuncio)
        {
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            await _anuncioRepository.Remover(anuncio);
            return Json(1);
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarAnuncio([FromBody] Anuncio anuncioData)
        {
            anuncioData.Id_Usuario = usuarioLogado.Id;
            if (anuncioData.Id == 0) {//se o anuncio for novo
                _anuncioRepository.Add(anuncioData);
            }
            else{//anuncio antigo sendo editado
                var anuncio = await _anuncioRepository.ObterPorId(anuncioData.Id);
                anuncio.Titulo      = anuncioData.Titulo;
                anuncio.Localizacao = anuncioData.Localizacao;
                anuncio.Categoria   = anuncioData.Categoria;
                anuncio.Preco       = anuncioData.Preco;
                anuncio.Descricao   = anuncioData.Descricao;
                anuncio.Imagem_Base64 = anuncioData.Imagem_Base64;
            }
            
            await _anuncioRepository.SaveChanges();

            return Json(1);
        }

    }
}