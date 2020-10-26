using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Sitecore.Commerce.Core;
using Plugin.Bootcamp.Exercises.ProductCompare.Commands;

namespace Plugin.Bootcamp.Exercises.ProductCompare.Controllers
{
    [EnableQuery]
    [Route("api/Compare")]
    public class CompareController : CommerceController
    {
        public CompareController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        [EnableQuery]
        [HttpGet]
        [Route("(Id={id})")]
        public async Task<IActionResult> Get(string id)
        {
            /* Student: Return a NotFound if the ModelState is not valid or id is null.
             * Return a BadRequestObjectResult for value if it does not contain any of the properties expected.
             * Return the result of the GetProductCompareCommand otherwise return NotFound*/
        }
    }
}
