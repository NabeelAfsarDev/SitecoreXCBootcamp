using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using System;
using System.Threading.Tasks;
using Plugin.Bootcamp.Exercises.ProductCompare.Entities;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines;
using System.Diagnostics.Contracts;

namespace Plugin.Bootcamp.Exercises.ProductCompare.Commands
{
    public class GetProductCompareCommand : CommerceCommand
    {
        private readonly IGetProductComparePipeline _getProductComparePipeline;

        public GetProductCompareCommand(IGetProductComparePipeline getProductComparePipeline, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            /* Student: Assign the Get Product Compare Pipeline */
        }

        public virtual async Task<ProductComparison> Process(CommerceContext context, string cartId)
        {
            /* Student: Require the Context is not null. Return null if the cartId is null, otherwise return the
             * Product Comparison.  Log if the Product Comparison is null. */ 
        }
    }
}
