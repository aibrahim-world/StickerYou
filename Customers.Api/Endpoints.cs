using Customers.Application.DTOs;
using Customers.Application.Interfaces;


namespace Customers.Api
{
    public static class Endpoints
    {
        public static void MapEndPoints(this WebApplication app)
        {
            app.MapGet("/customer/welcome", async (HttpContext httpContext, ICustomerService customerService) =>
            {
                await httpContext.Response.WriteAsync("Welcome!");
            });
            app.MapGet("/customer", async (HttpContext httpContext, ICustomerService customerService) =>
            {
                var name = httpContext.Request.Query["name"].ToString();
                var result = await customerService.GetCustomerByName(name);
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(result);
            });

            app.MapPost("/customer/upload", async (HttpContext httpContext, ICustomerService customerService) =>
            {
                var customers = await httpContext.Request.ReadFromJsonAsync<List<CustomerDto>>();
                if (customers != null && customers.Count() > 0)
                {
                    var result = await customerService.UploadCustomerAsync(customers);
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsJsonAsync(result);
                    await httpContext.Response.CompleteAsync();
                    return Results.Ok();
                }
                else
                {
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsJsonAsync("No data to upload");
                    return Results.BadRequest("No data to upload");
                }

            });
            app.MapPut("/customer/{id:int}", async (HttpContext httpContext, ICustomerService customerService, int id) =>
            {
                var nameDto = await httpContext.Request.ReadFromJsonAsync<UpdateNameDto>();
                if (nameDto != null && !string.IsNullOrEmpty(nameDto.Name))
                {
                    var result = await customerService.UpdateCustomerAsync(id, nameDto.Name);
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsJsonAsync(result);
                    await httpContext.Response.CompleteAsync();
                    return Results.Ok();
                }
                else
                {
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsJsonAsync("No data to update");
                    return Results.BadRequest("No data to update");
                }

            });
            app.MapPut("/customer", async (HttpContext httpContext, ICustomerService customerService) =>
            {
                var customer = await httpContext.Request.ReadFromJsonAsync<CustomerDto>();
                if (customer != null)
                {
                    var result = await customerService.UpdateCustomerAsync(customer);
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsJsonAsync(result);
                    await httpContext.Response.CompleteAsync();
                    return Results.Ok();
                }
                else
                {
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsJsonAsync("No data to update");
                    return Results.BadRequest("No data to update");
                }

            });

            app.MapDelete("/customer/{id:int}", async (HttpContext httpContext, ICustomerService customerService, int id) =>
            {
                var result = await customerService.DeleteCustomerAsync(id);
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(result);
                await httpContext.Response.CompleteAsync();
                return Results.Ok();
            });
        }
    }
}
