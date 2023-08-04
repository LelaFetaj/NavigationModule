using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Journeys.Models.Entities.Achievements
{
    public class Achievement
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public bool IsRewarded { get; set; }
        public DateTimeOffset? RewardDate { get; set; }
        public double DailyDistance { get; set; }
    }
}
