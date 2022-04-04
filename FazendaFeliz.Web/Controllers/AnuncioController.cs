using FazendaFeliz.ApplicationCore.Business;
using FazendaFeliz.ApplicationCore.Interfaces.Repository;
using FazendaFeliz.ApplicationCore.Interfaces.Service;
using FazendaFeliz.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

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

        public AnuncioController(IUsuarioRepository usuarioRepository, IAnuncioRepository anuncioRepository, IReclamacaoRepository reclamacaoRepository, IIdentityService identityService)
        {
            _usuarioRepository = usuarioRepository;
            _anuncioRepository = anuncioRepository;
            _reclamacaoRepository = reclamacaoRepository;
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["TipoUsuario"] = _identityService.ObterRole();
            var anuncios = await _anuncioRepository.ObterTodos();
            if (String.Equals(_identityService.ObterRole(), "Produtor"))
                return View("/Views/Anuncio/AnunciosProdutor.cshtml", anuncios);
            else
                return View("/Views/Anuncio/AnunciosConsumidor.cshtml", anuncios);
        }

        [HttpGet("criar")]
        public IActionResult CriarAnuncio()
        {
            ViewData["TipoUsuario"] = _identityService.ObterRole();
            return View();
        }

        [HttpGet("editar/{idAnuncio}")]
        public async Task<IActionResult> EditarAnuncio(int idAnuncio)
        {
            ViewData["TipoUsuario"] = _identityService.ObterRole();
            //OBTER O ANUNCIO DO BANCO E CARREGAR NA PÁGINA
            //puxo meu anuncio do BD pelo id
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            
            return View("EditarAnuncio", anuncio);
        }

        [HttpPost("ocultar")]
        public async Task<IActionResult> OcultarAnuncio([FromBody]int idAnuncio)
        {
            //OBTER O ANUNCIO DO BANCO E CARREGAR NA PÁGINA
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);

            if (anuncio.Oculto == false)
                anuncio.Oculto = true;
            else anuncio.Oculto = false;

            await _anuncioRepository.SaveChanges();

            //return View();
            return Json(1);
        }

        [HttpPost("excluir")]
        public async Task<IActionResult> ExcluirAnuncio([FromBody]int idAnuncio)
        {
            //OBTER O ANUNCIO DO BANCO E CARREGAR NA PÁGINA
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            await _anuncioRepository.Remover(anuncio);
            return Json(1);
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarAnuncio([FromBody] Anuncio anuncioData)
        {
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

        [HttpGet("reclamar/{idAnuncio}")]
        public IActionResult ReclamarAnuncio()
        {
            ViewData["TipoUsuario"] = _identityService.ObterRole();
            //OBTER O ANUNCIO DO BANCO E CARREGAR NA PÁGINA
            //puxo meu anuncio do BD pelo id

            return View("ReclamarAnuncio");
        }

        [HttpPost("reclamar/{idAnuncio}")]
        public async Task<IActionResult> CriarReclamacao([FromBody] Reclamacao reclamacaoData)
        {
            
            _reclamacaoRepository.Add(reclamacaoData);
         
            await _reclamacaoRepository.SaveChanges();

            return Json(1);
        }
    }
}