using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace models.entities.fit_workouts
{
    public class FitWorkouts
    {
        public int workout_id { get; set; }
        public string s3_path { get; set; }
        public string workout_name { get; set; }
    }
}