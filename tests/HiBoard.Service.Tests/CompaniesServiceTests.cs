using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Application.Repositories;
using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using Moq;
using NUnit.Framework;

namespace HiBoard.Service.Tests
{
    [TestFixture]
    public class CompaniesServiceTests
    {
        private CompaniesService _companiesService = null!;
        private Mock<ICompaniesRepository> _companiesRepositoryMock = null!;

        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            _companiesRepositoryMock = new Mock<ICompaniesRepository>();
            _companiesService = new CompaniesService(_companiesRepositoryMock.Object);
        }

        [Test]
        public async Task CreateCompany_HappyFlow()
        {
            //Given
            var companyDto = new CompanyDto()
            {
                Name = "Test",
                Description = "DescriptionTest",
                Departments = new List<string>()
                {
                    "Test1",
                    "Test2"
                },
                Users = new List<UserDto>()
            };

            _companiesRepositoryMock.Setup(repo => repo.CreateCompanyAsync(companyDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(companyDto);

            //When
            var companyResult = await _companiesService.CreateCompanyAsync(companyDto, CancellationToken.None);

            //Then
            Assert.AreEqual(companyDto, companyResult);
        }

        [Test]
        public async Task GetCompanyById_HappyFlow()
        {
            //Given
            var companyDto = new CompanyDto()
            {
                Id = 1,
                Name = "Test",
                Description = "DescriptionTest",
                Departments = new List<string>()
                {
                    "Test1",
                    "Test2"
                },
                Users = new List<UserDto>()
            };

            _companiesRepositoryMock.Setup(repo => repo.GetCompanyByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(companyDto);

            //When
            var companyResult = await _companiesService.GetCompanyByIdAsync(1, CancellationToken.None);

            //Then
            Assert.AreEqual(companyDto,companyResult);
        }

        [Test]
        public async Task UpdateCompany_HappyFlow()
        {
            //Given
            var companyDto = new CompanyDto()
            {
                Id = 1,
                Name = "Test",
                Description = "DescriptionTest",
                Departments = new List<string>()
                {
                    "Test1",
                    "Test2"
                },
                Users = new List<UserDto>()
            };

            _companiesRepositoryMock
                .Setup(repo => repo.UpdateCompanyAsync(1, companyDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(companyDto);

            //When
            var companyResult = await _companiesService.UpdateCompanyAsync(1, companyDto, CancellationToken.None);

            //Then
            Assert.AreEqual(companyDto,companyResult);
        }
    }
}