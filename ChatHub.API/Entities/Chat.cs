using System.ComponentModel.DataAnnotations;

namespace ChatHub.API.Entities;

public class Chat
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<Message> Messages { get; set; }
        = new List<Message>();
}
