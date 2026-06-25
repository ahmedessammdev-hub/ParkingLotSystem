using ParkingLotSystem.API.Domain.Enums;
using ParkingLotSystem.API.Interfaces;

namespace ParkingLotSystem.API.FeeStrategies;

public static class FeeCalculatorFactory
{
    private static readonly Dictionary<FeeType, IFeeCalculator> _calculators =
        new()
        {
            { FeeType.Hourly, new HourlyFeeCalculator() },
            { FeeType.Daily,  new DailyFeeCalculator()  },
            { FeeType.Flat,   new FlatFeeCalculator()   }
        };

    public static IFeeCalculator GetCalculator(FeeType feeType)
    {
        if (!_calculators.TryGetValue(feeType, out var calculator))
            throw new ArgumentException($"No calculator found for fee type: {feeType}");

        return calculator;
    }
}
