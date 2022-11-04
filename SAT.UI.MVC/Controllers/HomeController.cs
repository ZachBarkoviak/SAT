using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using SAT.UI.MVC.Models;
using System.Diagnostics;
using SAT.DATA.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace SAT.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly SATContext _context;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, SATContext context)
        {
            _logger = logger;
            _config = config;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Students = _context.Students.Count();
            ViewBag.Courses = _context.Courses.Count();
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Students = _context.Students.Count();
            ViewBag.Courses = _context.Courses.Count();
            return View();
        }

        public IActionResult CourseAbout()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string message = $"You have received a new email from your site's contact form!<br />" +
                $"Sender: {cvm.Name}<br />Email: {cvm.Email}<br />Subject: {cvm.Subject}<br />Message:\n{cvm.Message}";
            var mm = new MimeMessage();
            mm.From.Add(new MailboxAddress("Sender", _config.GetValue<string>("Credentials:Email:User")));
            mm.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));
            mm.Subject = cvm.Subject;
            mm.Body = new TextPart("HTML") { Text = message };
            mm.Priority = MessagePriority.Urgent;
            mm.ReplyTo.Add(new MailboxAddress("User", cvm.Email));
            using (var client = new SmtpClient())
            {
                client.Connect(_config.GetValue<string>("Credentials:Email:Client"), 8889);
                client.Authenticate(

                //Username
                    _config.GetValue<string>("Credentials:Email:User"),

                //Password
                    _config.GetValue<string>("Credentials:Email:Password")

                    );
                try
                {
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"There was an error processing your request. Please try again later.<br />Error Message: {ex.StackTrace}";
                    return View(cvm);
                }

            }
            return View("EmailConfirmation", cvm);
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