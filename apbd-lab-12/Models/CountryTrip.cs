namespace apbd_lab_12.Models;

public class CountryTrip
{
    public int IdCountry { get; set; }
    public int IdTrip { get; set; }
    
    public virtual Trip Trip { get; set; }
    public virtual Country Country { get; set; }
}
