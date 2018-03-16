using UnityEngine;
using System.Collections;

public class GetGPS : MonoBehaviour
{
    private string GPSLocation;
    // Use this for initialization
    public UnityEngine.UI.Text GPSText,ErrorGPS;

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            ErrorGPS.text = "GPS Location data is not enabled! Please enable it!";
            Debug.Log("GPS Location data is not enabled! Please enable it!");
            yield break;
        }

        Input.location.Start(1F,0.1F);
        int maximumWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maximumWait > 0)
        {
            yield return new WaitForSeconds(1);
            maximumWait--;
        }

        if (maximumWait < 1)
        {
            ErrorGPS.text = "GPS Timed out";
            Debug.Log("GPS Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            ErrorGPS.text = "GPS Unable to determine device location";
            Debug.Log("GPS Unable to determine device location");
            yield break;
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        GPSLocation = "Lat: " + Input.location.lastData.latitude + " Long: " + Input.location.lastData.longitude + "\nAltitude: " +
            Input.location.lastData.altitude + " horizontal Accuracy: " + Input.location.lastData.horizontalAccuracy + "\nTimestamp: " + Input.location.lastData.timestamp;
        GPSText.text = GPSLocation;
    }

    
}