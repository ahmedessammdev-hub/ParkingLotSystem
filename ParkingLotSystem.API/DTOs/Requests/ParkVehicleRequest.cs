using System.ComponentModel.DataAnnotations;
using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.DTOs.Requests;

public class ParkVehicleRequest
{
    [Required(ErrorMessage = "License plate is required")]
    [MaxLength(20, ErrorMessage = "License plate max 20 characters")]
    public string LicensePlate { get; set; } = string.Empty;

    [Required(ErrorMessage = "Owner name is required")]
    [MaxLength(100, ErrorMessage = "Owner name max 100 characters")]
    public string OwnerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vehicle type is required")]
    public VehicleType VehicleType { get; set; }

    [Required(ErrorMessage = "Fee type is required")]
    public FeeType FeeType { get; set; }
}
