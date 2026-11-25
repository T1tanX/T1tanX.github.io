using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Services;
using PortfolioSite2.DTOs;
using PortfolioSite2.Models;

namespace PortfolioSite_Tests;

public class BlogTests {
    private ApplicationDbContext TestDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
    
    [Fact]
    public async Task BlogPostTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new BlogService(dbContext);

        var testDto = new BlogInputDto {
            Titel = "Test",
            Datum = DateTime.Now,
            Inhoud = "Dit is een test blog lol :)"
        };
        
        //Act
        await service.PostB(testDto);
        
        //Assert
        var blogList = await  dbContext.Blogs.ToListAsync();
        Assert.Single(blogList);
        Assert.Equal(testDto.Titel, blogList[0].Titel);
        Assert.Equal(testDto.Inhoud, blogList[0].Inhoud);
    }

    [Fact]
    public async Task GetOneBlogTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new BlogService(dbContext);
        
        dbContext.Blogs.Add(new Blog{
            Id = 1,
            Titel = "Test",
            Datum = DateTime.Now,
            Inhoud = "Dit is een test blog lol :)"
        });
        await dbContext.SaveChangesAsync();
        
        //Act
        var blog = await service.GetOneB(1);
        
        //Assert
        Assert.NotNull(blog);
        Assert.Equal(1, blog.Id);
        Assert.Equal("Test", blog.Titel);
    }
    
    [Fact]
    public async Task GetBlogTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new BlogService(dbContext);

        dbContext.Blogs.AddRange(
            new Blog { Titel = "Test1", Datum = DateTime.Now, Inhoud = "Dit is een test blog lol :)" },
            new Blog { Titel = "Test2", Datum = DateTime.Now, Inhoud = "Dit is ook een test blog :)" },
            new Blog { Titel = "Test3", Datum = DateTime.Now, Inhoud = "Dit is de laatste test blog :(" }
            );
        await dbContext.SaveChangesAsync();
        
        //Act
        var blogs = await service.GetB();
        
        //Assert;
        Assert.NotNull(blogs);
        Assert.Equal(3, blogs.Count());
    }

    [Fact]
    public async Task UpdateBlogTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new BlogService(dbContext);

        dbContext.Blogs.Add(new Blog {
            Id = 8,
            Titel = "Test8",
            Datum = DateTime.Now,
            Inhoud = "Dit is een test blog lol :)"
        });
        await dbContext.SaveChangesAsync();

        var updateInhoud = new BlogInputDto {
            Titel = "Test8",
            Datum = DateTime.Now,
            Inhoud = "Deze inhoud was laatst gewijzigd."
        };

        //Act
        var updatedBlog = await service.UpdateB(8, updateInhoud);
        
        //Assert
        var blog = await service.GetOneB(8);
        Assert.True(updatedBlog);
        Assert.NotNull(blog);
        Assert.Equal("Test8", blog.Titel);
        Assert.Equal("Deze inhoud was laatst gewijzigd.", blog.Inhoud);
    }

    [Fact]
    public async Task DeleteBlogTest() {
        //Arrange
        var dbContext = TestDbContext();
        var service = new BlogService(dbContext);
        
        dbContext.Blogs.Add(new Blog {
            Id = 10,
            Titel = "Test10",
            Datum = DateTime.Now,
            Inhoud = "Dit is een test blog lol :)"
        });
        await dbContext.SaveChangesAsync();
        
        //Act
        var deleteBlog =  await service.DeleteB(10);
        
        //Assert
        var blog = await dbContext.Blogs.FindAsync(10);
        Assert.Null(blog);
        Assert.True(deleteBlog);
    }
}