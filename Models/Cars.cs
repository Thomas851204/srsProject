using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace srsProject.Models;

public class Car {
    public int Id { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required string LicensePlate { get; set; }

    [DataType(DataType.Date)]
    public required DateTime ProductionDate { get; set; }
    
}