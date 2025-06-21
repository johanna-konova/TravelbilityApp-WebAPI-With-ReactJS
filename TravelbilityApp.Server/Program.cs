using FluentValidation;
using Microsoft.AspNetCore.WebUtilities;

using System.Text.Json;

using TravelbilityApp.WebAPI.JsonConverters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new TimeOnlyNewtonsoftJsonConverter());

        options.SerializerSettings.Error = (sender, args) =>
        {
            args.ErrorContext.Handled = true;
        };
    });

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationSwaggerGen();

builder.Services.AddCors(config =>
{
    config.AddPolicy("AllowMyServer", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://localhost:51092");
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowMyServer");

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.ContentType?.StartsWith("application/json") == true)
    {
        return;
    }

    response.ContentType = "application/json";

    var payload = JsonSerializer.Serialize(new
    {
        status = response.StatusCode,
        title = ReasonPhrases.GetReasonPhrase(response.StatusCode)
    });
    
    await response.WriteAsync(payload);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
