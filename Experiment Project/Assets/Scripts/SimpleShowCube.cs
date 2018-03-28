using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShowCube : MonoBehaviour {

    public GameObject Cube,ParentObject;
    
	void Start ()
    {
        
	}
	
	
	public void ShowCube ()
    {
        GameObject obj = Instantiate(Cube, new Vector3(Camera.main.transform.position.x,
        Camera.main.transform.position.y, Camera.main.transform.position.z + 2f), Quaternion.identity);

        GameObject GetParentObject = GameObject.FindGameObjectWithTag("TopCubeParent");
        obj.transform.parent = GetParentObject.transform;

    }
}
