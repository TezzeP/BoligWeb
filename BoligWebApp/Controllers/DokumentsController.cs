using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BoligWebApp.Helper;
using BoligWebApp.Models;
using Newtonsoft.Json;

namespace BoligWebApp.Controllers
{
    public class DokumentsController : Controller
    {
        HttpClientHelperApi _api = new HttpClientHelperApi();

        public async Task<IActionResult> Index()
        {
            List<Dokument> dokument = new List<Dokument>();
            
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Dokuments");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dokument = JsonConvert.DeserializeObject<List<Dokument>>(result);
            }

            return View(dokument);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var dokument = new Dokument();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Dokuments/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dokument = JsonConvert.DeserializeObject< Dokument>(result);
            }

            return View(dokument);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            var dokument = new Dokument();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/Dokuments/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dokument = JsonConvert.DeserializeObject<Dokument>(result);
            }

            return View(dokument);



        }


    }
}
