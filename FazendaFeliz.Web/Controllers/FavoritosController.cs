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
    [Route("favoritos")]
    public class FavoritosController : Controller
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IAnuncioRepository _anuncioRepository;
        public readonly IIdentityService _identityService;
        public readonly IReclamacaoRepository _reclamacaoRepository;

        public FavoritosController(IUsuarioRepository usuarioRepository, IAnuncioRepository anuncioRepository, IReclamacaoRepository reclamacaoRepository, IIdentityService identityService)
        {
            _usuarioRepository = usuarioRepository;
            _anuncioRepository = anuncioRepository;
            _reclamacaoRepository = reclamacaoRepository;
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            var anuncios = await _anuncioRepository.ObterTodos();         
            return View("/Views/Favoritos/AnunciosFavoritos.cshtml", anuncios);
        }


        [HttpPost("/favoritos/favoritar")]
        public async Task<IActionResult> FavoritarAnuncio([FromBody] int idAnuncio)
        {
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            await _anuncioRepository.SaveChanges();

            //return View();
            return Json(1);
        }
    }
}