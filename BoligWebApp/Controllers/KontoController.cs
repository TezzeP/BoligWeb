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
    public class KontoController : Controller
    {
        HttpClientHelperApi _api = new();

        public async Task<IActionResult> Index()
        {
            List<Konto> posts = new List<Konto>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Kontos");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<Konto>>(result);
            }

            return View(posts);
        }
        
       

        public async Task<IActionResult> Details(int? id)
        {
            var konto = new Konto();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Kontos/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                konto = JsonConvert.DeserializeObject<Konto>(result);
            }

            return View(konto);

        }
        public async Task<IActionResult> CreateUser(Konto konto)
        {
            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<Konto>("api/Kontos", konto);
            

            return View(konto);

        }

        public ActionResult Create()
        {

            return View();
        }


        public async Task<IActionResult> Create(Konto konto)
        {

            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<Konto>("api/Kontos", konto);


            return View(konto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var konto = new Konto();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/Kontos/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                konto = JsonConvert.DeserializeObject<Konto>(result);
            }

            return View(konto);

        }
        //public ActionResult Edit()
        //{
        //    var posts = new Post();
        //    HttpClient client = _api.Initial();
        //    HttpResponseMessage res = client.GetAsync("api/Posts/{id}").Result;
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var result = res.Content.ReadAsStringAsync().Result;
        //        posts = JsonConvert.DeserializeObject<Post>(result);
        //    }


        //    return View();
        //}
    }
}
