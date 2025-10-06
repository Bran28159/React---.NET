using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProyectoWebFinal.Filters
{
    public class AuthorizeSessionAttribute : ActionFilterAttribute
    {
        private readonly int _rolRequerido;

        public AuthorizeSessionAttribute(int rolRequerido)
        {
            _rolRequerido = rolRequerido;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var rol = context.HttpContext.Session.GetInt32("rol");

            if (rol == null || rol != _rolRequerido)
            {
                // Redirige a login si no tiene permiso
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
        }
    }
}
