using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using myfirstapi.Models;

namespace myfirstapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaController : ControllerBase
    {
        private readonly DataContext _context;
        public PruebaController(DataContext _context)
        {
            this._context = _context;
        }

        [HttpGet("GetArray")]
        public List<Item> GetArray()
        {
            List<Item> json = new List<Item>();
            var ev = Environment.GetEnvironmentVariable("JsonTest");

            if (!string.IsNullOrWhiteSpace(ev)) {
                Console.WriteLine("json HERE " + ev);
                json = JsonSerializer.Deserialize<List<Item>>(ev);
                if (json == null) {
                    json = this._context.Item.ToList();
                }
            }

            XDocument doc = XDocument.Load("settings.xml");
            var val = doc
                        .Descendants("appSettings")
                        .Descendants("add")
                        .Select(x => new 
                        {
                            Key = x.Attribute("key")?.Value,
                            Value = x.Attribute("value")?.Value
                        });

            Console.WriteLine("AQUI " + val.ElementAt(0).Value);
            foreach (var item in val)
            {
                Console.WriteLine(item.Value);
            }

            return json;
        }
    }
}