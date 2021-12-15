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
        HttpClientHelperApi _api = new HttpClientHelperApi();
        private int idToUpdate;
        

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
             
            await client.PostAsJsonAsync<Role>("api/Roles", role);
            
           
            return View(role);


        }


        public async Task<IActionResult> Details(int id)
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

        public async Task<IActionResult> Delete(int id)
        {
            var role = new Role();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Roles/{id}");


            return RedirectToAction("Index");

        }



        public ActionResult  RedirectUpdate(int id)
        {
            idToUpdate = id;            
            return RedirectToAction("Update");
            //HttpClient client = _api.Initial();
            //HttpResponseMessage putTask = await client.PutAsJsonAsync<Role>("api/Roles/{id}",role);
            //if (putTask.IsSuccessStatusCode)
            //{
            //    var result = putTask.Content.ReadAsStringAsync().Result;
            //    role = JsonConvert.DeserializeObject<Role>(result);

            //}

            //return View(role);

        }
        public async Task<IActionResult> Update(string roleName)
        {
            Role role = new Role();
            role.Id = idToUpdate;
            role.RoleName = roleName;
            //HttpClient client = _api.Initial();
            //HttpResponseMessage res = await client.GetAsync("api/Roles/{id}");
            //if (res.IsSuccessStatusCode)
            //{
            //    var result = res.Content.().Result;
            //    role = JsonConvert.DeserializeObject<Role>(result);
            //}


            HttpClient client = _api.Initial();
            HttpResponseMessage putTask = await client.PutAsJsonAsync<Role>($"api/Roles/{idToUpdate}", role);
            if (putTask.IsSuccessStatusCode)
            {
                var result = putTask.Content.ReadAsStringAsync().Result;
                role = JsonConvert.DeserializeObject<Role>(result);
            }
            return View();
        }
        

    }
}
