using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
//using System;

namespace AnswersAPI_MitziVargasMejia.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        //Estamos creando un atributo que luego iusaremos como decoracion para nuestros
        //controllers e inyectarle el mecanismo de seguridad por Apikey para darle una 
        //capa de seguridad simple end points

        private const string NombreDelApikey = "Apikey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(NombreDelApikey, out var ApiSalida))
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "No se ha incluido una API Key"
                };

                return; 
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apikey = appSettings.GetValue<string>(NombreDelApikey);

            if (!apikey.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401, Content = "La API key suministrada no es la correcta"

                };
                return;
            }
            await next();

        }
        

    }
}
