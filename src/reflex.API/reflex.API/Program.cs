using Microsoft.EntityFrameworkCore;
using reflex.Application.Services;
using reflex.Domain.Interface;
using reflex.Domain.Interface.ServiceInterface;
using reflex.Persistence.Data;
using reflex.Persistence.Interface;
using reflex.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DbContext and Migration
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReflexdBConnection"), b => b.MigrationsAssembly("reflex.API")));

//Add Persistence Services
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOtpRepository, OtpRepository>();
builder.Services.AddTransient<ICardRepository, CardRepository>();

//Add Application Services
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IOtpService, OtpService>();





//Set up Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("ALLOW",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("ALLOW");
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
