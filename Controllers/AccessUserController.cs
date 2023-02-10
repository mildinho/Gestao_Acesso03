using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gestao_Acesso03.Controllers
{
    public class AccessUserController : Controller
    {
        
        private readonly IHttpContextAccessor _accessor;

        public AccessUserController( IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _accessor.HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }


        public IActionResult AcessoNegado()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Valida_Login(string login, string senha)
        {
            IList<Claim> ListaClaims;

            if (login == "1" && senha == "1")
            {
                    ListaClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Usuário Um"),
                    new Claim(ClaimTypes.Email, "user01@furacao.com.br"),
                    new Claim(ClaimTypes.MobilePhone, "5519997438379"),
                    new Claim(ClaimTypes.Role, "Gerente"),
                    new Claim("GRUPO", "FWD;TAS;TEAM;"),
                    new Claim("MATRICULA", "08091")

                };
            } else if (login == "2" && senha == "2")
            {
                ListaClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Usuário Dois"),
                    new Claim(ClaimTypes.Email, "user02@furacao.com.br"),
                    new Claim(ClaimTypes.MobilePhone, "5519997438379"),
                    new Claim(ClaimTypes.Role, "Administrador"),
                    new Claim("GRUPO", "FWD;TAS;TEAM;"),
                    new Claim("MATRICULA", "08091")

                };
            }
            else if (login == "3" && senha == "3")
            {
                ListaClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Usuário Dois"),
                    new Claim(ClaimTypes.Email, "user02@furacao.com.br"),
                    new Claim(ClaimTypes.MobilePhone, "5519997438379"),
                    new Claim(ClaimTypes.Role, "Supervisor"),
                    new Claim("GRUPO", "FWD;TAS;TEAM;"),
                    new Claim("MATRICULA", "08091")

                };
            }
            else
            {
                ListaClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Convidado"),
                    new Claim(ClaimTypes.Email, "convidado@furacao.com.br"),
                    new Claim(ClaimTypes.MobilePhone, "(19)2101-3000"),
                    new Claim(ClaimTypes.Role, "Usuario"),
                    new Claim("GRUPO", ""),
                    new Claim("MATRICULA", "00000")

                };

            }
            //Criando uma Identidade e associando-a ao ambiente.
            ClaimsIdentity identity = new(ListaClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new(identity);


            await _accessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal));

            return RedirectToAction("Index", "Home"); 

        }
    }
}
