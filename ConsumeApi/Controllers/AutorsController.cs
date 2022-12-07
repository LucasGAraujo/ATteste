using ConsumeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsumeApi.Controllers
{
    public class AutorsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Autor> autorlist = new List<Autor>();
            var accessToken = HttpContext.Session.GetString("JWToken");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Autors"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    autorlist = JsonConvert.DeserializeObject<List<Autor>>(apiResponse);
                }
            }
            return View(autorlist);
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

                using (var response = await httpClient.PostAsync("http://localhost:5000/api/Autors", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addAutor = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(addAutor);
        }
        public async Task<IActionResult> Update(int id)
        {
            Autor autors = new Autor();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Autors/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    autors = JsonConvert.DeserializeObject<Autor>(apiResponse);
                }
            }
            return View(autors);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Autor autor)
        {
            Autor receivedAutor = new Autor();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(autor), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:5000/api/Products/" + autor.Id, content))
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

                using (var response = await httpClient.DeleteAsync("https://localhost:5001/api/Products/" + AutorId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }

}
    
