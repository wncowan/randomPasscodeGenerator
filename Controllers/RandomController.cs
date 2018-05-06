using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace csharp_random.Controllers
{
    public class RandomController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Attempt") == null || HttpContext.Session.GetString("Passcode") == null)
            {
                HttpContext.Session.SetString("Passcode", "");
                HttpContext.Session.SetInt32("Attempt", 0);
                // TempData["attempt"] = 0;
                // TempData["passcode"] = "";
            }
            else
            {
                TempData["attempt"] = HttpContext.Session.GetInt32("Attempt");
                TempData["passcode"] = HttpContext.Session.GetString("Passcode");
            }
            return View();
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult Create()
        {
            int? attempt = HttpContext.Session.GetInt32("Attempt");
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] stringChars = new char[14];
            Random rand = new Random();
            for (int i=0; i<stringChars.Length; i++)
            {
                stringChars[i] = chars[rand.Next(chars.Length)];
            }
            string passcode = new String(stringChars);
            attempt += 1;
            HttpContext.Session.SetString("Passcode", passcode);
            HttpContext.Session.SetInt32("Attempt", (int)attempt);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}