namespace models.Entities.Fit_workouts
{
    public class FitWorkouts
    {
        public int workout_id { get; set; }
        public string s3_path { get; set; } = string.Empty;
        public string workout_name { get; set; } = string.Empty;
    }
}