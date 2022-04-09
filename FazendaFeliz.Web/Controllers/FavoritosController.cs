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
    [Route("favoritos")]
    public class FavoritosController : Controller
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IAnuncioRepository _anuncioRepository;
        public readonly IIdentityService _identityService;
        public readonly IFavoritoRepository _favoritoRepository;
        public Usuario usuarioLogado;

        public FavoritosController(IUsuarioRepository usuarioRepository, IAnuncioRepository anuncioRepository, IFavoritoRepository favoritoRepository, IIdentityService identityService)
        {
            _usuarioRepository = usuarioRepository;
            _anuncioRepository = anuncioRepository;
            _favoritoRepository = favoritoRepository;
            _identityService = identityService;
        }

        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            usuarioLogado = _usuarioRepository.ObterPorEmail(_identityService.ObterEmail());
        }

        public async Task<IActionResult> Index()
        {
            var anuncios = await _favoritoRepository.ObterAnunciosFavoritosPorUsuario(usuarioLogado.Id);
            return View("/Views/Favoritos/AnunciosFavoritos.cshtml", anuncios);
        }


        [HttpPost("/favoritos/favoritar")]
        public async Task<IActionResult> FavoritarAnuncio([FromBody] int idAnuncio)
        {
            var favorito = await _favoritoRepository.ObterPorUsuarioAnuncio(usuarioLogado.Id, idAnuncio);
            if(favorito is not null)
            {
                await _favoritoRepository.Remover(favorito);
                return Json(1);
            }

            await _favoritoRepository.Adicionar(new Favorito()
            {
                Id_Anuncio = idAnuncio,
                Id_Usuario = usuarioLogado.Id
            });

            return Json(1);
        }
    }
}