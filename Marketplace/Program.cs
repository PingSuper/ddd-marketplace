using Marketplace.ClassifiedAd;
using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.Shared;
using Marketplace.Domain.UserProfile;
using Marketplace.Framework;
using Marketplace.Infrastructure;
using Marketplace.UserProfile;
using Raven.Client.Documents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc(
            "v1",
            new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "ClassifiedAds",
                Version = "v1"
            }
        );
    }
    );

// RavenDB
var store = new DocumentStore
{
    Urls = new[] { "http://localhost:8080" },
    Database = "Marketplace_Chapter9",
    Conventions =
                {
                    FindIdentityProperty = x => x.Name == "DbId"
                }
};
store.Initialize();

// DI
var purgomalumClient = new PurgomalumClient();

builder.Services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
builder.Services.AddScoped(c => store.OpenAsyncSession());
builder.Services.AddScoped<IUnitOfWork, RavenDbUnitOfWork>();
builder.Services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<ClassifiedAdsApplicationService>();
builder.Services.AddScoped(c =>
                new UserProfileApplicationService(
                    c.GetService<IUserProfileRepository>(),
                    c.GetService<IUnitOfWork>(),
                    text => purgomalumClient.CheckForProfanity(text).GetAwaiter().GetResult()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

