using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InnerTotalCms : MonoBehaviour
{

    public GameObject GetMeasurement;
    public bool EnableMeasurement = false;
    public GameObject CalculateButtonObject;
    public GameObject SecondPos;
    public UnityEngine.UI.Text InnTotalCms;
    private GameObject emptyGameObject;
    public GameObject ZPosMeasurment,LineRend;
    private void Start()
    {
        CalculateButtonObject.SetActive(false);
        InnTotalCms.text = 0f.ToString();
    }

   

    private void CalculateInnerArea()
    {
 
        List<Vector2> TempList = GetMeasurement.GetComponent<SetGazeOnARPlane>().ListOfVectors;
        // Use the triangulator to get indices for creating triangles
        Vector2[] vertices2D = TempList.ToArray();
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();
        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }
        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        float result = 0;
        for (int p = msh.vertices.Length - 1, q = 0; q < msh.vertices.Length; p = q++)
        {
            result += (Vector3.Cross(msh.vertices[q], msh.vertices[p])).magnitude;
        }

       

        float mshfloat = result * 0.5f;

        
        // float mshfloat = msh.bounds.size.x * msh.bounds.size.x;
        //  InnTotalCms.text = mshfloat.ToString("f1") + "m";

        emptyGameObject = new GameObject();
        emptyGameObject.AddComponent<TextMesh>();
        emptyGameObject.AddComponent<LookAtCamera>();
        emptyGameObject.GetComponent<TextMesh>().anchor = TextAnchor.UpperCenter;
        emptyGameObject.GetComponent<TextMesh>().color = Color.red;
        emptyGameObject.GetComponent<TextMesh>().alignment = TextAlignment.Left;
        emptyGameObject.GetComponent<TextMesh>().characterSize = Vector3.Distance(Camera.main.transform.position, ZPosMeasurment.transform.position) / 55;
		emptyGameObject.GetComponent<TextMesh>().text = mshfloat.ToString("f2") + "m2";
		emptyGameObject.GetComponent<TextMesh> ().anchor = TextAnchor.UpperCenter;
        emptyGameObject.GetComponent<TextMesh>().transform.position =
            Vector3.Lerp(new Vector3(LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(0).x, LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(0).y + 0.1f, LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(0).z),
            new Vector3(LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(2).x, LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(2).y + 0.1f, LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(2).z), 0.6f);



    }


   
    void Update()
    {
        if (EnableMeasurement == true)
        {
            LineRend.GetComponent<SetGazeOnARPlane>().totalcms += LineRend.GetComponent<SetGazeOnARPlane>().rawSize;

            LineRend.GetComponent<SetGazeOnARPlane>().ListOfVectors.Add(LineRend.GetComponent<SetGazeOnARPlane>().PosFirst2.transform.position);
            LineRend.GetComponent<SetGazeOnARPlane>().clickNum = 0;


            CalculateInnerArea();
            EnableMeasurement = false;
            GetMeasurement.GetComponent<SetGazeOnARPlane>().ListOfVectors.Clear();
            GetMeasurement.GetComponent<SetGazeOnARPlane>().numberOfLines = 0;

           
        }

        if (GetMeasurement.GetComponent<SetGazeOnARPlane>().numberOfLines >= 1 && OnePlay == true)
        {
            LineRend.GetComponent<SetGazeOnARPlane>().MeasureButtonOff = false;
        }

            if (GetMeasurement.GetComponent<SetGazeOnARPlane>().numberOfLines >= 3 && ZPosMeasurment.GetComponent<CheckIFInnerBoxCollide>().BoxCollideTouching == true)
        {

            CalculateButtonObject.SetActive(true);
        }
        else
        {
            CalculateButtonObject.SetActive(false);
        }

        
    }

    public bool OnePlay = false;
    public void CalculateButton()
    {


        //LineRend.GetComponent<SetGazeOnARPlane>().MeasureButtonOff = true;
        OnePlay = true;
       
        EnableMeasurement = true;

        
    }
}

