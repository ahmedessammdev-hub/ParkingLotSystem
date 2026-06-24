using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotSystem.API.Domain.Entities;

[Table("ParkingLots")]
public class ParkingLot
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Address { get; set; } = string.Empty;

    public ICollection<Floor> Floors { get; set; } = new List<Floor>();
}