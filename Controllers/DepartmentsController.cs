using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HRSystemCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HRSystemCore.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IConfiguration _config;
        private string BaseUrl => _config["baseUrl"];

        public DepartmentsController(IConfiguration config)
        {
            _config = config;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
           
            List<Company> myCompanies;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myCompanies = JsonConvert.DeserializeObject<List<Company>>(apiResponse);
                }
            }
           

            using (var httpClient = new HttpClient())
            {
                List<Department> myDepartment;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartment = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
           
                foreach (var dept in myDepartment)
                {
                    dept.Company = myCompanies.Find(c => c.CompanyId == dept.CompanyId);
                }
           
                return View(myDepartment);
            }

        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Department myDepartment;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartment = JsonConvert.DeserializeObject<Department>(apiResponse);
                }

                Company myCompanies;
                using (var httpSubClient = new HttpClient())
                {
                    using (var responseSub = await httpSubClient.GetAsync($"{BaseUrl}/Companies/{myDepartment.CompanyId}"))
                    {
                        string apiResponseSub = await responseSub.Content.ReadAsStringAsync();
                        myCompanies = JsonConvert.DeserializeObject<Company>(apiResponseSub);
                    }
                }

                myDepartment.Company = myCompanies;
                return View(myDepartment);
            }
        }

        // GET: Status/Create
        public async Task<IActionResult> Create()
        {
            List<Company> myCompanies;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myCompanies = JsonConvert.DeserializeObject<List<Company>>(apiResponse);
                }
            }
            ViewBag.CompanyId = new SelectList(myCompanies, "CompanyId", "CompanyName");
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName,CompanyId")] Department department)
        {

            List<Company> myCompanies;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myCompanies = JsonConvert.DeserializeObject<List<Company>>(apiResponse);
                }
            }
            ViewBag.CompanyId = new SelectList(myCompanies, "CompanyId", "CompanyName");

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Departments");
                    // ReSharper disable once RedundantTypeArgumentsOfMethod
                    var postTask = await httpClient.PostAsJsonAsync<Department>("Departments", department);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            return View(department);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


           

            if (id == null)
            {
                return NotFound();
            }

            List<Company> myCompanies;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myCompanies = JsonConvert.DeserializeObject<List<Company>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                Department myDepartment;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartment = JsonConvert.DeserializeObject<Department>(apiResponse);
                }

                myDepartment.Company = myCompanies.Find(c => c.CompanyId == myDepartment.CompanyId);
                ViewBag.CompanyId = new SelectList(myCompanies, "CompanyId", "CompanyName", myDepartment.CompanyId);
                return View(myDepartment);
            }
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName,CompanyId")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Departments/{id}");
                    var postTask = await httpClient.PutAsJsonAsync($"{BaseUrl}/Departments/{id}", department);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }

            List<Company> myCompanies;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myCompanies = JsonConvert.DeserializeObject<List<Company>>(apiResponse);
                }
            }
            department.Company = myCompanies.Find(c => c.CompanyId == department.CompanyId);
            ViewBag.CompanyId = new SelectList(myCompanies, "CompanyId", "CompanyName", department.CompanyId);

            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Department myDepartment;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        myDepartment = JsonConvert.DeserializeObject<Department>(apiResponse);
                    }
                    else
                    {
                        return NotFound();
                    }


                }

                return View(myDepartment);
            }

        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Departments/{id}");
                    var postTask = await httpClient.DeleteAsync($"{BaseUrl}/Departments/{id}");
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
