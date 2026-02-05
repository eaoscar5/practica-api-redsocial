using PracticaApi_RedSocial.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<MastodonService>(client =>
{
    client.DefaultRequestHeaders.Add(
        "User-Agent",
        "PracticaApi_RedSocial/1.0 (Contacto: estudiante@ejemplo.com)"
    );
    client.DefaultRequestHeaders.Add(
        "Accept",
        "application/json"
    );
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
