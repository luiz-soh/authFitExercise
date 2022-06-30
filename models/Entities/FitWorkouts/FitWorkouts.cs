using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace models.Entities.FitWorkouts
{
    [Table("fit_workouts")]
    public class FitWorkouts
    {
        [Column("workout_id")]
        [Key]
        public int WorkoutId { get; set; }

        [Column("s3_path")]
        public string S3Path { get; set; } = string.Empty;

        [Column("workout_name")]
        public string WorkoutName { get; set; } = string.Empty;
    }
}