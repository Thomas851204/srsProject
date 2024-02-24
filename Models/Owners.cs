using System.ComponentModel.DataAnnotations;

namespace srsProject.Models;

public class Owner {
    public int Id { get; set; }
    public required string FirstName { get; set; }

    public required string LastName {get;set;}
    [DataType(DataType.Date)]
    public required DateTime BirthDate { get; set; }
    
}