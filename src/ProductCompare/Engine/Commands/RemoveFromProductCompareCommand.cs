using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using System;
using System.Threading.Tasks;
using Plugin.Bootcamp.Exercises.ProductCompare.Entities;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines.Arguments;

namespace Plugin.Bootcamp.Exercises.ProductCompare.Commands
{
    public class RemoveFromProductCompareCommand : CommerceCommand
    {
        private readonly IRemoveFromProductComparePipeline _removeFromProductComparePipeline;

        public RemoveFromProductCompareCommand(IRemoveFromProductComparePipeline removeFromProductComparePipeline, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            /* Student: Assign the Remove from Product Compare Pipeline */
        }

        public virtual async Task<ProductComparison> Process(CommerceContext commerceContext, string cartId, string sellableItemId)
        {
            using (CommandActivity.Start(commerceContext, this))
            {
                /* Student: Retrieve the current Product Comparison and then Remove the Sellable Item */
            }
        }
    }
}
