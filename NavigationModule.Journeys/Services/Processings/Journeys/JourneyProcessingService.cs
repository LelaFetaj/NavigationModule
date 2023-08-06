using Microsoft.AspNetCore.Http;
using NavigationModule.Infrastructure.Models.Filters;
using NavigationModule.Journeys.Models.DTOs.Filters;
using NavigationModule.Journeys.Models.DTOs.Journeys;
using NavigationModule.Journeys.Models.DTOs.UserStats;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Services.Foundations.Journeys;
using System.Linq.Expressions;
using System.Security.Claims;

namespace NavigationModule.Journeys.Services.Processings.Journeys
{
    public class JourneyProcessingService : IJourneyProcessingService
    {
        private readonly IJourneyService journeyService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JourneyProcessingService(IJourneyService journeyService, IHttpContextAccessor httpContextAccessor)
        {
            this.journeyService = journeyService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async ValueTask<Journey> AddJourneyAsync(JourneyRequest journeyRequest)
        {
            string userId =
                this.httpContextAccessor.HttpContext.User.FindFirst(
                    type: ClaimTypes.NameIdentifier)?.Value;

            var journey = new Journey
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Distance = journeyRequest.Distance,
                StartingDate = journeyRequest.StartingDate,
                ArrivalDate = journeyRequest.ArrivalDate,
                StartingPoint = journeyRequest.StartingPoint,
                ArrivalPoint = journeyRequest.ArrivalPoint
            };

            return await this.journeyService.AddJourneyAsync(journey);
        }

        public async ValueTask<IReadOnlyList<Journey>> RetrieveJourneysAsync(
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true)
        {
            string userId =
                this.httpContextAccessor.HttpContext.User.FindFirst(
                    type: ClaimTypes.NameIdentifier)?.Value;

            Expression<Func<Journey, bool>> searchCondition =
                vehicle => vehicle.UserId == userId;

            var pagination = new Pagination<Journey, DateTimeOffset>
            {
                OrderBy = x => x.ArrivalDate,
                Page = page,
                PageSize = pagesize,
                OrderByDescending = orderByDescending,
            };

            var (journeys, _) =
                await this.journeyService.RetrieveFilteredJourneysAsync(searchCondition, pagination);

            if (journeys is null || journeys.Count <= 0)
            {
                return Array.Empty<Journey>();
            }

            return journeys;
        }

        public async ValueTask<IReadOnlyList<Journey>> RetrieveJourneysAsync(
            string userId,
            int page = 1,
            int pagesize = 0,
            bool orderByDescending = true)
        {
            Expression<Func<Journey, bool>> searchCondition = journey =>
                string.IsNullOrWhiteSpace(userId) || journey.UserId == userId;

            var pagination = new Pagination<Journey, DateTimeOffset>
            {
                OrderBy = x => x.ArrivalDate,
                Page = page,
                PageSize = pagesize,
                OrderByDescending = orderByDescending,
            };

            var (journeys, _) =
                await this.journeyService.RetrieveFilteredJourneysAsync(searchCondition, pagination);

            if (journeys is null || journeys.Count <= 0)
            {
                return Array.Empty<Journey>();
            }

            return journeys;
        }

        public async ValueTask<Journey> RetrieveJourneyByIdAsync(Guid journeyId)
        {
            return await this.journeyService.RetrieveJourneyByIdAsync(journeyId);
        }

        public async ValueTask<Journey> RemoveJourneyByIdAsync(Guid journeyId)
        {
            return await this.journeyService.RemoveJourneyByIdAsync(journeyId);
        }

        public async ValueTask<IReadOnlyList<UserStats>> RetrieveJourneyStatsAsync(JourneyFilter filters)
        {
            Expression<Func<Journey, bool>> searchCondition = journey =>
                (string.IsNullOrWhiteSpace(filters.UserId) || journey.UserId == filters.UserId)
                && journey.ArrivalDate.Year == filters.Year
                && journey.ArrivalDate.Month == (int)filters.Month;

            var pagination = new Pagination<UserStats, double>
            {
                OrderBy = x => x.TotalDistance,
                Page = filters.Page,
                PageSize = filters.PageSize,
                OrderByDescending = filters.OrderByDesceding,
            };

            List<UserStats> userStats =
                await this.journeyService.RetrieveJourneyStatsAsync(searchCondition, pagination);

            if (userStats is null || userStats.Count <= 0)
            {
                return Array.Empty<UserStats>();
            }

            return userStats;
        }
    }
}
