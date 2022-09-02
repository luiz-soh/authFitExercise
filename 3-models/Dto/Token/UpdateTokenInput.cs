using System.ComponentModel.DataAnnotations;

public class UpdateTokenInput
{
    [Required]
    public string RefreshToken { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    public UpdateTokenInput()
    {
        RefreshToken = string.Empty;
        UserId = 0;
    }
}