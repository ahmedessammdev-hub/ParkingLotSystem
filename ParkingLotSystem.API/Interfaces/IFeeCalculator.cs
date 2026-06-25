namespace ParkingLotSystem.API.Interfaces;

public interface IFeeCalculator
{
    decimal Calculate(DateTime entryTime, DateTime exitTime);
}