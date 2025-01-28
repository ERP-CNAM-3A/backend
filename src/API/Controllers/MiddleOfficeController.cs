using Microsoft.AspNetCore.Mvc;

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
                    Key = "PR_PROJECTS_GET_ALL",
                    Endpoint = "/Project/GetAllProjects",
                    Description = "Retourne tous les projets",
                    Type = "GET",
                    RouteFormat = null,
                    QueryParams = null,
                    Body = null
                },
                new MeuchEndpointInput()
                {
                    Key = "PR_PROJECTS_GET_BY_ID",
                    Endpoint = "/Project/GetProjectById",
                    Description = "Retourne un projet par son ID",
                    Type = "GET",
                    RouteFormat = "/id",
                    QueryParams = ["ID_projet"],
                    Body = null
                }
            };

            return Ok(endpoints);
        }
    }
}