using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("GetVatTaxDashboardViewBlock")]
    public class GetVatTaxDashboardViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;
        
        public GetVatTaxDashboardViewBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }
        
        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(entityView != null);
            Contract.Requires(context != null);
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            /* STUDENT: Complete the body of the Run method. You should handle the 
             * entity view for both a new and existing entity. */
            if (entityView.Name != "VatTaxDashboard")
            {
                return entityView;
            }

            var isEditView = !string.IsNullOrEmpty(entityView.Action) &&
                entityView.Action.Equals("VatTax-AddVatTax", StringComparison.OrdinalIgnoreCase);

            if (!isEditView)
            {
                var vatTaxTable = entityView;
                entityView.UiHint = "Table";
                entityView.Icon = "cubes";
                entityView.ItemId = string.Empty;


                var vatTaxEntities = await this._commerceCommander.Command<ListCommander>()
                    .GetListItems<VatTaxEntity>(context.CommerceContext,
                    CommerceEntity.ListName<VatTaxEntity>(), 0, 99).ConfigureAwait(false);

                foreach (var vatTaxEntitiy in vatTaxEntities)
                {
                    vatTaxTable.ChildViews.Add(
                        new EntityView
                        {
                            ItemId = vatTaxEntitiy.Id,
                            Icon = "cubes",
                            Properties = new List<ViewProperty>
                            {
                                new ViewProperty{ Name = "TaxTag", DisplayName="Tag", RawValue= vatTaxEntitiy.TaxTag},
                                new ViewProperty{ Name = "CountryCode", DisplayName="Country Code", RawValue= vatTaxEntitiy.CountryCode},
                                new ViewProperty{ Name = "TaxPct", DisplayName="Tax Rate", RawValue= vatTaxEntitiy.TaxPct}

                            }
                        }
                    );
                }

            }
            else
            {
                entityView.Properties.Add(
                        new ViewProperty
                        {
                            Name = "TaxTag",
                            IsHidden = false,
                            IsReadOnly = true,
                            RawValue = string.Empty
                        }
                    );

                entityView.Properties.Add(
                        new ViewProperty
                        {
                            Name = "Country Code",
                            IsHidden = false,
                            IsReadOnly = true,
                            RawValue = string.Empty
                        }
                    );

                entityView.Properties.Add(
                        new ViewProperty
                        {
                            Name = "TaxPct",
                            IsHidden = false,
                            IsReadOnly = true,
                            RawValue = 0
                        }
                    );
            }
            return entityView;
        }
    }
}
