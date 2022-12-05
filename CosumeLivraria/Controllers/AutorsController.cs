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
    public class AutorsController : Controller
    {
        // GET: Autors
        public async Task<IActionResult> Index()
        {
            List<Autor> AutorList = new List<Autor>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Autors"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    AutorList = JsonConvert.DeserializeObject<List<Autor>>(apiResponse);

                }
            }
            return View(AutorList);
        }
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Autor autor)
        {
            Autor addProduct = new Autor();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(autor), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Autors", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addProduct = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(addProduct);
        }
        public async Task<IActionResult> Edit (int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Autor product = new Autor();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Autors/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Autor autor)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Autor receivedProduct = new Autor();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                StringContent content = new StringContent(JsonConvert
                    .SerializeObject(autor), Encoding.UTF8, "application/json");

                Console.WriteLine(content.ToString());

                using (var response = await httpClient.PutAsync("https://localhost:5001/api/autors/" + autor.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedProduct = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(receivedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int AutorId)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.DeleteAsync("https://localhost:5001/api/Autors/" + AutorId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }

            }
            return RedirectToAction("Index");

        }
    }
}