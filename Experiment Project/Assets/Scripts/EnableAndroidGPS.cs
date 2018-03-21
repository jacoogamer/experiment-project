using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndroidGPS : MonoBehaviour
{

    private void Awake()
    {
#if UNITY_ANDROID
        UniAndroidPermission.IsPermitted(AndroidPermission.ACCESS_FINE_LOCATION);
        RequestPermission();
#endif

    }

    public void RequestPermission()
    {
        UniAndroidPermission.RequestPermission(AndroidPermission.ACCESS_FINE_LOCATION, OnAllow, OnDeny, OnDenyAndNeverAskAgain);
    }

    private void OnAllow()
    {
        // OnAllow action that uses permitted function.
      
       
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
