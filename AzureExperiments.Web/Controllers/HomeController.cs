using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace AzureExperiments.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiConnection"]);

            var response = await client.GetAsync("api/default/getblobs");

            if (response.IsSuccessStatusCode)
            {
                var blobs =  await response.Content.ReadAsAsync<List<string>>();

                ViewBag.Message = "";

                foreach (var blob in blobs)
                {
                    ViewBag.Message += blob + Environment.NewLine;
                }
            }
            else
            {
                ViewBag.Message = "Your application description page.";
            }
            
            return View();
        }

        public async Task<ActionResult> Contact()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiConnection"]);

            var response =
                await
                    client.PostAsJsonAsync("api/default/addblob",
                        new
                        {
                            Name = Guid.NewGuid().ToString() + ".png",
                            BlobObject = System.IO.File.ReadAllBytes(Server.MapPath("~/content/assets/azurelogo.png"))
                        });

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Image Uploaded Successfully";
            }
            else
            {
                ViewBag.Message = "Image failed to upload" + (await response.Content.ReadAsStringAsync());
            }

            return View();
        }
    }
}