using AutoMapper;
using JendStore.Products.Service.API.Configurations;
using JendStore.Products.Service.API.Data;
using JendStore.Products.Service.API.Repository;
using JendStore.Products.Service.API.Repository.Interface;
using JendStore.Products.Service.API.ServiceExtensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MapperInitilizer));

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
var Config = builder.Configuration;


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.ConfigureJWT(Config);
builder.Services.ConfigSwagger(Config);


var app = builder.Build();

//Automatic Migration (checks for any pending migration, and if there's any it automatically apply migration to the database)
void ApplyAutoMigration()
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider.GetService<DatabaseContext>();

        if (services.Database.GetPendingMigrations().Count() > 0)
        {
            services.Database.Migrate();
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductAPI v1"));
}

ApplyAutoMigration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
