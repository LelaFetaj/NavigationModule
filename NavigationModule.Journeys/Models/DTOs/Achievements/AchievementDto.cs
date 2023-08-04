using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Journeys.Models.DTOs.Achievements
{
    public class AchievementDto
    {
        public bool IsRewarded { get; set; }
        public DateTimeOffset? RewardDate { get; set; }
        public double DailyDistance { get; set; }
    }
}
