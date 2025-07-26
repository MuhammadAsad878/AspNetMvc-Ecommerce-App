
---

# 📘 .NET Core MVC Tutorial – Notes & Project Structure

This README provides a comprehensive summary of key concepts, project structure, and conventions used in a typical **ASP.NET Core MVC** application.

---

## ✅ General Concepts

* A **Solution** (`.sln`) can contain **multiple projects**, each representing different layers or modules of the application.
* Each project includes a `.csproj` file (editable via "Edit Project File"), which manages **project configurations** and **dependencies**.
* Use **NuGet Package Manager** to install external libraries. These are reflected in the `.csproj` file under `<ItemGroup>`.

---

## 📁 Project Structure

```
MyApp/
│
├── Connected Services/
├── Dependencies/           # External and project dependencies (e.g., Microsoft.AspNetCore.*, EntityFramework, etc.)
├── Properties/
│   └── launchSettings.json # Contains environment and profile-specific launch settings
│
├── wwwroot/                # Static files: CSS, JS, images, PDFs, etc.
│   └── lib/                # Client-side libraries (e.g., Bootstrap, jQuery)
│
├── Controllers/            # C# classes handling incoming HTTP requests  HomeController : Controller , IActionResult, return View()
├── Models/                 # C# classes for application data and logic
├── Views/                  # Razor view files (.cshtml) used for rendering UI  
│   └── Shared/
│       ├── _Layout.cshtml  # Global layout page (header, footer, scripts)
│       └── Error.cshtml    # Default error page
│   └── _ViewStart.cshtml   # Sets the default layout for views
│
├── appsettings.json        # Stores configurations, connection strings, secrets
├── appsettings.Production.json
├── Program.cs              # Main entry point (replaces Startup.cs in .NET 6+)
```

---

## ⚙️ Program.cs – Configuration & Middleware

> In .NET 6+, `Startup.cs` is merged into `Program.cs`

### What happens in `Program.cs`?

1. **Service Registration** – via `builder.Services.Add...`
2. **Middleware Configuration** – via `app.Use...`
3. **Routing Setup** – via `app.MapControllerRoute(...)`
4. **Project Execution** – via `app.Run()`

### Example Routing:

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

* The `?` after `id` means it's optional.
* Default route: HomeController → Index action.

### Exception Handling:

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
```

* Uses `Views/Shared/Error.cshtml` in production mode.

---

## 🧠 MVC Conventions
NOTE: Controllers & Views Folder names must not be changed rather we can change the Models name if we want

### 🔹 Controllers

* Must inherit from `Controller`
* Naming must follow PascalCase and end with `Controller`
  e.g., `HomeController.cs`, `ProductController.cs`

```csharp
public class HomeController : Controller
{
    public IActionResult Index()  // Index will automatically directed if Action was not passed in asp-action="" attribute
    {
        var data = new Dictionary<string, string>();
        return View(data);                  
    }
}
```
The name of the IActionResult will be the action method we passed when using MVC patter to match route like
/Home/Delete/id etc 
### 🔹 Views

* `.cshtml` Razor files represent UI
* Shared layouts are placed under `Views/Shared/`
* `_ViewStart.cshtml` sets the layout for all views

```csharp
@{
    Layout = "~/Views/Shared/_Layout.cshtml";  ( ~ Sign indicates root path )
}
```

*  `_ViewImports.cshtml` sets the global imports file like
    `@using FirstProject
     @using FirstProject.Models
     @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`

---
### 🔹 Models
NOTE: Models folder name can be updateable 
Models/Category.cs contain the Model to be defined for Category table to be created in Database using Entitiy Framework

In models we define our model like

using FirstProject.Models
{
    public class Category
    {
        [Key]   // this will set this CategoryID as PK 
        public int Category_ID { get; set; }  // if we set it to id EF automatically set ID or CategoryID as primary key or we use annotations if not ID
        [Required] // this annotation will set Name as not null in database
        public strgin Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}

Now install
`   Microsoft.EntitiyFrameworkCore
    Microsoft.EntityFrameworkCore.SqlServer  
    Microsoft.EntityFrameworkCore.Tools
`
from Nuget packages 
NOTE: All have same .NET versions like 9.0.6

Now Add connection string in appsettings.json as
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FirstProject;Trusted_Connection=True;TrustServerCertificate=True"
  }

Now use connection string to connect with database and create database 
Now establish conneciton between Entity Framework and database 
Now create in project folder FirstProject/Data/ApplicationDbContext.cs to connect EF & Database
Each class in Data must have to implement DbContext class using which we access Entity Framework
Now after creating constructor with DbContextOptions register your ApplicatonDbContext in Program.cs before builder.build()
Telling our project we are adding Entity Framework Core 

Now Tools > Nuget Package Manager > Package Manager Console
Now write few commands to work with EF Core
1. update-database 
This will create database in Sql Server
Now we have to create a table for this we go to our ApplicatonDbcontext.cs and create a table like
1. public DbSet<Category> Category { get; set; }  // Category is the class name we build in our Moders
1. add-migration meaningfulNameHere
This will create table 
1.         public DbSet<Category> Categories { get; set; }



### 🔹 Server Side Validations

When we pass the Category or other object in controller action method we must check the applied validations on server side
using the Models.IsValid() Method which will validate the object according to the constraints defined in that Model class
using different annotations like [required] [Key] etc also we can pass custom error messages like

1. Models.IsValid() // validate the constraints we defined on Category Model
1. [Range(1,100,ErrorMessage = "Custom Error Message")]
2. asp-validation-for="Name" OR asp-validation-for="DisplayOrder"
1  asp-validation-summary="All" OR "ModelOnly" OR "None" this will display all errors we passed like following
   if(category.Name == category.DiplayOrder.ToString()){
    ModelState.AddModelError("name", "The Display Order cannot exactly match the Name this error shown in asp-validation-for="Name" and summary also"); OR
    ModelState.AddModelError("","This empty key error also displayed in summary ")

   }
1. 
### 🔹 Client Side Validations

For client side validations we use Jquery which is already added as scripts in Views/Shared/_ValidationScriptsPartial
by using following syntax in our Add or Create file

this will include that file 
@section Scripts{
    @{
        <partial name ="_ValidationScriptsPartial_">
    }
}

## 🗄️ Configuration Files

### `appsettings.json`

* Centralized place for:

  * **Connection Strings**
  * **Secret Keys**
  * **Environment Settings**
* You can have environment-specific versions like:

  * `appsettings.Development.json`
  * `appsettings.Production.json`

To switch environments:

* Update `ASPNETCORE_ENVIRONMENT` in `launchSettings.json` or via environment variables.

---



### ✅ Attributes / Styling:

* Use **code blocks** for all code or terminal commands.
* Use **bold** for key terms, and *italics* for side notes.
* Use ✅ / ⚠️ / ❌ to highlight actions, warnings, or errors.
* Use bullet points for listing concepts or folders.
* Break complex sections with dividers (`---`) for clarity.

---

