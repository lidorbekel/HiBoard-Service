using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Application.Repositories;
using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Domain.Requests;
using Moq;
using NUnit.Framework;

namespace HiBoard.Service.Tests
{
    [TestFixture]
    public class TemplatesServiceTests
    {
        private TemplatesService _templatesService = null!;
        private Mock<ITemplatesRepository> _templatesRepositoryMock = null!;

        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            _templatesRepositoryMock = new Mock<ITemplatesRepository>();
            _templatesService = new TemplatesService(_templatesRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllTemplates_HappyFlow()
        {
            //Given
            var templateDto = new TemplateDto()
            {
                Id = 123,
                Department = "TestDepartment",
                Name = "TestName",
                CompanyId = 1,
                Activities = new List<ActivityDto>()
            };

            _templatesRepositoryMock!
                .Setup(repo => repo.GetAllTemplates(1, "TestDepartment", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<TemplateDto>
                {
                    templateDto
                });

            //When
            var result = await _templatesService.GetAllTemplates(1, "TestDepartment", CancellationToken.None);

            //Then
            Assert.AreEqual(templateDto, result.First());
        }

        public async Task GetTemplateDtoById_HappyFlow()
        {
            //Given
            var templateDto = new TemplateDto()
            {
                Id = 1,
                Department = "TestDepartment",
                Name = "TestName",
                CompanyId = 1,
                Activities = new List<ActivityDto>()
            };

            _templatesRepositoryMock.Setup(repo => repo.GetTemplateDtoById(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(templateDto);

            //When
            var templateResult = await _templatesService.GetTemplateById(1, CancellationToken.None);

            //Then
            Assert.AreEqual(templateDto, templateResult);
        }

        public async Task CreateTemplate_HappyFlow()
        {
            //Given
            var templateDto = new TemplateDto()
            {
                Id = 1,
                Department = "TestDepartment",
                Name = "TestName",
                CompanyId = 1,
                Activities = new List<ActivityDto>()
            };

            _templatesRepositoryMock.Setup(repo =>
                    repo.CreateTemplate(1, templateDto.Department, templateDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(templateDto);

            //When
            var templateResult =
                await _templatesService.CreateTemplate(1, templateDto.Department, templateDto, CancellationToken.None);

            //Then
            Assert.AreEqual(templateDto, templateResult);
        }

        public async Task UpdateTemplate_HappyFlow()
        {
            //Given
            var templateDto = new TemplateDto()
            {
                Id = 1,
                Department = "TestDepartment",
                Name = "TestName",
                CompanyId = 1,
                Activities = new List<ActivityDto>()
            };


            _templatesRepositoryMock.Setup(repo => repo.UpdateTemplate(1, templateDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(templateDto);

            //When
            var templateResult = await _templatesService.UpdateTemplate(1, templateDto, CancellationToken.None);

            //Then
            Assert.AreEqual(templateDto, templateResult);
        }
    }
}