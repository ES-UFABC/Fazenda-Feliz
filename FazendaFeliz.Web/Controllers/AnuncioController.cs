using FazendaFeliz.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FazendaFeliz.Web.Controllers
{
    [Route("anuncio")]
    public class AnuncioController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AnuncioController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["TipoUsuario"] = "Produtor";
            //PEGA OS MEUS ANUNCIOS DO BANCO E PASSA PRA VIEW
            return View();
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
        public IActionResult ExcluirAnuncio([FromBody]int idAnuncio)
        {
            //OBTER O ANUNCIO DO BANCO E CARREGAR NA PÁGINA
            return View();
        }

        [HttpPost("criar")]
        public IActionResult CriarAnuncio([FromBody] CadastroAnuncioModel anuncioData)
        {
            //INSERIR NO BANCO
            //RETORNAR O ID DO ANUNCIO
            return Json(1);
        }
    }
}