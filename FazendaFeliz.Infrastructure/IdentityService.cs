using Microsoft.AspNetCore.Http;
using FazendaFeliz.ApplicationCore.Interfaces.Service;
using System.Security.Claims;

namespace FazendaFeliz.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor accessor;

        public IdentityService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public string ObterEmail()
        {
            return GetUser().Identity.Name;
        }

        public string ObterNome()
        {
            return GetUser()?.Claims?.Single(u => u.Type == "name").Value;
        }
        
        public string ObterRole()
        {
            return GetUser()?.Claims?.Where(i => i.Type.Contains("role")).FirstOrDefault().Value;
        }

        public bool IsAuthenticated()
        {
            return GetUser().Identity.IsAuthenticated;
        }

        public ClaimsPrincipal GetUser() => accessor?.HttpContext?.User;
    }
}
