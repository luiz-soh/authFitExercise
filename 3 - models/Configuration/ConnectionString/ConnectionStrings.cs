namespace Models.Configuration.ConnectionString
{
    public class ConnectionStrings
    {
        public const string ConnectionString = "ConnectionStrings";

        public ConnectionStrings()
        {
            FitExerciseDB = string.Empty;
        }

        public string FitExerciseDB { get; set; }
    }
}
