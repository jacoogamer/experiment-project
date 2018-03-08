using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealWorldScale : MonoBehaviour {

    public GameObject Cube;

    public InputField Xvalue, Yvalue, Zvalue;

	void Start ()
    {


    }
	
	
	public void CreateCubeSizeable ()
    {
        var GameObject = Instantiate(Cube, new Vector3(0F, 0F, 6F), Quaternion.identity);
        
        GameObject.transform.localScale = new Vector3(float.Parse(Xvalue.text) / 100f, float.Parse(Yvalue.text) / 100f, float.Parse(Zvalue.text) / 100f);

        if (float.Parse(Zvalue.text) < 200)
            GameObject.transform.position = new Vector3(0F, 0F, 2F);
        else if(float.Parse(Zvalue.text) >= 200 && float.Parse(Zvalue.text) <= 400)
            GameObject.transform.position = new Vector3(0F, 0F, 3F);
        else if (float.Parse(Zvalue.text) >= 400 && float.Parse(Zvalue.text) <= 600)
            GameObject.transform.position = new Vector3(0F, 0F, 4F);
        else if (float.Parse(Zvalue.text) >= 600 && float.Parse(Zvalue.text) <= 800)
            GameObject.transform.position = new Vector3(0F, 0F, 5F);
        else if (float.Parse(Zvalue.text) >= 800 && float.Parse(Zvalue.text) <= 1000)
            GameObject.transform.position = new Vector3(0F, 0F, 7F);
        else if (float.Parse(Zvalue.text) == 1000)
            GameObject.transform.position = new Vector3(0F, 0F, 8F);
    }
}
