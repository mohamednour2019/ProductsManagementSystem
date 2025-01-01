using ProductManagementSystem.AppService.Product.AddProduct;
using FluentValidation;
using ProductManagementSystem.Infrastructure.Context.ProductContext;
using MediatR;
using ProductManagementSystem.API.ValidatorBehavior;
using ProductManagementSystem.Infrastructure.AutoMapper;
using ProductManagementSystem.Domain._SharedKernel;
using ProductManagementSystem.API.Middlewares;
using ProductManagementSystem.AppService.Product.GetProduct;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Domain.Product.Dto;
using ProductManagementSystem.AppService.Product.EditProduct;
using ProductManagementSystem.AppService.Product.DeleteProduct;
using ProductManagementSystem.API.ExtensionMethods;
using pro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

//services registration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AddProductCommand).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddDbContext<AppDatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped<IUnitOfWork, AppDatabaseContext>();
builder.Services.RegistrRepositories(typeof(ProductRepository).Assembly);
builder.Services.AddCors();

var app = builder.Build();
//app.UseGlobalExceptionHandleMiddleware();//global exception middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
// Enable routing
app.UseRouting();


// minimal API's
var productApi = app.MapGroup("/api/products");

// Get by id
productApi.MapGet("", async (long id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductQuery() { Id = id });
    return Results.Ok(result);
});

// Get list
productApi.MapPost("/list", async (SearchProductDto searchProductDto, IMediator mediator) =>
{
    var result = await mediator.Send(searchProductDto);
    return Results.Ok(result);
});

// Edit product
productApi.MapPut("", async (EditProductCommand editProductCommand, IMediator mediator) =>
{
    var result = await mediator.Send(editProductCommand);
    return Results.Ok(result);
});

// Add product
productApi.MapPost("/add", async (AddProductCommand addProductCommand, IMediator mediator) =>
{
    var result = await mediator.Send(addProductCommand);
    return Results.Ok(result);
});

// Delete product
productApi.MapDelete("", async (long id, IMediator mediator) =>
{
    var result = await mediator.Send(new DeleteProductCommand() { Id = id });
    return Results.Ok(result);
});


app.Run();

