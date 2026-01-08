public class ParkingManagement
{
    private readonly Dictionary<string, DateTime> parkedCars = new Dictionary<string, DateTime>();
    private const decimal HourlyRate = 2.50m;

    public void ParkCar(string licensePlate)
    {
        if (!parkedCars.ContainsKey(licensePlate))
        {
            parkedCars[licensePlate] = DateTime.Now;
        }
        else
        {
            throw new InvalidOperationException("Car is already parked.");
        }
    }

    public decimal UnparkCar(string licensePlate)
    {
        if (parkedCars.TryGetValue(licensePlate, out DateTime parkTime))
        {
            DateTime unparkTime = DateTime.Now;
            TimeSpan duration = unparkTime - parkTime;
            parkedCars.Remove(licensePlate);

            decimal totalFee = (decimal)duration.TotalHours * HourlyRate;
            return Math.Ceiling(totalFee); // Round up to the nearest whole number
        }
        else
        {
            throw new InvalidOperationException("Car is not parked.");
        }
    }

    public bool IsCarParked(string licensePlate)
    {
        return parkedCars.ContainsKey(licensePlate);
    }
}