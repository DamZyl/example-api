using ExampleApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetSection("Sql")
        .GetSection("ConnectionString")
        .Value));

builder.Services.AddCors(p => p.AddPolicy("cors-policy", policyBuilder =>
{
    policyBuilder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors-policy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();