using System.Collections;
using System.Collections.Generic;
using UnityARInterface;
using UnityEngine;
using UnityEngine.UI;
public class SetGazeOnARPlane : ARBase
{

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

        
           
            if (emptyGameObject != null && clickNum == 0)
            {
            
                 t += Time.deltaTime / 10;
            
            if(t < 0.4)
                emptyGameObject.GetComponent<TextMesh>().transform.position = Vector3.Lerp(PosFirst2.transform.position, PosFirst.transform.position, t);
            }
        
        

    }
    private ParticleSystem.Particle[] m_Particles;
    public void SetPosition()
    {
        RaycastHit hitInfo;

        

            if ((Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, float.MaxValue,mask) || (clickNum == 2)))
            {
                MeasureButton.SetActive(true);
            
                NewMeasureButton.SetActive(true);
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
            NewMeasureButton.SetActive(false);


        }

    }

    public Vector3 Measurements;
    int clickNum = 0,clickplus = -1;
    public GameObject PosFirst2,PosFirst;
    
    public GameObject MeasureButton,NewMeasureButton;
    private GameObject clonels;
    public GameObject lrMeasument;
    private ARInterface.PointCloud m_PointCloud;
    
    public void NewBound()
    {
        OnePlay = false;
        clickNum = 1;
        clickplus = 0;
    }

    public void ClickOnMeasurmentButton()
    {
        if (clickNum == 0)
        {
            clickNum = 1;
            ++clickplus;
        }
        else if (clickNum == 2)
            clickNum = 0;

       

        Debug.Log(clickplus);
    }

    RaycastHit hitInfo;
    RaycastHit AllhitsInfo;
    public bool OnePlay = false,SecondOnePlay = false;
    GameObject emptyGameObject;
    int postioncounts = 1;
    float rawSize;
    private float t;
    
    public void CalculateSize()
    {

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, float.MaxValue,mask))
            {

                if (clickNum == 1)
                {

                t = 0;
                PosFirst.transform.position = hitInfo.point;

                SecondOnePlay = false;

                clickNum = 2;
                if (OnePlay == false)
                {
                    clonels = Instantiate(lrMeasument, hitInfo.point, Quaternion.identity);
                    postioncounts = 2;
                    clonels.GetComponent<LineRenderer>().positionCount = postioncounts;

                    clonels.SetActive(false);
                    OnePlay = true;
                }
                else
                {
                    postioncounts = postioncounts + 1;
                    clonels.GetComponent<LineRenderer>().positionCount = postioncounts;
                }
                
                if (clickplus == 0)
                {
                    clonels.GetComponent<LineRenderer>().SetPosition(0, PosFirst.transform.position);
                    ++clickplus;
                }
                else
                {
                    clonels.GetComponent<LineRenderer>().SetPosition(clickplus, PosFirst.transform.position);
                    
                }
            }

            }




        if (clickNum == 2)
        {

            PosFirst2.transform.position = hitInfo.point;


            rawSize = Vector3.Distance(PosFirst.transform.position, PosFirst2.transform.position);
            Debug.Log(rawSize = rawSize * 100);
           
            
            //Debug.Log((rawSize = rawSize * 100) + "rawSize");
            //clonels.GetComponentInChildren<TextMesh>().text = "(" +  CovertData.x.ToString("f1") + "cm Width," + CovertData.y.ToString("f1") + "cm Height,"+ CovertData.z.ToString("f1") + "cm Lenght)";
            // clonels.GetComponentInChildren<TextMesh>().text = rawSize.ToString("f1") + "cm";
            if (SecondOnePlay == false)
            {
                emptyGameObject = new GameObject();
                emptyGameObject.transform.parent = clonels.transform;
                emptyGameObject.AddComponent<TextMesh>();
                emptyGameObject.AddComponent<LookAtCamera>();
                emptyGameObject.GetComponent<TextMesh>().anchor = TextAnchor.UpperCenter;
                emptyGameObject.GetComponent<TextMesh>().alignment = TextAlignment.Left;
                emptyGameObject.GetComponent<TextMesh>().characterSize = Vector3.Distance(Camera.main.transform.position, PosFirst2.transform.position) / 30;

                SecondOnePlay = true;

               
            }

             
            clonels.SetActive(true);
            if (clickplus == 1)
            {
                
                clonels.GetComponent<LineRenderer>().SetPosition(1, PosFirst2.transform.position);
               
            }
            else
            {
               
                clonels.GetComponent<LineRenderer>().SetPosition(clickplus, PosFirst2.transform.position);
                

            }

           

            emptyGameObject.GetComponent<TextMesh>().text = rawSize.ToString("f1") + "cm";
            //emptyGameObject.GetComponent<TextMesh>().transform.position = Vector3.Lerp(PosFirst2.transform.position, PosFirst.transform.position, t);
            emptyGameObject.GetComponent<TextMesh>().transform.position = PosFirst2.transform.position;
        }




    }
}
