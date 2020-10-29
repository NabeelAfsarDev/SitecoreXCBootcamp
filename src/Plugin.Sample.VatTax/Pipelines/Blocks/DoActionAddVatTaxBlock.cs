using Microsoft.Extensions.Logging;
using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Framework.Pipelines;
using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("DoActionAddDashboardEntity")]
    public class DoActionAddVatTaxBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;

        public DoActionAddVatTaxBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(context != null);

            /* STUDENT: Complete this method by adding the code to save a new Vat Tax configuration item */
            if (entityView == null || !entityView.Action.Equals("VatTax-AddVatTax", StringComparison.OrdinalIgnoreCase))
                return entityView;
            

            try
            {
                var taxTag = entityView.Properties.First(p => p.Name == "TaxTag").Value ?? string.Empty;
                var countryCode = entityView.Properties.First(p => p.Name == "CountryCode").Value ?? string.Empty;
                var taxPct = Convert.ToDecimal(entityView.Properties.First(p => p.Name == "TaxPct").Value ?? "0", CultureInfo.InvariantCulture);

                using (var vatTax = new VatTaxEntity
                {
                    Id = CommerceEntity.IdPrefix<VatTaxEntity>() + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture),
                    Name = string.Empty,
                    TaxTag = taxTag,
                    CountryCode = countryCode,
                    TaxPct = taxPct
                })
                {
                    vatTax.GetComponent<ListMembershipsComponent>().Memberships.Add(CommerceEntity.ListName<VatTaxEntity>());
                    await this._commerceCommander.PersistEntity(context.CommerceContext, vatTax).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                context.Logger.LogError($"Catalog.DoActionAddDashboardEtity.Exception: Message: {e.Message}");
                throw;
            }

            return entityView;

        }
    }
}
