using AddressBookP;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PhoneBookDatabaseSet>(builder.Configuration.GetSection("PhoneBookDatabaseSet"));
builder.Services.AddSingleton<InfoSer>();

var app = builder.Build();

app.MapGet("/", () => "PhoneBook");

app.MapGet("/api/PhoneBook", async(InfoSer infoSer) => await infoSer.Get());

app.MapGet("/api/PhoneBook/{UUID}", async (InfoSer infoSer, int UUID) =>
    {
        var information = await infoSer.Get(UUID);
        return information is null ? Results.NotFound() : Results.Ok(information);
    });


app.MapPost("/api/PhoneBook", async (InfoSer infoSer, Information information) =>
    {
        await infoSer.Create(information);
        return Results.Ok();
    });

app.MapPut("/api/PhoneBook/{UUID}", async (InfoSer infoSer, int UUID, Information updateInformation) =>
{
    var Information = await infoSer.Get(UUID);
    if (Information is null) return Results.NotFound();

    updateInformation.UUID = Information.UUID;
    await infoSer.Update(UUID, updateInformation);

    return Results.NotFound();
});

app.MapDelete("/api/PhoneBook/{UUID}", async (InfoSer infoSer, int UUID) =>
{
    var information = await infoSer.Get(UUID);
    if (information is null) return Results.NotFound();

    await infoSer.Remove(information.UUID);

    return Results.NoContent();
});

app.Run();