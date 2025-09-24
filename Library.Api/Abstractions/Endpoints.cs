using System.Reflection;

namespace Library.Api.Abstractions;

public interface IEndpointModule
{
    void MapEndpoints(IEndpointRouteBuilder app);
}

public static class EndpointsExtensions
{
    public static void MapDiscoveredEndpoints(this IEndpointRouteBuilder app, params Assembly[] assemblies)
    {
        var scan = assemblies.Length == 0 ? [Assembly.GetExecutingAssembly()] : assemblies;

        var modules = scan.SelectMany(a => a.ExportedTypes)
            .Where(t => t is { IsAbstract: false, IsInterface: false } && typeof(IEndpointModule).IsAssignableFrom(t))
            .Select(Activator.CreateInstance)
            .Cast<IEndpointModule>();

        foreach (var m in modules)
            m.MapEndpoints(app);
    }
}