using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace NavigationModule.API.Infrastructure.Providers
{
    public class ExternalControllerFeatureProvider : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            // Get all referenced assemblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Search for controllers in the referenced assemblies
            foreach (var assembly in assemblies)
            {
                var controllerTypes = assembly.GetExportedTypes()
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract);

                foreach (var controllerType in controllerTypes)
                {
                    var controllerModel = new ControllerModel(controllerType.GetTypeInfo(), new List<object>());
                    application.Controllers.Add(controllerModel);
                }
            }
        }
    }
}