using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Order.Model;

namespace Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //POST : /api/<OrderController>

        [HttpPost(template: "{menuItemId}")]
        public Cart Post(int menuItemID)
        {
            var cart = new Cart()
            {
                Id = 1,
                userId = 1,
                menuItemId = menuItemID,
                menuItemName = getname(menuItemID)
            };
            return cart;
        }


        private string getname(int menuItemID)
        {
            string name;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://52.141.210.58:80");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(requestUri: "api/MenuItem/" + menuItemID).Result;
                if (response.IsSuccessStatusCode)
                {
                    name = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    name = null;
                }
                return name;
            }
        }

        //PUT: /api/<OrderController>/5
        [HttpPut(template: "{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        //Delete: api/<OrderCOntroller>/5
        [HttpDelete(template: "{id}")]
        public void Delete(int id)
        {

        }
    }
    
}
