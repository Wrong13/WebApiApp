List<Person> users = new List<Person> // Начальные данные
{
new() {Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
new() {Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
new() {Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

//Get запрос (получение)
app.MapGet("/api/users", () => users); // Возвращение всех пользователей
app.MapGet("/api/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id); // Поск пользователя
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    return Results.Json(user); // Возвращение в Json
});

// Delete запрос(удаление)
app.MapDelete("/api/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    
    users.Remove(user);
    return Results.Json(user);
});
 
// Post запрос(добавление)
app.MapPost("/api/users", (Person user)=>{
    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});
 
// Put запрос(изменение)
app.MapPut("/api/users", (Person userData) => {
    var user = users.FirstOrDefault(u => u.Id == userData.Id); 
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

app.Run();

public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}