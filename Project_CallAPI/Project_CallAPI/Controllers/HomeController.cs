using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_CallAPI.Models;
using Project_CreateAPI.Models;
using System.Diagnostics;

namespace Project_CallAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly string link = "http://localhost:5091/api/";
        HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                List<Course> listCourses = new List<Course>();
                string odataQuery = "?$filter= contains(Title, '" + keyword + "')&$expand=Lecturer,Assignments,CourseEnrollments,Quizzes";
                HttpResponseMessage response = await _client.GetAsync(link + "Course" + odataQuery);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    listCourses = JsonConvert.DeserializeObject<List<Course>>(data);
                    ViewBag.Keyword = keyword;
                    return View(listCourses);
                }
            }
            return Redirect("/Login");
        }
    }
}