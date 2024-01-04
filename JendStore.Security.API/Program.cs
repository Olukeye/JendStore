using JendStore.Security.API.Data;
using JendStore.Security.Service.API.AuthRepository;
using JendStore.Security.Service.API.Configuration;
using JendStore.Security.Service.API.ServiceExtensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddAutoMapper(typeof(MapperInitilizer));
var Config = builder.Configuration;

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(Config);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


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

    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JendStoreSecurity v1"));
}

app.ExceptionHandlerConfiguration();

app.UseSwagger();

ApplyAutoMigration();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
