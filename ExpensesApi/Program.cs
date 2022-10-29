using DatabaseProj.DatabaseEntities.ConnectionInfo;
using Microsoft.EntityFrameworkCore;
using SeederProj;
using ServiceProj.AplicationService.Expenses;
using ServiceProj.AplicationService.ExpensesList;
using ServiceProj.DbService.Expenses;
using ServiceProj.DbService.ExpensesList;
using ServiceProj.Models.Mapper;
using ServiceProj.ValidationService.Expenses;
using ServiceProj.ValidationService.ExpensesList;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ExpensesMapper).Assembly);

builder.Services.AddTransient<IUserExpensesListService, UserExpensesListService>();
builder.Services.AddTransient<IExpensesListService, ExpensesListService>();
builder.Services.AddTransient<IExpensesListValidation, ExpensesListValidation>();

builder.Services.AddTransient<IUserExpensesService, UserExpensesService>();
builder.Services.AddTransient<IUserExpensesAnalysisService, UserExpensesService>();
builder.Services.AddTransient<IExpensesService, ExpensesService>();
builder.Services.AddTransient<IExpensesValidation, ExpensesValidation>();
builder.Services.AddScoped<IExpensesSeeder, ExpensesSeeder>();

builder.Services.AddDbContext<ExpensesApiDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseDbString")));
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
