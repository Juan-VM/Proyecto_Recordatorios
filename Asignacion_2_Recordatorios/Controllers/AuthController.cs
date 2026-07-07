using Asignacion_2_Recordatorios.Models;
using Asignacion_2_Recordatorios.Services;
using Microsoft.AspNetCore.Mvc;

namespace Asignacion_2_Recordatorios.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            try
            {
                var client = await SupabClient.GetClient();

                var usuario = await client
                    .From<Usuario>()
                    .Where(x => x.Correo == model.Correo)
                    .Single();

                if (usuario == null)
                {
                    ViewBag.Error = "_UsuarioNoExiste";

                    return View(model);
                }
                if (usuario.Bloqueado)
                {
                    ViewBag.Error = "_UsuarioBloqueado";

                    return View(model);
                }
                if (usuario.Password != model.Password)
                {
                    ViewBag.Error = "_PasswordIncorrecto";

                    return View(model);
                }

                HttpContext.Session.SetInt32(
                    "UsuarioId",
                    usuario.Id);

                HttpContext.Session.SetString(
                    "UsuarioNombre",
                    usuario.Nombre);

                return RedirectToAction(
                    "Index",
                    "Recordatorio");
            }
            catch
            {
                ViewBag.Error = "_ErrorSupabase";
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}