using Application;
using Infrastructure.Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ApplicationRegistrationService();
builder.Services.InfrastructureRegistrationService(builder.Configuration);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
