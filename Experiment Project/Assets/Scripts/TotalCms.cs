using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalCms : MonoBehaviour {

    public UnityEngine.UI.Text TotalCmsText;
    public GameObject SetGazeOnARPlane;
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        TotalCmsText.text = SetGazeOnARPlane.GetComponent<SetGazeOnARPlane>().totalcms.ToString("f1") + "cm";

    }
}
