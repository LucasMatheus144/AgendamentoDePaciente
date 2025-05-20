using Aplication.Domain.Repository.Implementations;
using Aplication.Domain.Repository;
using Aplication.Domain.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using NHibernate;
using NHibernate.Cfg;
using Aplication.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddTransient<PacienteService>();
builder.Services.AddTransient<ValidacaoService>();


builder.Services.AddTransient<IRepository, RepositoryInMemory>();

builder.Services.AddSingleton<ISessionFactory>((s) =>
{
    var config = new Configuration();
    config.Configure();
    // config.Configure(@"..\Aplication.Infra\hibernate.cfg.xml"); 

    return config.BuildSessionFactory();
});


builder.Services.AddCors(
    b => b.AddDefaultPolicy(c =>
        c.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
