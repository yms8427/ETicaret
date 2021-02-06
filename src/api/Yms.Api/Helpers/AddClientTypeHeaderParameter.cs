using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Yms.Api.Helpers
{
    public class AddClientTypeHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Client-Type",
                In = ParameterLocation.Header,
                Description = "Erişim yapan client bilgisi",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                }
            });
        }
    }
}