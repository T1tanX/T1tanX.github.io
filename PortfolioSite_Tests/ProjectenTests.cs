using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Services;
using PortfolioSite2.DTOs;
using PortfolioSite2.Models;

namespace PortfolioSite_Tests;

public class ProjectenTests {
    private ApplicationDbContext TestDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
    
    [Fact]
    public async Task ProjectPostTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new ProjectenService(dbContext);

        var testDto = new ProjectInputDto {
            Titel = "Test",
            Beschrijving = "Dit is een test project lol :)",
            Categorie = "Web",
            GitHubUrl = "https://github.com/test/project",
            Datum = DateTime.Now
        };
        
        //Act
        await service.PostP(testDto);
        
        //Assert
        var projectList = await dbContext.Projects.ToListAsync();
        Assert.Single(projectList);
        Assert.Equal(testDto.Titel, projectList[0].Titel);
        Assert.Equal(testDto.Beschrijving, projectList[0].Beschrijving);
    }

    [Fact]
    public async Task GetOneProjectTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new ProjectenService(dbContext);
        
        dbContext.Projects.Add(new Project {
            Id = 1,
            Titel = "Test",
            Beschrijving = "Dit is een test project lol :)",
            Categorie = "Web",
            GitHubUrl = "https://github.com/test/project",
            Datum = DateTime.Now
        });
        await dbContext.SaveChangesAsync();
        
        //Act
        var project = await service.GetOneP(1);
        
        //Assert
        Assert.NotNull(project);
        Assert.Equal(1, project.Id);
        Assert.Equal("Test", project.Titel);
    }
    
    [Fact]
    public async Task GetProjectTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new ProjectenService(dbContext);

        dbContext.Projects.AddRange(
            new Project { Titel = "Test1", Beschrijving = "Dit is een test project lol :)", Categorie = "Web", GitHubUrl = "https://github.com/test/1", Datum = DateTime.Now },
            new Project { Titel = "Test2", Beschrijving = "Dit is ook een test project :)", Categorie = "API", GitHubUrl = "https://github.com/test/2", Datum = DateTime.Now },
            new Project { Titel = "Test3", Beschrijving = "Dit is het laatste test project :(", Categorie = "Mobile", GitHubUrl = "https://github.com/test/3", Datum = DateTime.Now }
        );
        await dbContext.SaveChangesAsync();
        
        //Act
        var projecten = await service.GetP();
        
        //Assert;
        Assert.NotNull(projecten);
        Assert.Equal(3, projecten.Count());
    }

    [Fact]
    public async Task UpdateProjectTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new ProjectenService(dbContext);

        dbContext.Projects.Add(new Project {
            Id = 8,
            Titel = "Test8",
            Beschrijving = "Dit is een test project lol :)",
            Categorie = "Web",
            GitHubUrl = "https://github.com/test/8",
            Datum = DateTime.Now
        });
        await dbContext.SaveChangesAsync();

        var updateInhoud = new ProjectInputDto {
            Titel = "Test8",
            Beschrijving = "Deze beschrijving was laatst gewijzigd.",
            Categorie = "Web",
            GitHubUrl = "https://github.com/test/8",
            Datum = DateTime.Now
        };

        //Act
        var updatedProject = await service.UpdateP(8, updateInhoud);
        
        //Assert
        var project = await service.GetOneP(8);
        Assert.True(updatedProject);
        Assert.NotNull(project);
        Assert.Equal("Test8", project.Titel);
        Assert.Equal("Deze beschrijving was laatst gewijzigd.", project.Beschrijving);
    }

    [Fact]
    public async Task DeleteProjectTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new ProjectenService(dbContext);
        
        dbContext.Projects.Add(new Project {
            Id = 10,
            Titel = "Test10",
            Beschrijving = "Dit is een test project lol :)",
            Categorie = "Web",
            GitHubUrl = "https://github.com/test/10",
            Datum = DateTime.Now
        });
        await dbContext.SaveChangesAsync();
        
        //Act
        var deleteProject = await service.DeleteP(10);
        
        //Assert
        var project = await dbContext.Projects.FindAsync(10);
        Assert.Null(project);
        Assert.True(deleteProject);
    }
}