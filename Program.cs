using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ufo_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<UFODbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Подключение базы данных

UFODbContext dbContext = new UFODbContext();


// Регистрация пользователя

app.MapGet("/api/user/add/{first_name}/{second_name}/{password}", async (string first_name, string second_name, string birthday, string password) => {
    
    User user = new User(
        user_id: 123,
        first_name: first_name,
        second_name: second_name,
        birthday: birthday,
        password: password
    );

    List<User> users = [user];

    dbContext.Add(user);
    await dbContext.SaveChangesAsync();

    return Results.Json(users);

});

// Получить список пользователей

app.MapGet("/api/users", () => {

    return Results.Json(dbContext.Users.ToList());

});

// Изменить данные пользователя

app.MapGet("/api/user/edit/{first_name}/{new_first_name}/{new_second_name}/{new_password}", async (string first_name, string new_first_name, string new_second_name, string new_password) => {

    var user = dbContext.Users.FirstOrDefault(x => x.getFirstName() == first_name);

    if (user == null){
        return Results.NotFound(new { message = "Пользователь не найден!" });
    } else {
        user.setFirstName(new_first_name);
        user.setSecondName(new_second_name);
        user.setPassword(new_password);

        await dbContext.SaveChangesAsync();

        return Results.Json(dbContext.Users.ToList());
    }    

});

// Удаление пользователя из базы

app.MapGet("/api/user/delete/{first_name}", async (string first_name) => {

    var user = dbContext.Users.FirstOrDefault(x => x.getFirstName() == first_name);

    if (user != null){
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return Results.Json(dbContext.Users.ToList());
    } else {
        return Results.Json( new { message = "Пользователь не найден!" });
    }

});

// Логин недоделанный

app.MapPost("/login/{first_name}/{password}", async (string first_name, string password, HttpContext context) => {

    var user = dbContext.Users.FirstOrDefault(x => x.getFirstName() == first_name);

    if (user != null){
        if (user.getPassword() == password){

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.getFirstName()) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            Results.Redirect("/index");

        } else {
            Results.Json(new { message = "Неверный пароль!" });
        }            
    } else {
        Results.Json(new { message = "Пользователь не найден!" });
    }
    

});

// Выход недоделанный

app.MapGet("/logout", async (HttpContext context) => {

    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Json(new { message = "Вы вышли из системы!" });

});

app.Map("/index", [Authorize]() => "Hello World!");

app.Run();

