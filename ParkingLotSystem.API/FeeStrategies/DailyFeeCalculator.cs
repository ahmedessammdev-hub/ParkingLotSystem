using ParkingLotSystem.API.Interfaces;

namespace ParkingLotSystem.API.FeeStrategies;

public class DailyFeeCalculator : IFeeCalculator
{
    private const decimal RatePerDay = 500m;

    public decimal Calculate(DateTime entryTime, DateTime exitTime)
    {
        var days = Math.Ceiling((exitTime - entryTime).TotalDays);
        return (decimal)days * RatePerDay;
    }
}