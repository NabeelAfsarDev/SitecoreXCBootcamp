using System;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.OData;
using Plugin.Bootcamp.Exercises.ProductCompare.Commands;

namespace Plugin.Bootcamp.Exercises.ProductCompare.Controllers
{
    public class CommandsController : CommerceController
    {
        public CommandsController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpPut]
        [Route("AddProductToComparison()")]
        public async Task<IActionResult> AddProductToComparison([FromBody] ODataActionParameters value)
        {
            /* Student: Return a BadRequestObjectResult for the ModelState if the ModelState or value is null.
             * Return a BadRequestObjectResult for value if it does not contain any of the properties expected.
             * Validate these properties are not empty, otherwise return a BadRequestObjectResult for value
             * Return the result of the AddToProductCompareCommand */
        }


        [HttpDelete]
        [Route("RemoveProductFromComparison()")]
        public async Task<IActionResult> RemoveProductFromComparison([FromBody] ODataActionParameters value)
        {
            /* Student: Return a BadRequestObjectResult for the ModelState if the ModelState or value is null.
             * Return a BadRequestObjectResult for value if it does not contain any of the properties expected.
             * Validate these properties are not empty, otherwise return a BadRequestObjectResult for value
             * Return the result of the RemoveFromProductCompareCommand */
        }
    }
}
