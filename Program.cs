using MicroService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Web;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://sts.windows.net/315cf027-e2aa-46c6-bdd1-b36500db3b42/";
        options.Audience = "api://c2ece34e-00aa-4968-992e-3e6cee6913f0";
    });
*/


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(
        _ => {
        },
        identityOptions => {
            identityOptions.TenantId = "315cf027-e2aa-46c6-bdd1-b36500db3b42";
            identityOptions.ClientId = "8a812dbe-43de-4f1d-8a93-dc47fd57ecb4";
            identityOptions.Instance = "https://login.microsoftonline.com/";
        });



builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});


var app = builder.Build();


app.UseCors("AllowLocalhost4200");



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapMethods("/cases/cleanup", new[] { "POST" }, Handler.EDRSCaseCleanup)
    .WithTags("Cases")
    .WithName("CleanupCases")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status429TooManyRequests)
    ;


app.MapMethods("/cases/adduser", httpMethods: new[] { "POST" }, Handler.EdrsAdduser)
    .WithTags("CasesUser")
    .WithName("adduser")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status429TooManyRequests)
    ;
// .WithOpenApi();

app.Run();
