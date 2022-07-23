using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiBoard.Application.Repositories;
using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace HiBoard.Service.Tests;

[TestFixture]
public class UsersServiceTests
{
    private UsersService _usersService = null!;
    private Mock<IUsersRepository> _usersRepositoryMock = null!;
    private Mock<IMapper> _mapperMock = null!;
    private Mock<IHttpContextAccessor> _contextAccessorMock = null!;

    [OneTimeSetUp]
    public void OnTimeSetup()
    {
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _mapperMock = new Mock<IMapper>();
        _contextAccessorMock = new Mock<IHttpContextAccessor>();
        _usersService = new UsersService(_usersRepositoryMock.Object, _contextAccessorMock.Object,
            _mapperMock.Object);
    }

    [Test]
    public async Task GetByIdAsync_HappyFlow()
    {
        //Given
        var userDto = new UserDto()
        {
            Id = 1,
            CompanyId = 1,
            CompletedActivities = 1,
            Department = "Test",
            Email = "user@test.com",
            FirstName = "FirstTest",
            LastName = "LastTest",
            ManagerId = 1,
            Role = "Employee",
            TotalActivities = 2
        };

        _usersRepositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDto);

        //When
        var userResult = await _usersService.GetUserAsync(1, CancellationToken.None);

        //Then
        Assert.AreEqual(userDto, userResult);
    }

    [Test]
    public async Task GetUserEmployeesAsync_HappyFlow()
    {
        var managerUserDto = new UserDto()
        {
            Id = 2,
            CompanyId = 1,
            CompletedActivities = 1,
            Department = "Test",
            Email = "user@test.com",
            FirstName = "FirstTest",
            LastName = "LastTest",
            ManagerId = 1,
            Role = "Manager",
            TotalActivities = 2
        };

        var userDto = new UserDto()
        {
            Id = 3,
            CompanyId = 1,
            CompletedActivities = 1,
            Department = "Test",
            Email = "user@test.com",
            FirstName = "FirstTest",
            LastName = "LastTest",
            ManagerId = 2,
            Role = "Employee",
            TotalActivities = 2
        };

        _usersRepositoryMock
            .Setup(repo => repo.GetUserEmployeesAsync(2, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<UserDto>() {userDto});

        //When
        var userList = await _usersService.GetUserEmployees(2, CancellationToken.None);

        //Then
        Assert.AreEqual(userDto, userList.First());
    }
}