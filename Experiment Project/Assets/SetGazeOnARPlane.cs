using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetGazeOnARPlane : MonoBehaviour {

    public GameObject ReticleTransform, FakeRecticleImage;
    public Image ReticleImage;
   
    public Vector3 OrginalPosition;
    public LayerMask mask;
   
    public Vector3 OrginalRotation;

    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update ()
    {
        SetPosition();
    }

    public void SetPosition()
    {

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hitInfo,100F, mask))
        {
          
            ReticleTransform.transform.position = hitInfo.point;
            ReticleTransform.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
            FakeRecticleImage.SetActive(false);
            ReticleTransform.SetActive(true);

        }
        else
        {
            FakeRecticleImage.SetActive(true);
            ReticleTransform.SetActive(false);




        }
    }
}
