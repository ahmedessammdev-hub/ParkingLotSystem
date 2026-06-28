using ParkingLotSystem.API.DTOs.Requests;
using ParkingLotSystem.API.DTOs.Responses;

namespace ParkingLotSystem.API.Interfaces;

public interface IParkingService
{
    Task<ParkingTicketResponse> ParkVehicleAsync(ParkVehicleRequest request);
    Task<ParkingTicketResponse> ExitVehicleAsync(int ticketId);
    Task<IEnumerable<ActiveVehicleResponse>> GetActiveVehiclesAsync(int pageNumber, int pageSize);
    Task<ParkingTicketResponse> GetTicketByIdAsync(int ticketId);
    Task<IEnumerable<ParkingTicketResponse>> GetVehicleHistoryAsync(string licensePlate);
}
