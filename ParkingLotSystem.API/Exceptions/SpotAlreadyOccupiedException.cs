namespace ParkingLotSystem.API.Exceptions;

public class SpotAlreadyOccupiedException : Exception
{
    public SpotAlreadyOccupiedException(string message) : base(message) { }
}
