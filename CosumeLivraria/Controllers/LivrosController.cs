using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CosumeLivraria.Data;
using CosumeLivraria.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CosumeLivraria.Controllers
{
    public class LivrosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Livros> LivrosList = new List<Livros>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Livros"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    LivrosList = JsonConvert.DeserializeObject<List<Livros>>(apiResponse);

                }
            }
            return View(LivrosList);
        }
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Livros livros)
        {
            Livros addProduct = new Livros();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(livros), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Livros", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addProduct = JsonConvert.DeserializeObject<Livros>(apiResponse);
                }
            }
            return View(addProduct);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Livros product = new Livros();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Livros/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Livros>(apiResponse);
                }
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Livros livros)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Livros receivedProduct = new Livros();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                StringContent content = new StringContent(JsonConvert
                    .SerializeObject(livros), Encoding.UTF8, "application/json");

                Console.WriteLine(content.ToString());

                using (var response = await httpClient.PutAsync("https://localhost:5001/api/Livros/" + livros.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedProduct = JsonConvert.DeserializeObject<Livros>(apiResponse);
                }
            }
            return View(receivedProduct);
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