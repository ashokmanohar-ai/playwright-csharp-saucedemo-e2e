namespace PlaywrightSauceDemo.Tests.Models;

public sealed record CheckoutInfo(string FirstName, string LastName, string PostalCode)
{
    public static CheckoutInfo Default => new("Ashok", "Demo", "600037");
}
