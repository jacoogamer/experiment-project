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
        CalculateSize();
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
    int clickNum = 0;
    public GameObject PosFirst2,PosFirst;
    
    public GameObject MeasureButton;
    private GameObject clonels;
    public GameObject lrMeasument;

    public void ClickOnMeasurmentButton()
    {
        if (clickNum == 0)
            clickNum = 1;
        else if (clickNum == 2)
            clickNum = 0;
    }


    public void CalculateSize()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 1000F, mask))
        {
           
            if (clickNum == 1)
                {
                    PosFirst.transform.position = hitInfo.point;
                    clickNum = 2;
                    clonels = Instantiate(lrMeasument, hitInfo.point,Quaternion.identity);
                clonels.SetActive(false);
                    clonels.GetComponent<LineRenderer>().SetPosition(0, hitInfo.point);
                }else if (clickNum == 2)
                {
                    PosFirst2.transform.position = hitInfo.point;
                    Vector3 rawSize = PosFirst.transform.position - PosFirst2.transform.position;
                    Vector3 CovertData = new Vector3(Mathf.Abs(rawSize.x * 10 * 2.54f), Mathf.Abs(rawSize.y * 10 * 2.54f), Mathf.Abs(rawSize.z * 10 * 2.54f));

                    Debug.Log(CovertData + "rawSize");
                clonels.GetComponentInChildren<TextMesh>().text = "(" +  CovertData.x.ToString("f1") + "cm Width," + CovertData.y.ToString("f1") + "cm Height,"+ CovertData.z.ToString("f1") + "cm Lenght)";
                    clonels.GetComponentInChildren<TextMesh>().transform.localPosition = Vector3.zero;
                
                clonels.SetActive(true);
                    clonels.GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
                   
                }
        }
        

            


        

    }
}
