﻿using Sitecore.Commerce.Core;
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
            Contract.Requires(arg != null);
            Contract.Requires(context != null);

            Condition.Requires(arg).IsNotNull($"{Name}: The arg can not be null");
            Condition.Requires(arg.ProductId).IsNotNull($"{Name}: The Product Id can not be null");
            Condition.Requires(arg.CompareCollection).IsNotNull($"{Name}: The Compare Collection can not be null");

            var sellableItem = await _getSellableItemPipeline.Run(BuildProductArgument(arg), context).ConfigureAwait(false);

            if (sellableItem == null)
            {
                context.Logger.LogWarning($"ProductCompare: Unable to find sellable item to add to collection: {arg.CatalogName}-{arg.ProductId}-{arg.VariantId}");
                return arg.CompareCollection;
            }

            var list = arg.CompareCollection.Products.ToList();
            if (list.Any(x => x.Id == sellableItem.Id))
            {
                context.Logger.LogDebug($"{Name}: SellableItem already exists in compare collection, no further action to take");
                return arg.CompareCollection;
            }

            var addArg = new ListEntitiesArgument(new List<string> { sellableItem.Id }, arg.CompareCollection.Name);
            await _addListEntitiesPipeline.Run(addArg, context).ConfigureAwait(false);
            return arg.CompareCollection;
        }

        private static ProductArgument BuildProductArgument(AddToProductCompareArgument arg)
        {
            return new ProductArgument
            {
                CatalogName = arg.CatalogName,
                ProductId = arg.ProductId,
                VariantId = arg.VariantId
            };
        }
    }
}

