using FazendaFeliz.ApplicationCore.Business;
using FazendaFeliz.ApplicationCore.Interfaces.Repository;
using FazendaFeliz.ApplicationCore.Interfaces.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;


namespace FazendaFeliz.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IIdentityService _identityService;
        public readonly IReclamacaoRepository _reclamacaoRepository;
        public readonly IFavoritoRepository _favoritoRepository;
        public Usuario usuarioLogado;


        public UsuarioController(IUsuarioRepository usuarioRepository, IReclamacaoRepository reclamacaoRepository, IIdentityService identityService, IFavoritoRepository favoritoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _identityService = identityService;
            _reclamacaoRepository = reclamacaoRepository;
            _favoritoRepository = favoritoRepository;
        }


        [HttpGet("/usuario/novo")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/perfil");
            return View("/Views/Usuario/CriarConta.cshtml");
        }

        [HttpPost("/usuario/novo")]
        public async Task<ActionResult> Create([FromBody] Usuario usuario)
        {
            await _usuarioRepository.Adicionar(usuario);
            return Json("ok");
        }

        [HttpGet("/usuario/login")]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/perfil");
            return View("/Views/Usuario/Login.cshtml");
        }

        [HttpPost("/usuario/login")]
        public async Task<ActionResult> Login([FromBody] Usuario usuario, string ReturnUrl)
        {
            var user = _usuarioRepository.ObterPorEmail(usuario.Email);
            if (user == null) return NotFound();

            if(user.Senha == usuario.Senha)
            {
                List<Claim> diretosAcesso = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Tipo)
                };

                var identity = new ClaimsIdentity(diretosAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal, 
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(1),
                        IsPersistent= true,
                    });
                return Json(ReturnUrl);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("/perfil")]
        public ActionResult Perfil()
        {
            var emailUsuario = _identityService.ObterEmail();
            var usuario = _usuarioRepository.ObterPorEmail(emailUsuario);
            return View("/Views/Usuario/Perfil.cshtml", usuario);
        }


        [Authorize]
        [HttpPost("/perfil/editar")]
        public async Task<ActionResult> Perfil([FromBody] Usuario user)
        {
            var emailUsuario = _identityService.ObterEmail();
            var usuario = _usuarioRepository.ObterPorEmail(emailUsuario);

            usuario.Telefone = user.Telefone;
            usuario.Email = user.Email;
            usuario.Nome = user.Nome;
            if (user.Senha != "")
                usuario.Senha = user.Senha;

            await _usuarioRepository.SaveChanges();

            return Json(1);
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
