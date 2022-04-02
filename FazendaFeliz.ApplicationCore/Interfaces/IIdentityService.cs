using System;
using System.Collections.Generic;
using System.Text;

namespace FazendaFeliz.ApplicationCore.Interfaces.Service
{
    public interface IIdentityService
    {
        string ObterNome();
        string ObterEmail();
        string ObterRole();
        bool IsAuthenticated();
    }
}
