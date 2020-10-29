using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Pipelines.Blocks
{
    [PipelineDisplayName("GetWarrantyNotesViewBlock")]
    public class GetWarrantyNotesViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");
            var catalogViewsPolicy = context.GetPolicy<KnownCatalogViewsPolicy>();

            /* STUDENT: Complete the Run method as specified in the requirements */
            var isVariationView = arg.Name.Equals(catalogViewsPolicy.Variant, StringComparison.OrdinalIgnoreCase);
            var isDetailsView = arg.Name.Equals(catalogViewsPolicy.Master, StringComparison.OrdinalIgnoreCase);
            var isWarrningDetailsView = arg.Name.Equals("WarrentyNotes", StringComparison.OrdinalIgnoreCase);
            var isConnectedView = arg.Name.Equals(catalogViewsPolicy.ConnectSellableItem, StringComparison.OrdinalIgnoreCase);
            var request = context.CommerceContext.GetObject<EntityViewArgument>();

            if (string.IsNullOrEmpty(arg.Name) ||
                !isDetailsView &&
                !isWarrningDetailsView &&
                !isVariationView &&
                !isConnectedView
                )
            {
                return Task.FromResult(arg);
            }

            if (!(request.Entity is SellableItem))
            {
                return Task.FromResult(arg);
            }

            var sellableItem = (SellableItem)request.Entity;


            var VariationId = string.Empty;
            if (isVariationView && !string.IsNullOrEmpty(arg.ItemId))
            {
                VariationId = arg.ItemId;
            }


            var isEditView = !string.IsNullOrEmpty(arg.Action) && arg.Action.Equals("WarrenyNotes.Edit", StringComparison.OrdinalIgnoreCase);


            var componentView = arg;
            if (!isEditView)
            {
                componentView = new EntityView
                {
                    Name = "WarrentyNotes",
                    DisplayName = "Warrenty Information",
                    EntityId = arg.EntityId,
                    EntityVersion = request.EntityVersion == null ? 1 : (int)request.EntityVersion,
                    ItemId = VariationId
                };

                arg.ChildViews.Add(componentView);

            }


            if (sellableItem != null && (sellableItem.HasComponent<WarrantyNotesComponent>(VariationId) || isConnectedView))
            {
                var component = sellableItem.GetComponent<WarrantyNotesComponent>(VariationId);

                componentView.Properties.Add(
                    new ViewProperty
                    {
                        Name = nameof(WarrantyNotesComponent.WarrantyInformation),
                        DisplayName = "Description",
                        RawValue = component.WarrantyInformation,
                        IsReadOnly = !isEditView,
                        IsRequired = false
                    }
                );

                componentView.Properties.Add(
                    new ViewProperty
                    {
                        Name = nameof(WarrantyNotesComponent.NumberOfYears),
                        DisplayName = "Warrenty Term (Years)",
                        RawValue = component.NumberOfYears,
                        IsReadOnly = !isEditView,
                        IsRequired = false
                    }
                );
            }

            return Task.FromResult(arg);
        }
    }
}
