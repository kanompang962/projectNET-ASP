using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectNET_ASP.Models;

namespace projectNET_ASP.Controllers
{
    public class CallStudentController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        public CallStudentController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        // GET: CallStudentController
        public async Task<ActionResult> Index()
        {
            var student = await GetStudent();
            return View(student);
        }

        [HttpGet]
        public async Task<List<Student>> GetStudent()
        {
            List<Student> studentList = new List<Student>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:7291/api/Student"))
                {
                    string strJson = await response.Content.ReadAsStringAsync();
                    studentList = JsonConvert.DeserializeObject<List<Student>>(strJson);
                }
            }
            return studentList;
        }

        // GET: CallStudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CallStudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallStudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallStudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CallStudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallStudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CallStudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
