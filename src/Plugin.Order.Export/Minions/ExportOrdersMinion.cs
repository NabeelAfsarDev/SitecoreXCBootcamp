using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plugin.Bootcamp.Exercises.Order.Export.Pipelines;
using Plugin.Bootcamp.Exercises.Order.Export.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using System;
using System.Threading.Tasks;
using System.Linq;
using XC = Sitecore.Commerce.Plugin.Orders;

namespace Plugin.Bootcamp.Exercises.Order.Export.Minions
{
    public class ExportOrdersMinion : Minion
    {
        protected IExportOrderMinionPipeline ExportOrderPipeline { get; set; }

        public override void Initialize(IServiceProvider serviceProvider,
            MinionPolicy policy,
            CommerceContext globalContext)
        {
            base.Initialize(serviceProvider, policy, globalContext);
            ExportOrderPipeline = serviceProvider.GetService<IExportOrderMinionPipeline>();
        }

        protected override async Task<MinionRunResultsModel> Execute()
        {
            MinionRunResultsModel runResults = new MinionRunResultsModel();
            /* STUDENT: Complete the body of this method. You need to pull from an appropriate list
             * and then execute an appropriate pipeline. */
            long listCount = await this.GetListCount(this.Policy.ListsToWatch[0]).ConfigureAwait(false);

            this.Logger.LogInformation($"{(object)this.Name}-Review List { (object)this.Policy.ListsToWatch[0] }: Count:{(object)listCount}");

            FindEntitiesInListArgument entitiesInListArgument = await this.GetListIds<XC.Order>(this.Policy.ListsToWatch[0], int.MaxValue, 0).ConfigureAwait(false);

            using (var commerceContext = new CommerceContext(this.Logger, this.MinionContext.TelemetryClient, (IGetLocalizableMessagePipeline)null))
            {
                foreach (var orderUniqueId in entitiesInListArgument.EntityReferences.Select<ListEntityReference, Guid>((Func<ListEntityReference, Guid>)(e => e.EntityUniqueId)))
                {
                    this.Logger.LogDebug($"{(object)this.Name}-Reviewing Pending Order: {(object)orderUniqueId.ToString()}", Array.Empty<object>());

                    var ordersMinionArgument = new ExportOrderArgument(orderUniqueId.ToString());

                    commerceContext.Environment = this.Environment;

                    var executionContextOptions = new CommercePipelineExecutionContextOptions(commerceContext);

                    await ExportOrderPipeline.Run(ordersMinionArgument, executionContextOptions).ConfigureAwait(false);
                }
            }

            return runResults;
        }


    }
}
