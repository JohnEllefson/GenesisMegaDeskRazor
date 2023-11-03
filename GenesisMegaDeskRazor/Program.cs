using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Session;
using GenesisMegaDeskRazor.Data;
using GenesisMegaDeskRazor.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddDbContext<GenesisMegaDeskRazorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GenesisMegaDeskRazorContext") ?? throw new InvalidOperationException("Connection string 'GenesisMegaDeskRazorContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
