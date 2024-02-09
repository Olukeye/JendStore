using AutoMapper;
using JendStore.Services.API.Configuration;
using JendStore.Services.API.Data;
using JendStore.Services.API.IRepository;
using JendStore.Services.API.Repository;
using JendStore.Services.API.ServiceExtensions;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CouponApi v1"));
}

ApplyAutoMigration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
