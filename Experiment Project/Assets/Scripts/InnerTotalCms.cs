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


       

        float mshfloat = msh.bounds.size.x * 100f;
        InnTotalCms.text = mshfloat.ToString("f1") + "m";

        emptyGameObject = new GameObject();
        emptyGameObject.AddComponent<TextMesh>();
        emptyGameObject.AddComponent<LookAtCamera>();
        emptyGameObject.GetComponent<TextMesh>().anchor = TextAnchor.UpperCenter;
        emptyGameObject.GetComponent<TextMesh>().color = Color.red;
        emptyGameObject.GetComponent<TextMesh>().alignment = TextAlignment.Left;
        emptyGameObject.GetComponent<TextMesh>().characterSize = Vector3.Distance(Camera.main.transform.position, ZPosMeasurment.transform.position) / 50;
        emptyGameObject.GetComponent<TextMesh>().text = mshfloat.ToString("f1") + "m";
        
        emptyGameObject.GetComponent<TextMesh>().transform.position = new Vector3(msh.bounds.center.x, ZPosMeasurment.transform.position.z);
        
        emptyGameObject.GetComponent<TextMesh>().transform.position = Vector3.Lerp(LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(1), LineRend.GetComponent<SetGazeOnARPlane>().clonels.GetComponent<LineRenderer>().GetPosition(3), 0.6f);

    }


   
    void Update()
    {
        if (EnableMeasurement == true)
        {
            CalculateInnerArea();
            EnableMeasurement = false;
            GetMeasurement.GetComponent<SetGazeOnARPlane>().ListOfVectors.Clear();
            GetMeasurement.GetComponent<SetGazeOnARPlane>().numberOfLines = 0;

           
        }

        if (GetMeasurement.GetComponent<SetGazeOnARPlane>().numberOfLines >= 1 && OnePlay == true)
        {
            LineRend.GetComponent<SetGazeOnARPlane>().MeasureButtonOff = false;
        }

            if (GetMeasurement.GetComponent<SetGazeOnARPlane>().numberOfLines >= 4 && ZPosMeasurment.GetComponent<CheckIFInnerBoxCollide>().BoxCollideTouching == true)
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


        LineRend.GetComponent<SetGazeOnARPlane>().MeasureButtonOff = true;
        OnePlay = true;


        EnableMeasurement = true;

        
    }
}

