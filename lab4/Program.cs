using Microsoft.EntityFrameworkCore;
using Slab4;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppContextDb>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});


builder.Services.AddTransient<IAppRepository, AppRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();