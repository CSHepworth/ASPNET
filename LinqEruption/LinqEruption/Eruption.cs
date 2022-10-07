public class Eruption
{
    public string Volcano {get;set;}
    public int Year {get;set;}
    public string Location {get;set;}
    public int ElevationInMeters {get;set;}
    public string Type {get;set;}
    public Eruption(string volcano, int year, string location, int elevationinmeters, string type)
    {
        Volcano = volcano;
        Year = year;
        Location = location;
        ElevationInMeters = elevationinmeters;
        Type = type;
    }
    public override string ToString()
    {
        return $@"
    Name: {Volcano}
    Year: {Year}
    Location: {Location}
    Elevation: {ElevationInMeters} meters
    Type: {Type}
        ";
    }
}