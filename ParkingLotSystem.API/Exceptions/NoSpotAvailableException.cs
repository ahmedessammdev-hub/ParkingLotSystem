namespace ParkingLotSystem.API.Exceptions;

public class NoSpotAvailableException : Exception
{
    public NoSpotAvailableException(string message) : base(message) { }
}
