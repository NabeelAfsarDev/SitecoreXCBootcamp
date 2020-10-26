using System;
using System.Threading.Tasks;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Commerce.Core;
using Plugin.Bootcamp.Exercises.ProductCompare.Entities;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines.Arguments;

namespace Plugin.Bootcamp.Exercises.ProductCompare.Commands
{
    public class AddToProductCompareCommand : CommerceCommand
    {
        private readonly IAddToProductComparePipeline _addToProductComparePipeline;

        public AddToProductCompareCommand(IAddToProductComparePipeline addToProductComparePipeline, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            /* Student: Assign the Add to Product Compare Pipeline */
        }

        public virtual async Task<ProductComparison> Process(CommerceContext commerceContext, string cartId, string catalogName, string productId, string variantId)
        {
            using (CommandActivity.Start(commerceContext, this))
            {
                /* Student: Use the Get Product Compare Command to retrieve the Product Comparison and add
                 * the new Product for the comparison return the result of your Add to Product Compare Pipeline */
             }
        }
    }
}
