using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/protect", (IDataProtectionProvider provider, HttpContext context) =>
{
    var protector = provider.CreateProtector("SamplePurpose");
    var input = context.Request.Query["data"];

    if (string.IsNullOrEmpty(input))
    {
        return Results.BadRequest("Please provide 'data' in the query string!");
    }

    var protectedData = protector.Protect(input!);
    return Results.Ok(new { ProtectedData = protectedData });
});

app.MapPost("/unprotect", (IDataProtectionProvider provider, HttpContext context) =>
{
    var protector = provider.CreateProtector("SamplePurpose");
    var input = context.Request.Query["data"];

    if (string.IsNullOrEmpty(input))
    {
        return Results.BadRequest("Please provide 'data' in the query string!");
    }

    try
    {
        var unprotectedData = protector.Unprotect(input!);
        return Results.Ok(new { UnprotectedData = unprotectedData });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = "Invalid or expired data!", Details = ex.Message });
    }
});

app.Run();