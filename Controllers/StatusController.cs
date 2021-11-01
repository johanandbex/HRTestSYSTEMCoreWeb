using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HRSystemCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HRSystemCore.Controllers
{
    public class StatusController : Controller
    {
        private readonly IConfiguration _config;
        private string BaseUrl => _config["baseUrl"];

        public StatusController(IConfiguration config)
        {
            _config = config;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                List<Status> myStatus;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
                }
                return View(myStatus);
            }
      
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                Status myStatus;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<Status>(apiResponse);
                }
                return View(myStatus);
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
        public async Task<IActionResult> Create([Bind("StatusId,Status1")] Status status)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Status");
                    // ReSharper disable once RedundantTypeArgumentsOfMethod
                    var postTask = await httpClient.PostAsJsonAsync<Status>("status", status);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                
                }
            }
            return View(status);
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
                Status myStatus;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myStatus = JsonConvert.DeserializeObject<Status>(apiResponse);
                }
                return View(myStatus);
            }
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,Status1")] Status status)
        {
            if (id != status.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Status/{id}");
                    var postTask = await httpClient.PutAsJsonAsync($"{BaseUrl}/Status/{id}", status);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            using (var httpClient = new HttpClient())
            {
                Status myStatus;
                using (var response = await httpClient.GetAsync($"{BaseUrl}/Status/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        myStatus = JsonConvert.DeserializeObject<Status>(apiResponse);
                    }
                    else
                    {
                        return NotFound();
                    }
                    

                }

                return View(myStatus);
            }

        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{BaseUrl}/Status/{id}");
                    var postTask = await httpClient.DeleteAsync($"{BaseUrl}/Status/{id}");
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

        /*private bool StatusExists(int id)
        {
            return _context.Status.Any(e => e.StatusId == id);
        }
        */
    }
}
