using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GetGPS : MonoBehaviour
{
    private string GPSLocation;
    // Use this for initialization
    public Text GPSText,ErrorGPS;
    public GameObject EnableGPSPanel, ToggleGPS;
    public bool OnePlay = true;

    private void Awake()
    {
#if UNITY_ANDROID
        if (UniAndroidPermission.IsPermitted(AndroidPermission.ACCESS_FINE_LOCATION))
        {
            RequestPermission();
        }
#endif

    }
    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            EnableGPSPanel.SetActive(true);
            ErrorGPS.text = "GPS Location data is not enabled! Enable it or Go to App Permission add manually!";
            Debug.Log("GPS Location data is not enabled! Please enable it!");
            yield break;
        }else
        {
            EnableGPSPanel.SetActive(false);
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

        

        if(ToggleGPS.GetComponent<Toggle>().isOn == true)
        {
#if UNITY_ANDROID
            RequestPermission();
#endif
        }
    }

    public void RequestPermission()
    {
        UniAndroidPermission.RequestPermission(AndroidPermission.ACCESS_FINE_LOCATION, OnAllow, OnDeny, OnDenyAndNeverAskAgain);
    }

    private void OnAllow()
    {
        // OnAllow action that uses permitted function.
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
   }

    private void OnDeny()
    {
        // back screen / show warnking window

    }

    private void OnDenyAndNeverAskAgain()
    {
        // show warning window and open app permission setting page
    }

  

}


