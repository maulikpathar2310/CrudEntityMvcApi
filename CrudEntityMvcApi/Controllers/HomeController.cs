using CrudEntityMvcApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CrudEntityMvcApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Get()
        {
            IEnumerable<EmpReg> empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/Api/Emp");

            var consumeapi = hc.GetAsync("Emp");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<EmpReg>>();
                displaydata.Wait();

                empobj = displaydata.Result;
            }
            return View(empobj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmpReg model)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/Api/Emp");

            var insertdata = hc.PostAsJsonAsync<EmpReg>("Emp", model);
            insertdata.Wait();

            var savedata = insertdata.Result;
            if(savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Get");
            }
            return View("Create");
        }

        public ActionResult Details(int id)
        {
            EmpClass empobj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/Api/Emp");

            var consumeapi = hc.GetAsync("Emp?id=" +id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<EmpClass>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }


        public ActionResult Edit(int id)
        {
            EmpClass empobj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/Api/Emp");

            var consumeapi = hc.GetAsync("Emp?id=" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<EmpClass>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }

        [HttpPost]
        public ActionResult Edit(EmpClass  model)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/Api/Emp");

            var insertdata = hc.PutAsJsonAsync<EmpClass>("Emp", model);
            insertdata.Wait();

            var savedata = insertdata.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Get");
            }
            else
            {
                ViewBag.message = "Employee record not updated...!";
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/Api/Emp");

            var delrec = hc.DeleteAsync("Emp/"+id.ToString());
            delrec.Wait();

            var displaydata = delrec.Result;
            if(displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Get");
            }

            return View("Get");
        }
    }
}
