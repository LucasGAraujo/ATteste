using Microsoft.AspNetCore.Mvc;
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

                using (var response = await httpClient.GetAsync("https://localhost:5000/api/Autors"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    AutorList = JsonConvert.DeserializeObject<List<Autor>>(apiResponse);

                }
            }
            return View(AutorList);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            Autor autor = new Autor();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Autors/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    autor = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(autor);
        }
        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Autor autor)
        {
            Autor addAutor = new Autor();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(autor), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:5000/api/Autors", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addAutor = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(addAutor);
        }
        public async Task<IActionResult> Edit (int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Autor autors = new Autor();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                using (var response = await httpClient.GetAsync("https://localhost:5000/api/Autors/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    autors = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(autors);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Autor autor)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Autor receivedAutor = new Autor();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                StringContent content = new StringContent(JsonConvert
                    .SerializeObject(autor), Encoding.UTF8, "application/json");

                Console.WriteLine(content.ToString());

                using (var response = await httpClient.PutAsync("https://localhost:5000/api/Autors/" + autor.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedAutor = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(receivedAutor);
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