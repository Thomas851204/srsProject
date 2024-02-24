using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace srsProject.Models;

public class Ownership {
    public int Id { get; set; }

    [Required]
    [ForeignKey("Owner")]
    public int OwnerId {get; set;}
    public virtual required Owner Owner { get; set; }

    [Required]
    [ForeignKey("Car")]
    public int CarId {get; set;}
    public virtual required Car Car { get; set; }
    
}