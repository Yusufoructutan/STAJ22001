using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class SwaggerIgnoreFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var ignoredProperties = context.MethodInfo
            .GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(SwaggerIgnoreAttribute), true).Any()))
            .Select(prop => prop.Name);

        foreach (var property in ignoredProperties)
        {
            operation.Parameters = operation.Parameters
                .Where(p => p.Name != property)
                .ToList();
        }
    }
}
