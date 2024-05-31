using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentAPI.Api.Extensions;
using TournamentAPIv2.Core.Repositories;
using TournamentAPIv2.Data.Data;
using TournamentAPIv2.Data.Repositories;

/*
   Jag har lämnat kvar varje steg i koden, för att kunna gå tillbaka och kika på det igen och jobba vidare på den
   Det gör det tyvärr lite rörigt att läsa.

   De konstiga projektnamnet är för att jag lyckades pajja projektet och var tvungen att börja om från ett nytt fräscht projekt
   efter några dagar ;)
 */

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TournamentAPIv2ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentAPIv2ApiContext") ?? throw new InvalidOperationException("Connection string 'TournamentAPIv2ApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
