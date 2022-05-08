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
        public readonly IFavoritoRepository _favoritoRepository;
        public Usuario usuarioLogado;

        public AnuncioController(IUsuarioRepository usuarioRepository, IAnuncioRepository anuncioRepository, IReclamacaoRepository reclamacaoRepository, IIdentityService identityService, IFavoritoRepository favoritoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _anuncioRepository = anuncioRepository;
            _reclamacaoRepository = reclamacaoRepository;
            _identityService = identityService;
            _favoritoRepository = favoritoRepository;
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

/*        [HttpGet("/anuncio/reclamar/{idAnuncio}")]
        public async Task<IActionResult> ReclamarAnuncio(int idAnuncio)
        {
            var anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            return View("ReclamarAnuncio", anuncio);
        }*/

        [HttpGet("/anuncio/descricao/{idAnuncio}")]
        public async Task<IActionResult> DescricaoAnuncio(int idAnuncio)
        {
            Descricao descricao = new Descricao();
            descricao.Anuncio = await _anuncioRepository.ObterPorId(idAnuncio);
            descricao.Usuario = await _usuarioRepository.ObterPorId(descricao.Anuncio.Id_Usuario);
            descricao.Reclamacao = await _reclamacaoRepository.ObterReclamacoesProdutor(descricao.Anuncio.Id_Usuario);
            return View("DescricaoAnuncio", descricao);
        }

        [HttpGet("/produtor/{idUser}")]
        public async Task<IActionResult> Perfil(int idUser)
        {
            var produtor = new ProdutorDTO();
            produtor.Usuario = await _usuarioRepository.ObterPorId(idUser);

            if (produtor.Usuario.Tipo != "Produtor")
            {
                return Redirect("/anuncio");
            }

            produtor.Reclamacaos = await _reclamacaoRepository.ObterReclamacoesProdutor(produtor.Usuario.Id);
            produtor.Favorito = await _favoritoRepository.ObterPorUsuarioProdutor(usuarioLogado.Id, idUser) != null ? true : false;
            return View("/Views/Usuario/Produtor.cshtml", produtor);
        }

        /*        [HttpPost("/anuncio/reclamar/{idAnuncio}")]
                public async Task<IActionResult> CriarReclamacao([FromBody] Reclamacao reclamacaoData)
                {
                    //inserir relação MxN

                    reclamacaoData.Id_Usuario = usuarioLogado.Id;
                    _reclamacaoRepository.Add(reclamacaoData);
                    await _reclamacaoRepository.SaveChanges();
                    return Json(1);
                }*/
    }
}