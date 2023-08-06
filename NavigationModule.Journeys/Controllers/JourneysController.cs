using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NavigationModule.Infrastructure.Infrastructures.Authorizations;
using NavigationModule.Journeys.Models.DTOs.Filters;
using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Services.Orchestrations.Journeys;
using System.Net;

namespace NavigationModule.Journeys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JourneysController : ControllerBase
    {
        private readonly IJourneyOrchestrationService journeyOrchestrationService;

        public JourneysController(IJourneyOrchestrationService journeyOrchestrationService)
        {
            this.journeyOrchestrationService = journeyOrchestrationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Journey), (int)HttpStatusCode.OK)]
        public async ValueTask<ActionResult<Journey>> CreateJourneyAsync(JourneyRequest journeyRequest) =>
            Ok(await this.journeyOrchestrationService.AddJourneyAsync(journeyRequest));

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Journey>), (int)HttpStatusCode.OK)]
        public async ValueTask<ActionResult<IReadOnlyList<Journey>>> GetJourneys(
            int? page,
            int? pageSize,
            bool orderByDesceding = true)
        {
            return Ok(await this.journeyOrchestrationService.RetrieveJourneysAsync(
                page ?? 1,
                pageSize ?? 0,
                orderByDesceding));
        }

        [HttpGet("{journeyId}")]
        [ProducesResponseType(typeof(Journey), (int)HttpStatusCode.OK)]
        public async ValueTask<ActionResult<Journey>> GetJourneyById(Guid journeyId) =>
            Ok(await this.journeyOrchestrationService.RetrieveJourneyByIdAsync(journeyId));

        [HttpDelete("{journeyId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async ValueTask<ActionResult<Journey>> RemoveJourney(Guid journeyId) =>
            Ok(await this.journeyOrchestrationService.RemoveJourneyByIdAsync(journeyId));

        [HttpGet("filter")]
        [ProducesResponseType(typeof(IReadOnlyList<Journey>), (int)HttpStatusCode.OK)]
        [Authorization(AuthorizationType.All, "Admin")]
        public async ValueTask<ActionResult<IReadOnlyList<Journey>>> FilterJourneys(
            string userId,
            int? page,
            int? pageSize,
            bool orderByDesceding = true)
        {
            return Ok(await this.journeyOrchestrationService.FilterJourneysAsync(
                userId,
                page ?? 1,
                pageSize ?? 0,
                orderByDesceding));
        }

        [HttpGet("stats")]
        [ProducesResponseType(typeof(IReadOnlyList<UserStats>), (int)HttpStatusCode.OK)]
        [Authorization(AuthorizationType.All, "Admin")]
        public async ValueTask<ActionResult<IReadOnlyList<UserStats>>> GetJourneyStats(
            [FromQuery] JourneyFilter filters)
        {
            return Ok(await this.journeyOrchestrationService.GetJourneysStatsAsync(filters));
        }
    }
}
