using DeckofCardsLab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeckofCardsLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory newHttpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = newHttpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DisplayDeckOfCards()   
        {
            //https://deckofcardsapi.com/
            HttpClient httpClient = _httpClientFactory.CreateClient();
            const string createDeckOfCardsApiUrl = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";
            
            var apiResponse = httpClient.GetFromJsonAsync<DeckOfCards_Create>(createDeckOfCardsApiUrl).GetAwaiter().GetResult();
            return View(apiResponse);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class DeckOfCards_Create
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }

    }
}
