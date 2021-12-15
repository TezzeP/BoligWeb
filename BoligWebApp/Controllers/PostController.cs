using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BoligWebApp.Helper;
using BoligWebApp.Models;
using Newtonsoft.Json;

namespace BoligWebApp.Controllers
{
    public class PostController : Controller
    {
        HttpClientHelperApi _api = new();

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
            HttpResponseMessage res = await client.GetAsync("api/Posts/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<Post>(result);
            }

            return View(posts);

        }
        public async Task<IActionResult> Create(Post post)
        {

            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync<Post>("api/Posts", post);
            
            return View(post);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var post = new Post();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/Posts/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<Post>(result);
            }

            return View(post);

        }
        public  ActionResult Edit()
        {
            var posts = new Post();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.GetAsync("api/Posts/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<Post>(result);
            }

            
            return View();
        }

        //public async Task<IActionResult> Edit(int id)
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

