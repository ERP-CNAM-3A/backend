using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("/")]
    public class MiddleOfficeController : ControllerBase
    {
        public class MeuchEndpointInput
        {
            public string Key { get; set; }
            public string Endpoint { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public string RouteFormat { get; set; }
            public List<string> QueryParams { get; set; }
            public string Body { get; set; }
        }


        [HttpGet("meuch_map")]
        public IActionResult GetMeuch()
        {
            List<MeuchEndpointInput> endpoints = new List<MeuchEndpointInput>()
            {
                new MeuchEndpointInput()
                {
                    Key = "PROJECTS_GET_ALL",
                    Endpoint = "/Project/GetAllProjects",
                    Description = "Retourne tous les projets",
                    Type = "GET",
                    RouteFormat = null,
                    QueryParams = null,
                    Body = null
                },
                new MeuchEndpointInput()
                {
                    Key = "PROJECTS_GET_BY_ID",
                    Endpoint = "/Project/GetProjectById/{id}",
                    Description = "Retourne un projet par son ID",
                    Type = "GET",
                    RouteFormat = "/id",
                    QueryParams = null,
                    Body = null
                },
                new MeuchEndpointInput()
                {
                    Key = "RESSOURCES_GET_ALL",
                    Endpoint = "/Ressource/GetAllRessources",
                    Description = "Retourne toutes les ressources",
                    Type = "GET",
                    RouteFormat = null,
                    QueryParams = null,
                    Body = null
                },
                new MeuchEndpointInput()
                {
                    Key = "RESSOURCES_GET_BY_ID",
                    Endpoint = "/Ressource/GetRessourceById/{id}",
                    Description = "Retourne une ressource par son ID",
                    Type = "GET",
                    RouteFormat = "/id",
                    QueryParams = null,
                    Body = null
                },
                new MeuchEndpointInput()
                {
                    Key = "SALES_GET_ALL",
                    Endpoint = "/Sale/GetAllSales",
                    Description = "Retourne toutes les ventes",
                    Type = "GET",
                    RouteFormat = null,
                    QueryParams = null,
                    Body = null
                },
                new MeuchEndpointInput()
                {
                    Key = "SALES_GET_BY_ID",
                    Endpoint = "/Sale/GetSaleById/{id}",
                    Description = "Retourne une vente par son ID",
                    Type = "GET",
                    RouteFormat = "/id",
                    QueryParams = null,
                    Body = null
                }
            };

            return Ok(endpoints);
        }
    }
}