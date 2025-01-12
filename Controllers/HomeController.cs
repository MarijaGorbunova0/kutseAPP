using Kutse_App.Models;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Configuration;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 10 ? "Tere hommikust!" :
                                (hour < 18 ? "Tere päevast!" : "Tere õhtust!");

            string holidayMessage = "";
            string holidayImage = "../Images/smile.png";

            int month = DateTime.Now.Month;
            switch (month)
            {
                case 12:
                    holidayMessage = "Häid jõule ja head uut aastat!";
                    holidayImage = "../Images/joulu.jpg";
                    break;
                case 1:
                    holidayMessage = "Head uut aastat!";
                    holidayImage = "../Images/uutast.jpg";
                    break;
                case 2:
                    holidayMessage = "Armastuse päev!";
                    holidayImage = "../Images/soberpaev.jpg";
                    break;
                case 5:
                    holidayMessage = "Head emadepäeva!";
                    holidayImage = "../Images/ema.jpg";
                    break;
                default:
                    holidayMessage = "Meil on suur rõõm sind tervitada!";
                    break;
            }

            ViewBag.Message = holidayMessage;
            ViewBag.HolidayImage = holidayImage;

            return View();
        }

        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            if (ModelState.IsValid)
            {
                E_Mail(guest);
                return View("Thanks", guest);
            }
            else
            {
                ViewBag.Message = "Palun täitke kõik vajalikud väljad.";
                return View();
            }
        }

        public void E_Mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "mariarapirovna@gmail.com";
                WebMail.Password = "dxob enlh rjpj cnyk";
                WebMail.From = "mariarapirovna@gmail.com";
                WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " Vastus " + ((guest.WillAttend ?? false) ?
                    "tuleb poele " : "ei tule poele"));
                ViewBag.Message = "Kiri on saatnud!";

                ViewBag.Message = "Kiri on saadetud!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Mul on kuhju! Ei saa kirja saada!!! " + ex.Message;
            }
        }

        [HttpPost]
        public ActionResult SendReminder(Guest guest)
        {
            if (guest.WillAttend == true)
            {
                try
                {
                    string filePath = Server.MapPath("~/Files/Reminder.pdf");
                    if (System.IO.File.Exists(filePath))
                    {
                        string subject = "Meeldetuletus kutse kohta";
                        string body = "See on meeldetuletus teie kutse kohta.";

                        E_MailWithAttachment(guest, filePath, subject, body);
                        ViewBag.Message = "Meeldetuletus on saadetud edukalt!";
                    }
                    else
                    {
                        ViewBag.Message = "Faili ei leitud!";
                    }
                }
                catch (Exception)
                {
                    ViewBag.Message = "Viga meeldetuletuse saatmisel!";
                }
            }
            else
            {
                ViewBag.Message = "Kuna te olete öelnud, et ei tule, ei saa sa meeldetuletust saata!";
            }

            return View("ReminderConfirmation");
        }

        private void E_MailWithAttachment(Guest guest, string filePath, string subject, string body)
        {
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "mariarapirovna@gmail.com";
            WebMail.Password = "dxob enlh rjpj cnyk";
            WebMail.From = "mariarapirovna@gmail.com";
            WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " Vastus " + ((guest.WillAttend ?? false) ?
                "tuleb poele " : "ei tule poele"));
            ViewBag.Message = "Kiri on saatnud!";

        }
    }
}
