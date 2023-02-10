using Gestao_Acesso03.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gestao_Acesso03.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "Autenticados")]
        public IActionResult Privacy()
        {
            return View();
        }


        // Exemplo Referente a POLICY
        [Authorize(Policy = "SomenteAdministrador")]
        public IActionResult Policy_Administrador()
        {
            return View();
        }

        // Exemplo Referente a ROLES
        [Authorize(Roles = "Administrador")]
        public IActionResult Roles_Administrador()
        {
            return View();
        }

        // Exemplo Referente a ROLES
        [Authorize(Roles = "Gerente")]
        public IActionResult Roles_Gerente()
        {
            return View();
        }


        // Exemplo Referente a ROLES
        [Authorize(Roles = "Supervisor")]
        public IActionResult Roles_Supervisor()
        {
            return View();
        }

        // Exemplo Referente a ROLES
        [Authorize(Roles = "Usuario")]
        public IActionResult Roles_Usuario()
        {
            return View();
        }









        [Route("/PageNotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }


       




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
