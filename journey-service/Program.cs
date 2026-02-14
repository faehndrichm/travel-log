using journey_service.Entities;
using journey_service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata  = false; // only disable for local dev
        options.Audience = "account";
        options.Authority = "http://keycloak:8080/realms/demo-realm";
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = "http://localhost:8080/realms/demo-realm",
        };
        
    });


// Add services to the container.
builder.Services.AddSingleton<KafkaProducerService>();


builder.Services.AddSingleton<S3ImageStorageService>(sp =>
{
    var factory = new S3ClientFactory();
    var internalClient = factory.CreateClient();
    var externalClient = factory.CreateExternalClient();
    return new S3ImageStorageService(internalClient, externalClient);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<JourneyContext>(opt =>
    opt.UseInMemoryDatabase("JourneyList"));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // TODO: from env var
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
