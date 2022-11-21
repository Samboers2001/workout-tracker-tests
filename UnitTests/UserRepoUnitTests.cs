using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using workout_tracker_backend.Data;
using workout_tracker_backend.Helpers;
using workout_tracker_backend.Models;
using workout_tracker_backend.Repositories;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace workout_tracker_tests.UnitTests;

public class UserRepoUnitTests
{
    private readonly IMapper _mapper;

    public UserRepoUnitTests()
    {
            _mapper = A.Fake<IMapper>();
            // _appSettings = 
    }

    private async Task<WorkoutTrackerDbContext> CreateAndSeedDB()
    {
        var options = new DbContextOptionsBuilder<WorkoutTrackerDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        var DatabaseContext = new WorkoutTrackerDbContext(options);
        DatabaseContext.Database.EnsureCreated();
        if (await DatabaseContext.Users.CountAsync() <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                DatabaseContext.Users.Add(
                    new User()
                    {
                        Id = 1,
                        Name = "Sam",
                        Email = "sam@boers.family",
                        Password = "Jantje123",
                        Height = 189,
                        Weight = 85
                    }
                );
                await DatabaseContext.SaveChangesAsync();
            }
        }
        return DatabaseContext;
    }

    [Fact]
    public async void UserRepo_GetUserById_ReturnsUser()
    {
        // Given
        // var DbContext = await CreateAndSeedDB();
        // var services = new ServiceCollection().AddOptions();
        // IServiceCollection serviceCollection = services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        // var repository = new UserRepo(DbContext, _mapper, _appSettings);

        // When

        // Then
    }
}