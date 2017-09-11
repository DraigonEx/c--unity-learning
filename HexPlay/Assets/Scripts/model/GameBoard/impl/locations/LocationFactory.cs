using UnityEngine;

class LocationFactory
{

    private LocationFactory() { }

    /// <summary>
    /// This method will return a random playable location - NOT 0:NONE or 1:CITY
    /// </summary>
    /// <returns></returns>
    public static ILocation getRandomPlayableLocation() {
        return getLocation((Locations)Random.Range(2,System.Enum.GetNames(typeof(Locations)).Length));
    }

    public static ILocation getLocation(Locations locationType)
    {
        ILocation returnValue;

        switch (locationType)
        {
            case Locations.CITY:
                returnValue = new City();
                break;
            case Locations.HARBOR:
                returnValue = new Harbor();
                break;
            case Locations.TOWER:
                returnValue = new Tower();
                break;
            case Locations.OASIS:
                returnValue = new Oasis();
                break;
            case Locations.BARN:
                returnValue = new Barn();
                break;
            case Locations.TAVERN:
                returnValue = new Tavern();
                break;
            case Locations.FARM:
                returnValue = new Farm();
                break;
            case Locations.ORACLE:
                returnValue = new Oracle();
                break;
            case Locations.PADDOCK:
                returnValue = new Paddock();
                break;
            default:
                returnValue = null;
                break;
        }

        return returnValue;
    }
}
