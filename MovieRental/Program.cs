using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.Rental;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

//builder.Services.AddSingleton<IRentalFeatures, RentalFeatures>(); //IRentalFeatures shouldn't be registered as a singleton since we have to inject it with MovieRentalDbContext which is scoped
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); //exception handling for development mode

    app.UseSwagger();
    app.UseSwaggerUI();
} else { //This is a possible implementation of a global exception handler
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            var errorResponse = new
            {
                message = "An unexpected error occurred.",
            };

            var json = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(json);
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var client = new MovieRentalDbContext())
{
	client.Database.EnsureCreated();
}

app.Run();
