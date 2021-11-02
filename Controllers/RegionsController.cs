using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HRSystemCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HRTestSYSTEMCoreWeb.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IConfiguration _config;
        private string BaseUrl => _config["baseUrl"];

        public RegionsController(IConfiguration config)
        {
            _config = config;
        }

        // GET: Region
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                List<Region> myRegion;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRegion = JsonConvert.DeserializeObject<List<Region>>(apiResponse);
                }
                return View(myRegion);
            }

        }

        // GET: Region/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Region myRegion;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRegion = JsonConvert.DeserializeObject<Region>(apiResponse);
                }
                return View(myRegion);
            }
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionId,RegionName")] Region region)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Regions");
                    // ReSharper disable once RedundantTypeArgumentsOfMethod
                    var postTask = await httpClient.PostAsJsonAsync<Region>("regions", region);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            return View(region);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Region myRegion;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRegion = JsonConvert.DeserializeObject<Region>(apiResponse);
                }
                return View(myRegion);
            }
        }

        // POST: region/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,RegionName")] Region region)
        {
            if (id != region.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Regions/{id}");
                    var postTask = await httpClient.PutAsJsonAsync($"{BaseUrl}/Regions/{id}", region);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            return View(region);
        }

        // GET: Region/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Region myRegion;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Regions/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        myRegion = JsonConvert.DeserializeObject<Region>(apiResponse);
                    }
                    else
                    {
                        return NotFound();
                    }


                }

                return View(myRegion);
            }

        }

        // POST: Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Regions/{id}");
                    var postTask = await httpClient.DeleteAsync($"{BaseUrl}/Regions/{id}");
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
