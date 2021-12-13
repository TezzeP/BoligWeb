using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BoligWebApp.Helper;
using BoligWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace BoligWebApp.Controllers
{
    
    public class RoleController : Controller
    {
        FilesApi _api = new FilesApi();

        public async Task<IActionResult> Index()
        {
            List<Role> roles = new List<Role>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Roles");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                roles = JsonConvert.DeserializeObject<List<Role>>(result);
            }

            return View(roles);
        }
        public async Task<IActionResult> Create(Role role)
        {
            
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<Role>("api/Roles", role);
            if (postTask.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(role);


        }


        public async Task<IActionResult> Details(int? id)
        {
            var role = new Role();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Roles/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                role = JsonConvert.DeserializeObject<Role>(result);
            }

            return View(role);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            var role = new Role();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/Roles/{id}");


            return RedirectToAction("Index");

        }
    }
    
}
