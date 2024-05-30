using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentAPI.Api.Extensions;
using TournamentAPIv2.Core.Repositories;
using TournamentAPIv2.Data.Data;
using TournamentAPIv2.Data.Repositories;

/*
1. Jag har l�mnat kvar varje steg i koden, f�r att kunna g� tillbaka och kika p� det igen. 
   Det g�r det tyv�rr lite r�rigt att l�sa.
2. Patch-metoden har jag f�rs�kt implementera p� Tournament, men ins�g att jag inte riktigt 
   har koll p� PATCH, s� jag har kommit en bit p� v�g, hoppas jag, men inte hela v�gen fram.
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
