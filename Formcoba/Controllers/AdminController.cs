using Form.Data;
using Form.Models;
using Form.Models.Domain;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Net.Http.Headers;
using System.Net.Mail;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Form.Controllers
{
    public class AdminController : Controller
    {
        private readonly FormDBContext formDBContext;
        private readonly IHostingEnvironment hostingEnvironment;

        public AdminController(FormDBContext formDBContext, IHostingEnvironment environment)
        {
            this.formDBContext = formDBContext;
            hostingEnvironment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await formDBContext.Employees.ToListAsync();
            return View(employees);
        }
        public JsonResult List()
        {
            return Json(formDBContext.Employees.Where(x => x.confirmation == "no").ToList());
        }
        [HttpGet]
        public JsonResult GetbyID(Guid Id)
        {
            var Employee = formDBContext.Employees.ToList().Find(x => x.Id.Equals(Id));

            return Json(Employee);
        }
        [HttpPost]
        public IActionResult Update(Addemployee data)
        {
            var employee = formDBContext.Employees.ToList().Find(x => x.Id.Equals(data.Id)); ;
            if (employee != null)
            {
                employee.Email = data.Email;
                employee.Name = data.Name;
                employee.Phone = data.Phone;
                employee.Departement = data.Departement;
                employee.confirmation = data.confirmation;
                formDBContext.SaveChanges();
            }
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("Pertamina@gmail.com");
            mailMessage.To.Add( new MailAddress(data.Email));
            mailMessage.Subject = "Perpanjangan Kartu";
            mailMessage.Body = "Silahkan datang untuk perpanjangan jangkat waktu kartu";
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("albert.samuel.work@gmail.com", "ajkcbktsiqoexslr"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);
            return Json(employee);
        }

      
        public IActionResult Download(Addemployee data)
        {
            var employee = formDBContext.Employees.ToList().Find(x => x.Id.Equals(data.Id));
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Images", employee.file);

            byte[] fileBytes = System.IO.File.ReadAllBytes(uploads);
            string base64 = Convert.ToBase64String(fileBytes, 0, fileBytes.Length);

            return Content(base64);

        }
        public JsonResult Delete(Guid Id)
        {
            var Employee = formDBContext.Employees.ToList().Find(x => x.Id.Equals(Id));

            return Json(Employee);
        }
        public IActionResult Delete2(Addemployee data)
        {
            var employee = formDBContext.Employees.ToList().Find(x => x.Id.Equals(data.Id)); ;
            if (employee != null)
            {
                employee.alasan = data.alasan;
                employee.confirmation = data.confirmation;
                formDBContext.SaveChanges();
            }
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("Pertamina@gmail.com");
            mailMessage.To.Add(new MailAddress(data.Email));
            mailMessage.Subject = "Penolakan Perpanjangan Kartu";
            mailMessage.Body = "Mohon maaf permintaan ditolak dikarenakan " + data.alasan;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("albert.samuel.work@gmail.com", "ajkcbktsiqoexslr"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);
            return Json(employee);
        }

        public async Task<IActionResult> History()
        {
            var employees = await formDBContext.Employees.ToListAsync();
            return View(employees);
        }


    }
}
