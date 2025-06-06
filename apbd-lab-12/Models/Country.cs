namespace apbd_lab_12.Models;

public class Country
{
    public int IdCountry { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<CountryTrip> TripCountries { get; set; }
}