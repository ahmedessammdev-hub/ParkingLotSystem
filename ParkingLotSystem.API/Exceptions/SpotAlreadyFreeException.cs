namespace ParkingLotSystem.API.Exceptions;

public class SpotAlreadyFreeException : Exception
{
    public SpotAlreadyFreeException(string message) : base(message) { }
}
