using HashIdBasedApi;
using HashidsNet;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new DbContext());
/*
 * Caution: changing the salt will change the hashed IDs of your entities
 * This will invalidate all saved URLs (for example a user linking a video
 * from your site would get a 404 using the same URL after changing the salt)
 */
builder.Services.AddSingleton<IHashids>(new Hashids(
    salt: "Secret hash salt that should not change",
    minHashLength: 10));

builder.Services.AddControllers();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
