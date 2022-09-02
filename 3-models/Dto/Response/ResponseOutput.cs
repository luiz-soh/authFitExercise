namespace Models.Dto.Error
{
    public class ErrorOutput
    {
        public string Message { get; set; }

        public ErrorOutput(string message)
        {
            Message = message;
        }

        public ErrorOutput()
        {
            Message = string.Empty;
        }
    }
}
