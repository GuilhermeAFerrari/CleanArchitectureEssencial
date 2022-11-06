# Clean Arch Essencial

<p align="center">
    <a href="#application">Application</a>&nbsp;&nbsp;|&nbsp;&nbsp;
    <a href="#-technologies-and-frameworks">Technologies and Frameworks</a>&nbsp;&nbsp;|&nbsp;&nbsp;
    <a href="#requirements">Requirements</a>&nbsp;&nbsp;|&nbsp;&nbsp;
    <a href="#running-the-application">Running the application</a>&nbsp;&nbsp;|&nbsp;&nbsp;
    <a href="#features">Features</a>&nbsp;&nbsp;|&nbsp;&nbsp;
    <a href="#-author">Author</a>&nbsp;&nbsp;|&nbsp;&nbsp;
    <a href="#-license">License</a>
</p>

### Application

The goal is to apply the concepts of clean architecture in .Net MVC Project

## 🛠 Technologies and Frameworks
- [.NET 6](https://visualstudio.microsoft.com/pt-br/vs/community/)
- [EntityFrameworkCore](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)
- [Razor Pages](https://docs.microsoft.com/pt-br/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio)

## Requirements
- Visual Studio version 17.0 or higher of 2022 for compiling and running the backend - [VisualStudio](https://visualstudio.microsoft.com/pt-br/vs/community/)
- Microsoft SQLServer Database - [SQLServer](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

## Running the application
```bash
# Clone this repository or download .zip and extract the files
$ git clone <https://github.com/GuilhermeAFerrari/CleanArchitectureEssencial.git>

# Access the project's CleanArchitectureEssencial-main folder and open the solution file with Visual Studio

# Open the appsetting.json file and configure your connection string for the database adding or editing the values in the "ConnectionStrings" key

# In the package manager console execute the command to run the migrations
$ dotnet ef database update

# Run the application (CRTL+F5)
```

## Features
- Products management
- Categories management

## 😎 Author
- Guilherme Ferrari
- E-mail: guile.ferrari@hotmail.com
- [<img src="https://img.shields.io/badge/linkedin-%230077B5.svg?&style=for-the-badge&logo=linkedin&logoColor=white" />](https://www.linkedin.com/in/guilherme-antonio-ferrari/)

## 📝 License
MIT.