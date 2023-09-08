using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STTEWebApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace STTEWebApp.Controllers
{
    public class TeacherController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:8082/");
        //Uri baseAddress = new Uri("https://localhost:7038/");
        private readonly HttpClient _client;
        public TeacherController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        //----GET TEACHER DATA-----//
        [HttpGet]
        public IActionResult Index()
        {
            List<Teacher> teacherslist = new List<Teacher>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Teachers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                teacherslist = JsonConvert.DeserializeObject<List<Teacher>>(data);
            }
            return View(teacherslist);
        }


        //-----CREATE TEACHER-----//

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Teacher teacher)
        {
            try
            {
                string data = JsonConvert.SerializeObject(teacher);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress +
                    "api/Teachers", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Student Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }


        //---------Update TEACHER-----//

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {


                Teacher teacher = new Teacher();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Teachers/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    teacher = JsonConvert.DeserializeObject<Teacher>(data);
                }
                return View(teacher);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }
        [HttpPost]
        public IActionResult Update(Teacher teacher)
        {

            string data = JsonConvert.SerializeObject(teacher);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "api/Teachers/" + teacher.T_ID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        //-------------Delete-------------------

        [HttpGet]

        public IActionResult Delete(int id)
        {
            try
            {

                Teacher teacher = new Teacher();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Teachers/" + id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    teacher = JsonConvert.DeserializeObject<Teacher>(data);
                }
                return View(teacher);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }


        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "api/Teachers/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }
    }
}
