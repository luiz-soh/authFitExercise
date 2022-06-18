using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace models.entities.user_workout
{
    public class UserWorkout
    {
        public int uw_id { get; set; }
        public int user_id { get; set; }
        public int workout_id { get; set; }
    }
}