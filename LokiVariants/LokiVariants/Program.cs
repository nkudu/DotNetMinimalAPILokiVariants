using LokiVariants;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("LokiVariants"));
builder.Services.AddScoped<IVariantService, VariantService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/variants", async (IVariantService VariantService)
    => await VariantService.GetVariants());

app.MapGet("/variants/{id}", async (int id, IVariantService VariantService)
    => await VariantService.GetVariantById(id));

app.MapPost("/variants", async (VariantRequest VariantRequest, IVariantService VariantService)
    => await VariantService.CreateVariant(VariantRequest));

app.MapPut("/variants/{id}", async (int id, VariantRequest VariantRequest, IVariantService VariantService)
    => await VariantService.UpdateVariant(id, VariantRequest));

app.MapDelete("/variants/{id}", async (int id, IVariantService VariantService)
    => await VariantService.DeleteVariant(id));

app.Run();
