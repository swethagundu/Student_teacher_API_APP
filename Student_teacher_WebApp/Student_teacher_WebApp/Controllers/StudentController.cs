using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Student_teacher_WebApp.Models;
using System.Data;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;

namespace Student_teacher_WebApp.Controllers
{
    public class StudentController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:8082/");
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
       
    }
}

