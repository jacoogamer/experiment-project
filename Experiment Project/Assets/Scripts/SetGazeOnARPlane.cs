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
        lrMeasument.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        SetPosition();
        
    }

    public void SetPosition()
    {

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hitInfo,1000F, mask))
        {
            MeasureButton.SetActive(true);
            ReticleTransform.transform.position = hitInfo.point;
            ReticleTransform.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
            FakeRecticleImage.SetActive(false);
            ReticleTransform.SetActive(true);

        }
        else
        {
            FakeRecticleImage.SetActive(true);
            ReticleTransform.SetActive(false);
            


                MeasureButton.SetActive(false);
            


        }
    }

    public Vector3 Measurements;
    bool clickNum = true;
    public GameObject PosFirst2,PosFirst;
    public Text Measurement;
    public GameObject MeasureButton;
    private GameObject clonels;
    public GameObject lrMeasument;
    public void CalculateSize()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 1000F, mask))
        {
           
            if (clickNum == false)
                {
                    PosFirst2.transform.position = hitInfo.point;
                    Vector3 rawSize = PosFirst.transform.position - PosFirst2.transform.position;
                    Vector3 CovertData = new Vector3 (Mathf.Abs(rawSize.x * 10), Mathf.Abs(rawSize.y * 10), Mathf.Abs(rawSize.z * 10));
                clonels.SetActive(true);
                clonels.GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
                    Debug.Log(CovertData + "rawSize");
                    clickNum = true;
                    Measurement.text = CovertData.ToString();
                } else if (clickNum == true)
                {
                    PosFirst.transform.position = hitInfo.point;
                    clickNum = false;
                    clonels = Instantiate(lrMeasument, hitInfo.point,Quaternion.identity);
                clonels.SetActive(false);
                    clonels.GetComponent<LineRenderer>().SetPosition(0, hitInfo.point);
                }
        }
        

            


        

    }
}
