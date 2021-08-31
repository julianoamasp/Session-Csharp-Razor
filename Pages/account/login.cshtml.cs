using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication15.Pages.account
{
    public class loginModel : PageModel
    {
        public void OnGet()
        {

            TempData["alerta"] = HttpContext.Session.GetString("alerta");
            HttpContext.Session.Remove("alerta");
            

        }

        public async Task<IActionResult> OnPost(LoginInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!IsUserAuthenticated(inputModel.usuario, inputModel.senha))
            {
                ModelState.AddModelError(string.Empty, "usuario senha invalida");
                return Page();
            }
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, inputModel.usuario) 
            };
            var useIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(useIdentity);
            await HttpContext.SignInAsync(principal);

            HttpContext.Session.SetString("alerta", "logado");
            //HttpContext.Session.SetString("sdf", JsonConvert.SerializeObject(onjeto));

            return Redirect("/");
        }

        public async Task<IActionResult> OnPostSair()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        private bool IsUserAuthenticated(string usuario, string senha)
        {
            return true;
        }
    }
}
