using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using workout_tracker_backend.Data;
using workout_tracker_backend.Helpers;
using workout_tracker_backend.Models;
using workout_tracker_backend.Repositories;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using workout_tracker_backend.Dtos;
using Microsoft.Extensions.Options;

namespace workout_tracker_tests.UnitTests;

public class UserRepoUnitTests
{
    private readonly IMapper _mapper;
    private readonly IOptions<AppSettings> _appSettings;


    public UserRepoUnitTests()
    {
        _mapper = A.Fake<IMapper>();
        _appSettings = A.Fake<IOptions<AppSettings>>();
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
            DatabaseContext.Users.Add(
                new User()
                {
                    Id = 1,
                    Name = "Sam",
                    Email = "sam@boers.com",
                    Password = "Jantje123"
                }
            );
            DatabaseContext.Users.Add(
                new User()
                {
                    Id = 2,
                    Name = "Marcel",
                    Email = "marcel@boers.com",
                    Password = "Jantje123"
                }
                );
                            DatabaseContext.Users.Add(
                new User()
                {
                    Id = 3,
                    Name = "Steph",
                    Email = "steph@curry.com",
                    Password = "Jantje123"
                }
                );
                            DatabaseContext.Users.Add(
                new User()
                {
                    Id = 4,
                    Name = "Klay",
                    Email = "klay@thompson.com",
                    Password = "Jantje123"
                }
                );

            await DatabaseContext.SaveChangesAsync();
        }
        return DatabaseContext;
    }

    [Fact]
    public async void GetsUserById_ReturnsUser()
    {
        // Given
        var userId = 1;
        var DbContext = await CreateAndSeedDB();
        var repo = new UserRepo(DbContext, _mapper, _appSettings);

        // When
        var result = repo.GetUserById(userId);

        // Then
        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
    }

    [Fact]
    public async void GetsAllUsers_ReturnsUsers()
    {
        // Given
        var DbContext = await CreateAndSeedDB();
        var repo = new UserRepo(DbContext, _mapper, _appSettings);

        // When
        var result = repo.GetAllUsers();

        // Then
        result.Should().NotBeNull();
        result.Should().HaveCount(4);
    }
}