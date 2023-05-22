using ExampleApi.Infrastructure;
using ExampleApi.Models.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails(x =>
{
    x.Map<ValidationException>((ctx, ex) => new ValidationProblemDetails(ex, ctx));
    x.Map<NotFoundException>((ctx, ex) => new NotFoundProblemDetails(ex, ctx));
    x.Map<InternalServerException>((ctx, ex) => new InternalServerProblemDetails(ex, ctx));
    x.Map<UnhandledException>((ctx, ex) => new UnhandledProblemDetails(ex, ctx));
});

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

app.UseProblemDetails();
app.UseCors("cors-policy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();