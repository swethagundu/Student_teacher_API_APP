using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STTEWebApp.Models;
using System.Net.Http.Headers;
using System.Text;

namespace STTEWebApp.Controllers
{
    public class CourseController : Controller
    {
       Uri baseAddress = new Uri("http://localhost:8082/");
       // Uri baseAddress = new Uri("https://localhost:7038/");
        private readonly HttpClient _client;
        public CourseController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        //----GET Course DATA-----//
        [HttpGet]
      
        public IActionResult Index()
        {
            List<Course> courseslist = new List<Course>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +"api/Courses").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                courseslist = JsonConvert.DeserializeObject<List<Course>>(data);
            }
            return View(courseslist);
        }


        //-----CREATE Course-----//

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course course)
        {
            try
            {
                string data = JsonConvert.SerializeObject(course);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress +
                    "api/Courses", content).Result;

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
        //---update

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {


                Course course = new Course();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Courses/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    course = JsonConvert.DeserializeObject<Course>(data);
                }
                return View(course);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }
        [HttpPost]
        public IActionResult Update(Course course)
        {

            string data = JsonConvert.SerializeObject(course);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "api/Courses/" + course.C_Id, content).Result;
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

                Course course = new Course();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Courses/" + id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    course = JsonConvert.DeserializeObject<Course>(data);
                }
                return View(course);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "api/Courses/" + id).Result;
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
