using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ARGridRealWorldScale : MonoBehaviour {



	public float XvalueCM = 100, YvalueCM = 100, ZvalueCM = 100;

	void Start ()
	{
		

	}


	public void Resize ()
	{


		this.gameObject.transform.localScale = new Vector3(XvalueCM / 100F, 
			YvalueCM / 100F, 
			ZvalueCM / 100F);

	}
}
