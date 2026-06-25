using ParkingLotSystem.API.Interfaces;

namespace ParkingLotSystem.API.FeeStrategies;

public class HourlyFeeCalculator : IFeeCalculator
{
    private const decimal RatePerHour = 50m;

    public decimal Calculate(DateTime entryTime, DateTime exitTime)
    {
        var hours = Math.Ceiling((exitTime - entryTime).TotalHours);
        return (decimal)hours * RatePerHour;
    }
}