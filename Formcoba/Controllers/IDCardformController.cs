using Form.Data;
using Form.Models;
using Form.Models.Domain;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Web;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Form.Controllers
{
    public class IDCardformController : Controller
    {
        private readonly FormDBContext formDBContext;
        private readonly IHostingEnvironment hostingEnvironment;

        public IDCardformController(FormDBContext formDBContext, IHostingEnvironment environment)
        {
            this.formDBContext = formDBContext;
            hostingEnvironment = environment;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Addemployee addemployeeRequest, IFormFile file)
        {

            

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addemployeeRequest.Name,
                Email = addemployeeRequest.Email,
                Phone = addemployeeRequest.Phone,
                Departement = addemployeeRequest.Departement,
                confirmation = addemployeeRequest.confirmation,
                file = file.FileName,




            };
            var uniqueFileName = file.FileName;
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Images", file.FileName);
            var stream = new FileStream(uploads, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();




            await formDBContext.Employees.AddAsync(employee);
            await formDBContext.SaveChangesAsync();
            return RedirectToAction("Add");



        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);


        }

    }
}
