using System.Reflection;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ConfigureServiceApiBlock = Plugin.Bootcamp.Exercises.ProductCompare.Pipelines.Blocks.ConfigureServiceApiBlock;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines.Blocks;

namespace Plugin.Bootcamp.Exercises.ProductCompare
{
    public class ConfigureSitecore : IConfigureSitecore
    {
#pragma warning disable CA1822 // Mark members as static
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);
            services.RegisterAllCommands(assembly);

            /* Configure the Service Api Block
             * Add the GetProductCompare, AddToProductCompare, and RemoveFromProductCompare Pipelines */
        }
    }
}
