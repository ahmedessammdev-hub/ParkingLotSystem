namespace ParkingLotSystem.API.Exceptions;

public class TicketAlreadyClosedException : Exception
{
    public TicketAlreadyClosedException(string message) : base(message) { }
}
