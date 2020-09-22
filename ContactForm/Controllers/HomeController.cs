using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactForm.Models;
using System.Net.Mail;

namespace ContactForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage _email = new MailMessage();
                    _email.From = new MailAddress(vm.Email);//Email which you are getting 
                                                         //from contact us page 
                    _email.To.Add("youremail@gmail.com");//Where mail will be sent
                    _email.Subject = vm.Subject;
                    _email.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("youreamil@gmail.com", "yourpassword");

                    smtp.EnableSsl = true;

                    smtp.Send(_email);

                    ModelState.Clear();
                    ViewBag.Message = "Thanks for your message!";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $"Something went wrong :(  {ex.Message}";
                }
            }

            return View();
        }

        public IActionResult Privacy()
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
