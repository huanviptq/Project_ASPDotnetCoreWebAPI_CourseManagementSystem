using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_CreateAPI.Models;

namespace Project_CallAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly string link = "http://localhost:5091/api/";
        HttpClient _client;

        public LoginController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return View();
            }
            return Redirect("/Home");
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            User u = new User();
            string odataQuery = "$select=UserId,Email,Username,Password,Role&$expand=Courses,CourseEnrollments,QuizAttendances,StudentAssignments";
            HttpResponseMessage response = await _client.PostAsJsonAsync(link + "User/login?" + odataQuery, user);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                u = JsonConvert.DeserializeObject<User>(data);

                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(u));

                if (string.Equals(u.Role, "Lecturer", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Session.SetString("lecturer", JsonConvert.SerializeObject(u));
                }
                ViewBag.LoginSuccess = true;
                return Redirect("/Home");
            }
            else
            {
                ViewBag.Error = "Invalid email or password!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Login");
        }
    }
}
