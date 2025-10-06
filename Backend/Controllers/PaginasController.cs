using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using ProyectoWebFinal.Filters;

namespace ProyectoWebFinal.Controllers
{
    public class PaginasController : Microsoft.AspNetCore.Mvc.Controller
    {


        public PaginasController()
        {
        }

       [AuthorizeSession(1)] // Solo Admin
    public IActionResult Admin()
    {
        return View();
    }

    [AuthorizeSession(2)] // Solo Estudiante
    public IActionResult Estudiante()
    {
        return View();
    }

    [AuthorizeSession(3)] // Solo Profesor
    public IActionResult Profesor()
    {
        return View();
    }

    }
}