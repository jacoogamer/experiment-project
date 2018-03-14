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
        float mshfloat = msh.bounds.size.x * 1000f;
        InnTotalCms.text = mshfloat.ToString("f1") + "cm";
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

        if (GetMeasurement.GetComponent<SetGazeOnARPlane>().numberOfLines >= 3)
        {

            CalculateButtonObject.SetActive(true);
        }
        else
        {
            CalculateButtonObject.SetActive(false);
        }
    }

    public void CalculateButton()
    {
        EnableMeasurement = true;

    }
}

