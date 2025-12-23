using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;  
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;  // საჭირო ფუნქციებისვის შემოგვაქ ესენი

var builder = WebApplication.CreateBuilder(args); // ამთით იწყება აპის შენება  მის შიგნითაა:
                                                  // Configuration(jason file), Deoendency injection container
                                                  //logging და enviroment info


builder.Services.AddControllers();   // კონტროლერები შემოგვაქ


builder.Services.AddEndpointsApiExplorer(); // სვაგერის თემებია
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => 
{ 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
}); //ეუბნება აპპს დაუკავშირდეს SQL სერვერს რომლის დაკავშირების სახელიც jason file ში DefaultConnection-ია

builder.Services.AddTransient<Seed>(); // seeding ის თემაა რომლიც ვერ გავიგე
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
} //ესენი იმ კაცმა იჩალიჩა რაღაცები ვერ გავიგე

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} //სვაგერის თემებეი

app.UseHttpsRedirection(); // htto გადაყავს https ად
app.UseAuthorization(); // აუტორიზაციის თემებია
app.MapControllers(); // აკავშირებს გზებს კონტროლერებთან
app.Run(); // იწყებს სერვერს