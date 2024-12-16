using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using University.Data;
using University.Endpoints;

const string myAllowSpecificOrigins = "allowCloudFlareOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("internal", options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "University API";
        document.Info.Contact = new OpenApiContact()
        {
            Name = "Yousef Eldosoky",
            Email = "eldosoky@yousefsite.com",
            Url = new Uri("https://yousefsite.com/")
        };
        
        return Task.CompletedTask;
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("https://university-esx.pages.dev", "http://localhost:4200", "https://university.yousefsite.com").AllowAnyHeader()
                .AllowAnyMethod().AllowCredentials();
        });
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString)
    );

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.UseCors(myAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.CustomMapIdentityApi<IdentityUser>();
app.MapQaEndpoint();

app.Run();