namespace ParkingLotSystem.API.Exceptions;

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException(string message) : base(message) { }
}
