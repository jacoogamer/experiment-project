using System.Collections;
using System.Collections.Generic;
using UnityARInterface;
using UnityEngine;
using UnityEngine.UI;
public class SetGazeOnARPlane : MonoBehaviour {

    public GameObject ReticleTransform, FakeRecticleImage;
    public Image ReticleImage;
   
    public Vector3 OrginalPosition;
    public LayerMask mask;
   
    public Vector3 OrginalRotation;
    public Sprite Square, TargetIcon;
   
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

        if ((Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, float.MaxValue, mask) || (clickNum == 2)))
        {
            MeasureButton.SetActive(true);
            // ReticleTransform.transform.position = hitInfo.point;
            //ReticleTransform.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
            //FakeRecticleImage.SetActive(true);
            //ReticleTransform.SetActive(true);
            FakeRecticleImage.GetComponent<Image>().sprite = TargetIcon;
            FakeRecticleImage.transform.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);
            // FakeRecticleImage.SetActive(true);
        }

        else
        {


            FakeRecticleImage.GetComponent<Image>().sprite = Square;
            FakeRecticleImage.transform.localScale = new Vector3(0.00005f, 0.00005f, 0.00005f);
            MeasureButton.SetActive(false);



        }

    }

    public Vector3 Measurements;
    int clickNum = 0;
    public GameObject PosFirst2,PosFirst;
    
    public GameObject MeasureButton;
    private GameObject clonels;
    public GameObject lrMeasument;
    private ARInterface.PointCloud m_PointCloud;
    public void ClickOnMeasurmentButton()
    {
        if (clickNum == 0)
            clickNum = 1;
        else if (clickNum == 2)
            clickNum = 0;
    }

    RaycastHit hitInfo;
    RaycastHit AllhitsInfo;
    
    public void CalculateSize()
    {

       

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, float.MaxValue, mask))
        {

            if (clickNum == 1)
            {

                PosFirst.transform.position = hitInfo.point;

                

                clickNum = 2;
                clonels = Instantiate(lrMeasument, hitInfo.point, Quaternion.identity);
                clonels.SetActive(false);
                clonels.GetComponent<LineRenderer>().SetPosition(0, PosFirst.transform.position);
            }

        }




        if (clickNum == 2)
        {

            PosFirst2.transform.position = hitInfo.point;


            float rawSize = Vector3.Distance(PosFirst.transform.position, PosFirst2.transform.position);


            Debug.Log((rawSize = rawSize * 100) + "rawSize");
            //clonels.GetComponentInChildren<TextMesh>().text = "(" +  CovertData.x.ToString("f1") + "cm Width," + CovertData.y.ToString("f1") + "cm Height,"+ CovertData.z.ToString("f1") + "cm Lenght)";
            clonels.GetComponentInChildren<TextMesh>().text = rawSize.ToString("f1") + "cm";

            clonels.GetComponentInChildren<TextMesh>().transform.position = clonels.GetComponent<LineRenderer>().bounds.center;

            clonels.SetActive(true);
            clonels.GetComponent<LineRenderer>().SetPosition(1, PosFirst2.transform.position);

        }




    }
}
