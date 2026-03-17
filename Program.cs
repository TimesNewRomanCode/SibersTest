using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManager_1.Data;
using ProjectManager_1.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TaskService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    //In Docker, the database container is launched later than the project, which is why migrations fail.
    
    var retries = 10;
    var delay = TimeSpan.FromSeconds(5);

    for (int i = 0; i < retries; i++)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("Миграции успешно применены");
            break;
        }
        catch (SqlException)
        {
            Console.WriteLine($"The database is not ready yet\n");
            Thread.Sleep(delay);
        }
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();
    
app.UseExceptionHandler("/Home/Error");
app.UseHsts();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();