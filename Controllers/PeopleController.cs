using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PeopleController : Controller
    {
        private readonly IConfiguration _config;
        private string BaseUrl => _config["baseUrl"];

        public PeopleController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            string pageSizeStr,
            int? pageNumber)
        {


            ViewData["currentPageSize"] = pageSizeStr ?? "10";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["FirstNameParm"] = sortOrder == "first_name" ? "first_name_desc" : "first_name";
            ViewData["CompanyNameParm"] = sortOrder == "comp_name" ? "comp_name_desc" : "comp_name";
            ViewData["DeptNameParm"] = sortOrder == "dept_name" ? "dept_name_desc" : "dept_name";
            ViewData["StatusNameParm"] = sortOrder == "status_name" ? "status_name_desc" : "status_name";
            ViewData["EmpNumParm"] = sortOrder == "emp_num" ? "emp_num_desc" : "emp_num";
            ViewData["DateOfBirthParm"] = sortOrder == "dob" ? "dob_desc" : "dob";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            List<Status> myStatus;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
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

            List<Department> myDepartments;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }

            List<Person> people;
            string searchstring = searchString;
            using (var httpClient = new HttpClient())
            {
                
                using (var response = await httpClient.GetAsync($"{BaseUrl}/People?searchstring={searchstring}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    people = JsonConvert.DeserializeObject<List<Person>>(apiResponse);
                }

                foreach (var pers in people)
                {
                    pers.Company = myCompanies.Find(c => c.CompanyId == pers.CompanyId);
                    pers.Department = myDepartments.Find(c => c.DepartmentId == pers.DepartmentId);
                    pers.Status = myStatus.Find(c => c.StatusId == pers.StatusId);
                }
                
            }

            if (!String.IsNullOrEmpty(searchString))
            {
              people = people.Where(p => p.Status.Status1.Contains(searchString) || p.Department.DepartmentName.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    people = people.OrderByDescending(s => s.LastName).ToList();
                    break;
                case "first_name":
                    people = people.OrderBy(s => s.FirstName).ToList();
                    break;
                case "first_name_desc":
                    people = people.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case "comp_name":
                    people = people.OrderBy(s => s.Company.CompanyName).ToList();
                    break;
                case "comp_name_desc":
                    people = people.OrderByDescending(s => s.Company.CompanyName).ToList();
                    break;
                case "dept_name":
                    people = people.OrderBy(s => s.Department.DepartmentName).ToList();
                    break;
                case "dept_name_desc":
                    people = people.OrderByDescending(s => s.Department.DepartmentName).ToList();
                    break;
                case "status_name":
                    people = people.OrderBy(s => s.Status.Status1).ToList();
                    break;
                case "status_name_desc":
                    people = people.OrderByDescending(s => s.Status.Status1).ToList();
                    break;
                case "emp_num":
                    people = people.OrderBy(s => s.EmployeeNumber).ToList();
                    break;
                case "emp_num_desc":
                    people = people.OrderByDescending(s => s.EmployeeNumber).ToList();
                    break;
                case "dob":
                    people = people.OrderBy(s => s.DateOfBirth).ToList();
                    break;
                case "dob_desc":
                    people = people.OrderByDescending(s => s.DateOfBirth).ToList();
                    break;
                default:
                    people = people.OrderBy(s => s.LastName).ToList();
                    break;
            }

            var pageSizeStrLocal = String.IsNullOrEmpty(pageSizeStr) ? "10" : pageSizeStr;
            
            int pageSize = int.Parse(pageSizeStrLocal);
            return View(await PaginatedList<Person>.CreateAsync(people.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            List<Status> myStatus;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
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

            List<Department> myDepartments;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }

            Person person;
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"{BaseUrl}/People/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    person = JsonConvert.DeserializeObject<Person>(apiResponse);
                }
                if (person == null)
                {
                    return NotFound();
                }


                person.Company = myCompanies.Find(c => c.CompanyId == person.CompanyId);
                person.Department = myDepartments.Find(c => c.DepartmentId == person.DepartmentId);
                person.Status = myStatus.Find(c => c.StatusId == person.StatusId);
                
            }

          
            return View(person);
        }

        // GET: People/Create
        public async Task <IActionResult> Create()
        {

            List<Status> myStatus;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
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

            List<Department> myDepartments;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }

            ViewData["CompanyId"] = new SelectList(myCompanies, "CompanyId", "CompanyName");
            ViewData["DepartmentId"] = new SelectList(myDepartments, "DepartmentId", "DepartmentName");
            ViewData["StatusId"] = new SelectList(myStatus, "StatusId", "Status1");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FirstName,LastName,DateOfBirth,StatusId,DepartmentId,CompanyId,EmployeeNumber,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/People");
                    // ReSharper disable once RedundantTypeArgumentsOfMethod
                    var postTask = await httpClient.PostAsJsonAsync<Person>("People", person);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }


            List<Status> myStatus;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
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

            List<Department> myDepartments;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }


            ViewData["CompanyId"] = new SelectList(myCompanies, "CompanyId", "CompanyName");
            ViewData["DepartmentId"] = new SelectList(myDepartments, "DepartmentId", "DepartmentName");
            ViewData["StatusId"] = new SelectList(myStatus, "StatusId", "Status1");
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Person person;
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"{BaseUrl}/People/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    person = JsonConvert.DeserializeObject<Person>(apiResponse);
                }
                if (person == null)
                {
                    return NotFound();
                }


            }
            
            List<Status> myStatus;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
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

            List<Department> myDepartments;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }


            ViewData["CompanyId"] = new SelectList(myCompanies, "CompanyId", "CompanyName");
            ViewData["DepartmentId"] = new SelectList(myDepartments, "DepartmentId", "DepartmentName");
            ViewData["StatusId"] = new SelectList(myStatus, "StatusId", "Status1");
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FirstName,LastName,DateOfBirth,StatusId,DepartmentId,CompanyId,EmployeeNumber,Email")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/People/{id}");
                    var postTask = await httpClient.PutAsJsonAsync($"{BaseUrl}/People/{id}", person);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }

                return RedirectToAction(nameof(Index));
            }

            List<Status> myStatus;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
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

            List<Department> myDepartments;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Departments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myDepartments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }


            ViewData["CompanyId"] = new SelectList(myCompanies, "CompanyId", "CompanyName");
            ViewData["DepartmentId"] = new SelectList(myDepartments, "DepartmentId", "DepartmentName");
            ViewData["StatusId"] = new SelectList(myStatus, "StatusId", "Status1");

            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person person;
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"{BaseUrl}/People/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    person = JsonConvert.DeserializeObject<Person>(apiResponse);
                }
                if (person == null)
                {
                    return NotFound();
                }


            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/People/{id}");
                    var postTask = await httpClient.DeleteAsync($"{BaseUrl}/People/{id}");
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
