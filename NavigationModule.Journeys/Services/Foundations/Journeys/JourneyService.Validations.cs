using Journeys.API.Models.Entities.Waypoints;
using NavigationModule.Journeys.Models.Entities.Journeys;
using NavigationModule.Journeys.Models.Exceptions.Journeys;
using NetXceptions.Validations;

namespace NavigationModule.Journeys.Services.Foundations.Journeys
{
    public partial class JourneyService
    {
        private static void ValidateJourneyInput(Journey journey)
        {
            ValidateJourneyIsNull(journey);

            var invalidJourneyException = new InvalidJourneyException();

            invalidJourneyException.Validate(
                (Rule: ModelValidator.IsInvalid(journey.Id, nameof(Journey.Id)),
                    Parameter: nameof(Journey.Id)),
                (Rule: ModelValidator.IsInvalid(journey.UserId, nameof(Journey.UserId)),
                    Parameter: nameof(Journey.UserId)),
                (Rule: ModelValidator.IsInvalid(journey.StartingDate, nameof(Journey.StartingDate)),
                    Parameter: nameof(Journey.StartingDate)),
                (Rule: ModelValidator.IsInvalid(journey.ArrivalDate, nameof(Journey.ArrivalDate)),
                    Parameter: nameof(Journey.ArrivalDate)),
                (Rule: ModelValidator.IsInvalid(journey.StartingPoint, nameof(Journey.StartingPoint)),
                    Parameter: nameof(Journey.StartingPoint)),
                (Rule: ModelValidator.IsInvalid(journey.ArrivalPoint, nameof(Journey.ArrivalPoint)),
                    Parameter: nameof(Journey.ArrivalPoint))
            );

            invalidJourneyException.ThrowIfContainsErrors();
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
                throw new NotFoundJourneyException(journeyId);
            }
        }
    }
}
