using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ConsumeApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsumeApi.Controllers
{
    public class LivrosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Livros> livroslist = new List<Livros>();
            var accessToken = HttpContext.Session.GetString("JWToken");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Livros"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    livroslist = JsonConvert.DeserializeObject<List<Livros>>(apiResponse);
                }
            }
            return View(livroslist);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            Livros livro = new Livros();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Livros/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    livro = JsonConvert.DeserializeObject<Livros>(apiResponse);
                }
            }
            return View(livro);
        }
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Livros livros)
        {
            Livros addLivro = new Livros();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(livros), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5000/api/Livros", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addLivro = JsonConvert.DeserializeObject<Livros>(apiResponse);
                }
            }
            return View(addLivro);
        }
        public async Task<IActionResult> Update(int id)
        {
            Livros livrosr = new Livros();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Livros/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    livrosr = JsonConvert.DeserializeObject<Livros>(apiResponse);
                }
            }
            return View(livrosr);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Livros livros)
        {
            Livros receivedlivros  = new Livros();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(livros), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:5000/api/Livros/" + livros.Id, content))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedlivros = JsonConvert.DeserializeObject<Livros>(apiResponse);
                }
            }
            return View(receivedlivros);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int LivrosId)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.DeleteAsync("https://localhost:5001/api/Livros/" + LivrosId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
