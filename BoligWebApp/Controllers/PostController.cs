using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoligWebApp.Helper;
using BoligWebApp.Models;
using Newtonsoft.Json;

namespace BoligWebApp.Controllers
{
    public class PostController : Controller
    {
        FilesApi _api = new();

        public async Task<IActionResult> Index()
        {
            List<Post> posts = new List<Post>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Posts");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<Post>>(result);
            }

            return View(posts);
        }


        public async Task<IActionResult> Details(int? id)
        {
            var posts = new Post();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Dokuments/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<Post>(result);
            }

            return View(posts);

        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var posts = new Post();
        //    HttpClient client = _api.Initial();
        //    HttpResponseMessage res = await client.Async("api/Dokuments/{id}");
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var result = res.Content.ReadAsStringAsync().Result;
        //        posts = JsonConvert.DeserializeObject<Post>(result);
        //    }

        //    return View(posts);

        //}
    }
}

