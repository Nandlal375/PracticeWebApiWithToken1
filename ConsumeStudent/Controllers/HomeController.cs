using ConsumeStudent.Models;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsumeStudent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           List<Student> students = new List<Student>();    
           HttpClient client = new HttpClient();
           client.BaseAddress = new Uri("http://localhost:5115/");
           HttpResponseMessage response =  await client.GetAsync("api/Student");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<Student>>(results);
            }
            return View(students);
        }
        [HttpGet]
        public ActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5115/");
            var response = await client.PostAsJsonAsync("api/Student", student);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student student = new Student();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5115/");
            var response = await client.GetAsync($"api/Student/{id}");
            if (response.IsSuccessStatusCode) 
            {
                var results = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<Student>(results);
                return View(student);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Student student) 
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5115/");
            var result = await client.PutAsJsonAsync($"api/Student/{student.Id}", student);
            if (result.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public async Task<ActionResult> Delete(int id)
        { 
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5115/");
            HttpResponseMessage message = await httpClient.DeleteAsync($"api/Student/{id}");
            if(message.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
