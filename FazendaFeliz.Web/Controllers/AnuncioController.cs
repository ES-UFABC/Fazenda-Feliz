using FazendaFeliz.ApplicationCore.Business;
using FazendaFeliz.ApplicationCore.Interfaces.Repository;
using FazendaFeliz.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FazendaFeliz.Web.Controllers
{
    [Route("anuncio")]
    public class AnuncioController : Controller
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IAnuncioRepository _anuncioRepository;

        public AnuncioController(IUsuarioRepository usuarioRepository, IAnuncioRepository anuncioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _anuncioRepository = anuncioRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["TipoUsuario"] = "Produtor";
            //PEGA OS MEUS ANUNCIOS DO BANCO E PASSA PRA VIEW
            var anuncios = await _anuncioRepository.ObterTodos();
            return View(anuncios);
        }

        [HttpGet("criar")]
        public IActionResult CriarAnuncio()
        {
            ViewData["TipoUsuario"] = "Produtor";
            return View();
        }

        [HttpGet("editar/{idAnuncio}")]
        public IActionResult EditarAnuncio(int idAnuncio)
        {
            ViewData["TipoUsuario"] = "Produtor";
            //OBTER O ANUNCIO DO BANCO E CARREGAR NA PÁGINA
            return View();
        }

        [HttpPost("ocultar")]
        public IActionResult OcultarAnuncio([FromBody]int idAnuncio)
        {
            //OBTER O ANUNCIO DO BANCO E CARREGAR NA PÁGINA
            return View();
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
            _anuncioRepository.Add(anuncioData);
            await _anuncioRepository.SaveChanges();

            return Json(1);
        }
    }
}