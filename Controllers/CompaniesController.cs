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
    public class CompaniesController : Controller
    {
        private readonly IConfiguration _config;
        private string BaseUrl => _config["baseUrl"];

        public CompaniesController(IConfiguration config)
        {
            _config = config;
        }

        // GET: Company
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
            List<Region> myRegions;
            using (var httpSubClient = new HttpClient())
            {
                using (var responseSub = await httpSubClient.GetAsync($"{BaseUrl}/Regions"))
                {
                    string apiResponseSub = await responseSub.Content.ReadAsStringAsync();
                    myRegions = JsonConvert.DeserializeObject<List<Region>>(apiResponseSub);
                }
            }

            foreach (var cmpny in myCompanies)
            {
                cmpny.Region = myRegions.Find(r=>r.RegionId == cmpny.RegionId);
            }
            
            return View(myCompanies);
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            using (var httpClient = new HttpClient())
            {
                Company myCompany;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myCompany = JsonConvert.DeserializeObject<Company>(apiResponse);
                }

                Region myRegions;
                using (var httpSubClient = new HttpClient())
                {
                    using (var responseSub = await httpSubClient.GetAsync($"{BaseUrl}/Regions/{myCompany.RegionId}"))
                    {
                        string apiResponseSub = await responseSub.Content.ReadAsStringAsync();
                        myRegions = JsonConvert.DeserializeObject<Region>(apiResponseSub);
                    }
                }

                myCompany.Region = myRegions;
                ViewBag.RegionName = myRegions.RegionName;
                return View(myCompany);
            }
        }

        // GET: Status/Create
        public async Task<IActionResult> Create()
        {
            List<Region> myRegions;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRegions = JsonConvert.DeserializeObject<List<Region>>(apiResponse);
                }
            }
            ViewBag.RegionId = new SelectList(myRegions, "RegionId", "RegionName");
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,RegionId")] Company company)
        {

            List<Region> myRegions;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRegions = JsonConvert.DeserializeObject<List<Region>>(apiResponse);
                }
            }
            ViewBag.RegionId = new SelectList(myRegions, "RegionId", "RegionName");

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Companies");
                    // ReSharper disable once RedundantTypeArgumentsOfMethod
                    var postTask = await httpClient.PostAsJsonAsync<Company>("Companies", company);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            return View(company);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            List<Region> myRegions;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRegions = JsonConvert.DeserializeObject<List<Region>>(apiResponse);
                }
            }
            
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Company myCompany;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myCompany = JsonConvert.DeserializeObject<Company>(apiResponse);
                }
                ViewBag.RegionId = new SelectList(myRegions, "RegionId", "RegionName", myCompany.RegionId);
                return View(myCompany);
            }
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName,RegionId")] Company company)
        {
            if (id != company.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Companies/{id}");
                    var postTask = await httpClient.PutAsJsonAsync($"{BaseUrl}/Companies/{id}", company);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }

            List<Region> myRegions;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRegions = JsonConvert.DeserializeObject<List<Region>>(apiResponse);
                }
            }
            ViewBag.RegionId = new SelectList(myRegions, "RegionId", "RegionName", company.RegionId);

            return View(company);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Company myCompany;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Companies/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        myCompany = JsonConvert.DeserializeObject<Company>(apiResponse);
                    }
                    else
                    {
                        return NotFound();
                    }


                }

                return View(myCompany);
            }

        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Companies/{id}");
                    var postTask = await httpClient.DeleteAsync($"{BaseUrl}/Companies/{id}");
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
