using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    public static DontDestroyOnLoad Instance;
	// Use this for initialization
	void Awake ()
    {
        //
        //DontDestroyOnLoad (gameObject);

        

         //   if (Instance == null)
          //  {
           //     Instance = this;
            //}
           // else
           // {
           //     DestroyObject(gameObject);
           // }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
