using FazendaFeliz.ApplicationCore.Business;
using FazendaFeliz.ApplicationCore.Interfaces.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FazendaFeliz.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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
                    });
                return Json(ReturnUrl);
            }

            return Unauthorized();
        }

        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
