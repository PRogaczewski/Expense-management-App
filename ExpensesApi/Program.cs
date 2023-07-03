using Application;
using Application.Authentication;
using ExpensesApi.Models.Mapper.Home;
using Infrastructure.Authentication;
using Infrastructure.EF;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

builder.Configuration.AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true);

builder.Services.ApplicationRegistrationService();
builder.Services.InfrastructureRegistrationService(builder.Configuration);
builder.Services.InfrastructureAuthenticationRegistrationService(builder.Configuration);
builder.Services.ApplicationAuthenticationRegistrationService(builder.Configuration);
builder.Services.AddAutoMapper(typeof(HomeModelsMapper).Assembly);
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ExpenseUi", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ExpenseUi");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
