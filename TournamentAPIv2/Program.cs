using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentAPI.Api.Extensions;
using TournamentAPIv2.Core.Repositories;
using TournamentAPIv2.Data.Data;
using TournamentAPIv2.Data.Repositories;

/*
1. Jag har lämnat kvar varje steg i koden, för att kunna gå tillbaka och kika på det igen. 
   Det gör det tyvärr lite rörigt att läsa.
2. Patch-metoden har jag försökt implementera på Tournament, men insåg att jag inte riktigt 
   har koll på PATCH, så jag har kommit en bit på väg, hoppas jag, men inte hela vägen fram.
 */

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TournamentAPIv2ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentAPIv2ApiContext") ?? throw new InvalidOperationException("Connection string 'TournamentAPIv2ApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

//builder.Services.AddScoped<IGameRepository, GameRepository>();
//builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<IUoW, UoW>();
builder.Services.AddAutoMapper(typeof(TournamentMappings));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.SeedDataAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
