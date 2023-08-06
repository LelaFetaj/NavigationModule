using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.Entities.Journeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Journeys.Tests.Unit.Services.Orchestrations
{
    public partial class JourneyOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldAddJourneyAndAchievementAsync()
        {
            //arrange
            JourneyRequest inputRequest = CreateRandomObject<JourneyRequest>();
            Journey expectedResponse = CreateRandomObject<Journey>();

            this.journeyProcessingServiceMock
                .Setup(service => service.AddJourneyAsync(inputRequest))
                .ReturnsAsync(expectedResponse);

            //act
            var actualResult =
                await this.journeyOrchestrationService.AddJourneyAsync(inputRequest);

            //assert
            actualResult.Should().BeEquivalentTo(expectedResponse);

            this.journeyProcessingServiceMock.Verify(
                service => service.AddJourneyAsync(inputRequest), Times.Once);

            this.achievementProcessingServiceMock.Verify(
                service => service.UpsertAchievementAsync(inputRequest), Times.Once);

            this.journeyProcessingServiceMock.VerifyNoOtherCalls();
            this.achievementProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
