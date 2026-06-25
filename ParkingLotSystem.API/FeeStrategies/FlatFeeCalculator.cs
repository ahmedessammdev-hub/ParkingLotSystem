using ParkingLotSystem.API.Interfaces;

namespace ParkingLotSystem.API.FeeStrategies;

public class FlatFeeCalculator : IFeeCalculator
{
    private const decimal FlatRate = 200m;

    public decimal Calculate(DateTime entryTime, DateTime exitTime)
    {
        return FlatRate;
    }
}