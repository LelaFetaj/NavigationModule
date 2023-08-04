using System.Linq.Expressions;
using System.Security.Claims;
using Journeys.API.Models.Entities.Waypoints;
using Microsoft.AspNetCore.Http;
using NavigationModule.Journeys.Infrastructure.ContextAccessors;
using NavigationModule.Journeys.Infrastructure.Mappings;
using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Models.Filters;
using NavigationModule.Journeys.Services.Foundations.Journeys;

namespace NavigationModule.Journeys.Services.Processings.Journeys
{
    public class JourneyProcessingService : IJourneyProcessingService
    {
        private readonly IJourneyService journeyService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JourneyProcessingService(IJourneyService journeyService, IHttpContextAccessor httpContextAccessor)
        {
            this.journeyService = journeyService;
            this.httpContextAccessor=httpContextAccessor;
        }

        public async ValueTask<Journey> AddJourneyAsync(JourneyRequest journeyRequest)
        {
            string userid =
                this.httpContextAccessor.HttpContext.User.FindFirst(
                    type: ClaimTypes.NameIdentifier)?.Value;

            var journey = new Journey
            {
                Distance = journeyRequest.Distance,
                StartingDate = journeyRequest.StartingDate,
                ArrivalDate = journeyRequest.ArrivalDate,
                StartingPoint = journeyRequest.StartingPoint,
                ArrivalPoint = journeyRequest.ArrivalPoint
            };

            await this.journeyService.AddJourneyAsync(journey);

            return journey;
        }

        public async ValueTask<JourneyResponse> UpsertJourneyAsync(JourneyRequest journeyRequest)
        {
            string userId = this.workContext.ToggId;

            Journey maybeJourney = await this.journeyService.RetrieveJourneyByIdAsync(
                journeyId: journeyRequest.Id);

            if (maybeJourney is null)
            {
                Journey newJourney = journeyRequest?.ToEntity();
                newJourney.UserId = userId;

                await this.journeyService.AddJourneyAsync(newJourney);

                return newJourney?.ToJourneyResponse();
            }

            var updateDefinition = maybeJourney.CreateUpdateDefinition(journeyRequest);

            Journey updatedJourney = await this.journeyService.ModifyJourneyAsync(maybeJourney, updateDefinition);

            return updatedJourney?.ToJourneyResponse();
        }

        public async ValueTask<FilteredResponse<JourneyResponse>> RetrieveFilteredJourneys(
            JourneyFilter filters)
        {
            var response = new FilteredResponse<JourneyResponse>();

            Expression<Func<Journey, bool>> searchCondition = x =>
                (string.IsNullOrWhiteSpace(filters.ToggId) || x.UserId == filters.ToggId)
                && (filters.StartDate == null || x.ArrivalDate >= filters.StartDate)
                && (filters.EndDate == null || x.ArrivalDate <= filters.EndDate);

            var pagination = new Pagination
            {
                SortBy = nameof(Journey.ArrivalDate),
                OrderByDescending = filters.OrderByDesceding,
                Page = filters.Page,
                PageSize = filters.PageSize
            };

            var (journeys, count) =
                await this.journeyService.RetrieveJourneysAsync(searchCondition, pagination);

            if (journeys is not null)
            {
                response.Count = count;
                response.FilteredCollection = journeys.ToJourneyListResponse();
            }

            return response;
        }

        public async ValueTask<IReadOnlyList<JourneyResponse>> RetrieveJourneysAsync(
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true)
        {
            string userId = this.workContext.ToggId;

            Expression<Func<Journey, bool>> searchCondition =
                vehicle => vehicle.UserId == userId;

            var pagination = new Pagination
            {
                SortBy = nameof(Journey.ArrivalPoint),
                OrderByDescending = orderByDescending,
                Page = page,
                PageSize = pagesize > 0 ? pagesize : 0
            };

            var (journeys, _) =
                await this.journeyService.RetrieveJourneysAsync(searchCondition, pagination);

            if (journeys is null || journeys.Count <= 0)
            {
                return Array.Empty<JourneyResponse>();
            }

            return journeys.ToJourneyListResponse();
        }

        public async ValueTask<JourneyResponse> RetrieveLatestJourneyAsync()
        {
            string userId = this.workContext.ToggId;
            var journey = await this.journeyService.RetrieveLatestJourneyByUserIdAsync(userId);

            return journey?.ToJourneyResponse();
        }
    }
}
