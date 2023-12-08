using AutoMapper;
using JendStore.Services.API.Configuration;
using JendStore.Services.API.Data;
using JendStore.Services.API.IRepository;
using JendStore.Services.API.Repository;
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
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

ApplyAutoMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
