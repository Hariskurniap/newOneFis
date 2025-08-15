namespace Application.Settings;

public class SapSynchSettings
{
    public string TokenUrl { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string GrantType { get; set; } = null!;
    public string ListDOUrl { get; set; } = null!;
    public string DetailDOUrl { get; set; } = null!;
    public string CustomerUrl { get; set; } = string.Empty;
    public string GiShipmentUrl { get; set; } = null;
    public string ShipmentUrl { get; set; } = null;

}

