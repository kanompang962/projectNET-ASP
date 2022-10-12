using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectNET_ASP.Models;
using System.Text;

namespace projectNET_ASP.Controllers
{
    public class CallStudentController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        // GET: CallIssueController
        public CallStudentController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
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
        public async Task<ActionResult> Details(int id)
        {
            Student student = new Student();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:7291/api/Student/id?id=" + id))
                {
                    string strJson = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<Student>(strJson);
                }
            }
            return View(student);
        }



        // GET: CallIssueController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallIssueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                Student std = new Student();
                using (var httpClient = new HttpClient(_clientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("https://localhost:7291/api/Student", content))
                    {
                        string strJson = await response.Content.ReadAsStringAsync();
                        std = JsonConvert.DeserializeObject<Student>(strJson);
                        if (ModelState.IsValid)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(std);
            }
            catch
            {
                return View();
            }

        }

        // GET: CallIssueController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Student student = new Student();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:7291/api/Student/id?id=" + id))
                {
                    string strJson = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<Student>(strJson);
                }
            }
            return View(student);
        }

        // POST: CallIssueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Student student)
        {
            string del = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:7291/api/Student/" + id, content))
                {
                    del = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));


        }

        // GET: CallIssueController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            string del = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7291/api/Student/" + id))
                {
                    del = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CallIssueController/Delete/5
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
