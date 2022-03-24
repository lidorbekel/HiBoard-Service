using HiBoard.Service.Resources;
using JsonApiDotNetCore.Configuration;
using Newtonsoft.Json.Converters;

namespace HiBoard.Service.Configuration;

public static class JsonApiConfiguration
{
    public static void Options(JsonApiOptions options, IHostEnvironment environment)
    {
        options.IncludeExceptionStackTraceInErrors = environment.IsDevelopment();
        options.Namespace = null;
        options.DefaultPageSize = new PageSize(5);
        options.IncludeTotalResourceCount = true;
        options.LoadDatabaseValues = true;
        options.ValidateModelState = true;
        options.EnableResourceHooks = true;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    }

    public static void Discovery(ServiceDiscoveryFacade discovery)
    {
        discovery.AddAssembly(typeof(ContactResource).Assembly);
    }

    public static void Resources(ResourceGraphBuilder resources)
    {
        // todo customize or remove.
        // note: all resources in current assembly are added using discovery
    }
}
