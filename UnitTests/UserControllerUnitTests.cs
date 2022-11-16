using FakeItEasy;
using workout_tracker_backend.Interfaces;
using AutoMapper;
using workout_tracker_backend.Dtos;
using workout_tracker_backend.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using workout_tracker_backend.Controllers;
using System.Collections;

namespace workout_tracker_tests.UnitTests;

public class UserControllerUnitTests
{
    private readonly IUser _repository;
    private readonly IMapper _mapper;

    public UserControllerUnitTests()
    {
        _repository = A.Fake<IUser>();
        _mapper = A.Fake<IMapper>();
    }


    [Fact]
    public void UserController_GetAllUsers_ReturnOK()
    {
        // Given
        var users = A.Fake<IEnumerable<UserReadDto>>();
        var usersList = A.Fake<IEnumerable<User>>();
        A.CallTo(() => _mapper.Map<IEnumerable<UserReadDto>>(usersList)).Returns(users);
        var controller = new UserController(_repository, _mapper);
     
        // When
        var result = controller.GetAllUsers();
    
        // Then
        result.Result.Should().BeOfType(typeof(OkObjectResult));
    }


    [Fact]
    public void UserController_Register_ReturnOK()
    {
        // Given
        int userId = 1;
        UserCreateDto userCreateDto = A.Fake<UserCreateDto>();
        userCreateDto.Name = "Sam";
        userCreateDto.Email = "sam@boers.family";
        userCreateDto.Password = "Jantje123";
        A.CallTo(() => _repository.Register(userCreateDto)).Returns(userId);
        var controller = new UserController(_repository, _mapper);
    
        // When
        var result = controller.Register(userCreateDto);
    
        // Then
        result.Should().BeOfType(typeof(OkObjectResult));
    }



}