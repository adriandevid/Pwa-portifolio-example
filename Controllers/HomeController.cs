using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdrianP.Models;
using AdrianP.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WebPush;
using System;

namespace AdrianP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.applicationServerKey = configuration["VAPID:publicKey"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string client, string endpoint, string p256dh, string auth)
        {
            var PersistentStorage = new NotifyService();

            if (client == null)
            {
                return BadRequest("No Client Name parsed.");
            }
            var namesClients = await PersistentStorage.Listar();

            if (namesClients.Data.Contains(client))
            {
                return BadRequest("Client Name already used.");
            }
            var subscription = new UserNotify { userIdentity = client, endPoint = endpoint, p256dh = p256dh, auth = auth };
            await PersistentStorage.Register(subscription);

            var namesClientsNewResults = await PersistentStorage.Listar();

            return View("Notify", namesClientsNewResults.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> Notify()
        {
            var PersistentStorage = new NotifyService();
            var result = await PersistentStorage.Listar();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Notify(string message, string client)
        {
            var PersistentStorage = new NotifyService();
            if (client == null)
            {
                return BadRequest("No Client Name parsed.");
            }

            var subscriptionList = await PersistentStorage.ListarNotify();
            var subscriptionListResultSearch = subscriptionList.Data.Where(x => x.userIdentity == client).FirstOrDefault();
            
            if (subscriptionListResultSearch == null)
            {
                return BadRequest("Client was not found");
            }

            PushSubscription subscription = new PushSubscription
            {
                Endpoint = subscriptionListResultSearch.endPoint,
                P256DH = subscriptionListResultSearch.p256dh,
                Auth = subscriptionListResultSearch.auth
            };

            var subject = configuration["VAPID:subject"];
            var publicKey = configuration["VAPID:publicKey"];
            var privateKey = configuration["VAPID:privateKey"];

            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);

            var webPushClient = new WebPushClient();

            try
            {
                webPushClient.SendNotification(subscription, message, vapidDetails);
            }
            catch (Exception exception)
            {
                // Log error
            }

            var resultListOfClients = await PersistentStorage.Listar();

            return View(resultListOfClients.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
