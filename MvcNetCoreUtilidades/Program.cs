using MvcNetCoreUtilidades.Helpers;

var builder = WebApplication.CreateBuilder(args);

//  RESOLVEMOS LAS DEPENDECIAS DE NUESTRO HELPER
builder.Services.AddSingleton<HelperPathProvider>();
//  INCLUIMOS SESSION
builder.Services.AddSession();
//  INCLUIREMOS UN SEVICIO MEMORYCACHE
builder.Services.AddMemoryCache();
//  INDICAMOS QUE USAREMOS LA MEMORIA DISTRIBUIDA
builder.Services.AddDistributedMemoryCache();
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


//  HABILITAMOS LOS FICHEROS ESTATICOS
app.UseStaticFiles();



app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
//  HABILITAMOS SESSION
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
