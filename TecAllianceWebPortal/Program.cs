using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TecAllianceWebPortal;
using TecAllianceWebPortal.Services;
using TecAllianceWebPortal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.AllowCredentials().WithOrigins("https://localhost:44440").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<PortalDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PortalDb")));
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IUserService, UserService>();

IMapper mapper = new MapperConfiguration(f => f.AddProfile(new MappingProfile())).CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseCors(MyAllowSpecificOrigins);

app.MapFallbackToFile("index.html"); ;

app.Run();
