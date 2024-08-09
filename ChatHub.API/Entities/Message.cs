using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatHub.API.Entities;

public class Message
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    public string Sentiment { get; set; } = null!;
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }

    [Required]
    public DateTime MessageTime { get; set; }
    
    [ForeignKey(nameof(Chat.Name))]
    public int ChatId { get; set; }

    public string ChatName { get; set; } = null!;
}