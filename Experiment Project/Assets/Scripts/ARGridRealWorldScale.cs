using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ARGridRealWorldScale : MonoBehaviour {



	private InputField Xvalue, Yvalue, Zvalue;

	void Start ()
	{
		

	}


	public void Resize ()
	{


		this.gameObject.transform.localScale = new Vector3(float.Parse(GameObject.FindGameObjectWithTag("Xvalue").GetComponent<InputField>().text) / 100f, 
			float.Parse(GameObject.FindGameObjectWithTag("Yvalue").GetComponent<InputField>().text) / 100f, 
			float.Parse(GameObject.FindGameObjectWithTag("Zvalue").GetComponent<InputField>().text) / 100f);

	}
}
