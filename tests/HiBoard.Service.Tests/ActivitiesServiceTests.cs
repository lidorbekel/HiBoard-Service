using HiBoard.Application.CustomExceptions.ActivityExceptions;
using HiBoard.Application.Repositories;
using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using Moq;
using NUnit.Framework;

namespace HiBoard.Service.Tests
{
    [TestFixture]
    public class ActivitiesServiceTests
    {
        private ActivitiesService _activitiesService = null!;
        private Mock<IActivitiesRepository> _activitiesRepositoryMock = null!;

        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            _activitiesRepositoryMock = new Mock<IActivitiesRepository>();
            _activitiesService = new ActivitiesService(_activitiesRepositoryMock.Object);
        }

        [Test]
        public async Task GetList_HappyFlow()
        {
            //Given
            var activityDto = new ActivityDto
            {
                Id = 1,
                Description = "test",
                Tag = "tag",
                Title = "title",
                Week = 1,
                TimeEstimation = new TimeSpan(),
                UserAverageTime = new TimeSpan(),
                UserCompletedCount = 1
            };

            _activitiesRepositoryMock!.Setup(repository => repository.GetListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ActivityDto>
                {
                    activityDto
                });

            //When
            var favoriteBets = (await _activitiesService.GetActivitiesAsync(CancellationToken.None))
                .ToList();

            //Then
            Assert.AreEqual(activityDto, favoriteBets.First());
        }

        [Test]
        public async Task GetById_HappyFlow()
        {
            //Given
            var activityDto = new ActivityDto
            {
                Id = 1,
                Description = "test",
                Tag = "tag",
                Title = "title",
                Week = 1,
                TimeEstimation = new TimeSpan(),
                UserAverageTime = new TimeSpan(),
                UserCompletedCount = 1
            };

            _activitiesRepositoryMock.Setup(repository => repository.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(activityDto);

            //When
            var activity = await _activitiesService.GetActivityAsync(1, CancellationToken.None);

            //Then
            Assert.AreEqual(activityDto, activity);
        }

        [Test]
        public async Task GetById_ActivityNotFound_ShouldReturnNull()
        {
            //Given
            _activitiesRepositoryMock.Setup(repository => repository.GetByIdAsync(2, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ActivityNotFoundException(2));

            //When
            var activityResult = await _activitiesService.GetActivityAsync(2, CancellationToken.None);

            //Then
            Assert.AreEqual(null, activityResult);
        }

        [Test]
        public async Task CreateActivity_HappyFlow()
        {
            //Given
            var activityDto = new ActivityDto
            {
                Id = 1,
                Description = "test",
                Tag = "tag",
                Title = "title",
                Week = 1,
                TimeEstimation = new TimeSpan(),
                UserAverageTime = new TimeSpan(),
                UserCompletedCount = 1
            };

            _activitiesRepositoryMock
                .Setup(repository => repository.CreateAsync(activityDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(activityDto);

            //When
            var activityResult = await _activitiesService.CreateActivityAsync(activityDto, CancellationToken.None);

            //Then
            Assert.AreEqual(activityDto, activityResult);
        }

        [Test]
        public async Task UpdateActivity_HappyFlow()
        {
            //Given
            var activityDto = new ActivityDto
            {
                Id = 1,
                Description = "test",
                Tag = "tag",
                Title = "title",
                Week = 1,
                TimeEstimation = new TimeSpan(),
                UserAverageTime = new TimeSpan(),
                UserCompletedCount = 1
            };

            _activitiesRepositoryMock
                .Setup(repository => repository.UpdateAsync(1, activityDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(activityDto);

            //When
            var activityResult = await _activitiesService.UpdateActivityAsync(1, activityDto, CancellationToken.None);

            //Then
            Assert.AreEqual(activityDto, activityResult);
        }
    }
}