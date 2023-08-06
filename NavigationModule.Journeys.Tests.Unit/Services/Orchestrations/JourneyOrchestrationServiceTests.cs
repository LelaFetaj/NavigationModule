using NavigationModule.Journeys.Services.Orchestrations.Journeys;
using NavigationModule.Journeys.Services.Processings.Achievements;
using NavigationModule.Journeys.Services.Processings.Journeys;

namespace NavigationModule.Journeys.Tests.Unit.Services.Orchestrations
{
    public partial class JourneyOrchestrationServiceTests
    {
        private readonly Mock<IAchievementProcessingService> achievementProcessingServiceMock;
        private readonly Mock<IJourneyProcessingService> journeyProcessingServiceMock;
        private readonly IJourneyOrchestrationService journeyOrchestrationService;

        public JourneyOrchestrationServiceTests()
        {
            this.achievementProcessingServiceMock = new Mock<IAchievementProcessingService>();
            this.journeyProcessingServiceMock = new Mock<IJourneyProcessingService>();

            this.journeyOrchestrationService = new JourneyOrchestrationService(
                achievementProcessingService: this.achievementProcessingServiceMock.Object,
                journeyProcessingService: this.journeyProcessingServiceMock.Object);
        }

        private static T CreateRandomObject<T>() where T : class
            => CreateObjectFiller<T>().Create();

        private static Filler<T> CreateObjectFiller<T>()
            where T : class
        {
            var filler = new Filler<T>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
