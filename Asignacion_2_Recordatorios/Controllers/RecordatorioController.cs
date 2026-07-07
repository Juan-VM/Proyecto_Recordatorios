using Asignacion_2_Recordatorios.Models;
using Asignacion_2_Recordatorios.Services;
using Microsoft.AspNetCore.Mvc;

namespace Asignacion_2_Recordatorios.Controllers
{
    public class RecordatorioController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var client = await SupabClient.GetClient();

            var response =
                await client
                .From<RecordatorioVista>()
                .Get();

            return View(response.Models);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            Recordatorio recordatorio)
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var client = await SupabClient.GetClient();

            recordatorio.UsuarioId = HttpContext.Session.GetInt32("UsuarioId").Value;
            recordatorio.Completado = false;

            await client
                .From<Recordatorio>()
                .Insert(recordatorio);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Completar(int id)
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var client = await SupabClient.GetClient();

            // 1. Buscar el registro por ID
            var recordatorio = await client
                .From<Recordatorio>()
                .Where(x => x.Id == id)
                .Single();

            if (recordatorio == null)
                return NotFound();

            // 2. Cambiar estado
            recordatorio.Completado = true;

            // 3. Actualizar en Supabase
            await recordatorio.Update<Recordatorio>();

            return RedirectToAction("Index");
        }
    }
}
