using Journeys.API.Models.Entities.Waypoints;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Models.Exceptions.Journeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Journeys.Services.Foundations.Journeys
{
    public partial class JourneyService
    {
        private static void ValidateJourneyInput(Journey journey)
        {
            ValidateJourneyIsNull(journey);

            if (journey.ArrivalPoint != null)
            {
                Validate(
                    (Rule: IsInvalid(journey.Id, nameof(Journey.Id)),
                        Parameter: nameof(Journey.Id)),
                    (Rule: IsInvalid(journey.UserId, nameof(Journey.UserId)),
                        Parameter: nameof(Journey.UserId)),
                    (Rule: IsInvalid(journey.Distance, nameof(Journey.Distance)),
                        Parameter: nameof(Journey.Distance)),
                    (Rule: IsInvalid(journey.StartingDate, nameof(Journey.StartingDate)),
                        Parameter: nameof(Journey.StartingDate)),
                    (Rule: IsInvalid(journey.ArrivalDate, nameof(Journey.ArrivalDate)),
                        Parameter: nameof(Journey.ArrivalDate)),
                    (Rule: IsInvalid(journey.StartingPoint, nameof(Journey.StartingPoint)),
                        Parameter: nameof(Journey.StartingPoint)),
                    (Rule: IsInvalid(journey.ArrivalPoint, nameof(Journey.ArrivalPoint)),
                        Parameter: nameof(Journey.ArrivalPoint))
                );
            }
            else
            {
                Validate(
                    (Rule: IsInvalid(journey.Id, nameof(Journey.Id)),
                        Parameter: nameof(Journey.Id)),
                    (Rule: IsInvalid(journey.UserId, nameof(Journey.UserId)),
                        Parameter: nameof(Journey.UserId)),
                    (Rule: IsInvalid(journey.StartingDate, nameof(Journey.StartingDate)),
                        Parameter: nameof(Journey.StartingDate)),
                    (Rule: IsInvalid(journey.StartingPoint, nameof(Journey.StartingPoint)),
                        Parameter: nameof(Journey.StartingPoint))
                );
            }
        }

        private static void ValidateJourneyId(Guid journeyId)
        {
            if (journeyId == Guid.Empty)
            {
                throw new InvalidJourneyException(
                    parameterName: nameof(Journey.Id),
                    parameterValue: journeyId);
            }
        }

        private static void ValidateJourneyIsNull(Journey journey)
        {
            if (journey is null)
            {
                throw new NullJourneyException();
            }
        }

        private static void ValidateStorageJourney(Journey journey, Guid journeyId)
        {
            if (journey is null)
            {
                throw new NotFoundJorneyException(journeyId);
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidJourneyException = new InvalidJourneyException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidJourneyException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidJourneyException.ThrowIfContainsErrors();
        }

        private static dynamic IsInvalid(string value, string fieldName) => new
        {
            Condition = string.IsNullOrWhiteSpace(value),
            Message = $"{fieldName} is required."
        };

        private static dynamic IsInvalid(DateTimeOffset value, string fieldName) => new
        {
            Condition = value == default,
            Message = $"{fieldName} is required."
        };

        private static dynamic IsInvalid(double value, string fieldName) => new
        {
            Condition = value <= 0,
            Message = $"{fieldName} is required."
        };

        private static dynamic IsInvalid(StartingPoint waypoint, string fieldName) => new
        {
            Condition = waypoint is null || ValidateWaypoint(waypoint),
            Message = $"{fieldName} is required."
        };

        private static dynamic IsInvalid(ArrivalPoint waypoint, string fieldName) => new
        {
            Condition = waypoint is null || ValidateWaypoint(waypoint),
            Message = $"{fieldName} is required."
        };

        private static bool ValidateWaypoint(StartingPoint waypoint)
        {
            return waypoint.Latitude == 0 || waypoint.Longitude == 0;
        }

        private static bool ValidateWaypoint(ArrivalPoint waypoint)
        {
            return waypoint.Latitude == 0 || waypoint.Longitude == 0;
        }
    }
}
