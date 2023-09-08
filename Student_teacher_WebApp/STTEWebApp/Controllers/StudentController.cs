using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STTEWebApp.Models;
using System.Data;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace STTEWebApp.Controllers
{
    public class StudentController : Controller
    {





        //------------------------------------------------------------
        Uri baseAddress = new Uri("http://localhost:8082/");
      // Uri baseAddress = new Uri("https://localhost:7038/");
        private readonly HttpClient _client;
        public StudentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        //----GET STUDENT DATA-----//
        [HttpGet]
        public IActionResult Student()
        {
            List<Student> studentsList = new List<Student>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Students").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                studentsList = JsonConvert.DeserializeObject<List<Student>>(data);
            }
            return View(studentsList);
        }

        //-----CREATE STUDENT-----//

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            try
            {
                string data = JsonConvert.SerializeObject(student);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress +
                    "api/Students", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Student Created";
                    return RedirectToAction("Student");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }

        //-----------------------original--------------------------
        //---------Update Student-----//
        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {


                Student student = new Student();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Students/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    student = JsonConvert.DeserializeObject<Student>(data);
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }
        [HttpPost]
        public IActionResult Update(Student student)
        {

            string data = JsonConvert.SerializeObject(student);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "api/Students/" + student.S_ID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Student");
            }

            return View();
        }

        //-------------Delete-------------------

        [HttpGet]

        public IActionResult Delete(int id)
        {
            try
            {

                Student student = new Student();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Students/" + id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    student = JsonConvert.DeserializeObject<Student>(data);
                }
                return View(student);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "api/Students/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Student");
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


