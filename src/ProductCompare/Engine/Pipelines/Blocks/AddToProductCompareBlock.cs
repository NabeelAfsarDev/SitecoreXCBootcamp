using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.ProductCompare.Entities;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines.Arguments;
using Sitecore.Framework.Conditions;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics.Contracts;

namespace Plugin.Bootcamp.Exercises.ProductCompare.Pipelines.Blocks
{
    public class AddToProductCompareBlock : PipelineBlock<AddToProductCompareArgument, ProductComparison, CommercePipelineExecutionContext>
    {
        private readonly IGetSellableItemPipeline _getSellableItemPipeline;
        private readonly IAddListEntitiesPipeline _addListEntitiesPipeline;

        public AddToProductCompareBlock(IGetSellableItemPipeline getSellableItemPipeline, IAddListEntitiesPipeline addListEntitiesPipeline)
        {
            _getSellableItemPipeline = getSellableItemPipeline;
            _addListEntitiesPipeline = addListEntitiesPipeline;
        }

        public override async Task<ProductComparison> Run(AddToProductCompareArgument arg, CommercePipelineExecutionContext context)
        {
            /* Student: Require a contract that the arg and context are not null.
             * Require conditions that the arg and it's product id and compare collection are not null.
             * Retrieve the sellable item.  Log a warning and return the collection if the sellable
             * item is null.
             * If the sellable item already exists in the collection, log at the debug level that
             * the sellable item exists already and return the collection.
             * Add the list entities using the Add List Entities Pipeline.
             * Return the collection. */
        }
    }
}
