using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace models.Entities.UserWorkout
{
    [Table("user_workout")]
    public class UserWorkout
    {
        [Column("uw_id")]
        [Key]
        public int UwId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("workout_id")]
        public int WorkoutId { get; set; }
    }
}