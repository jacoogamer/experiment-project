using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;

public class PlaceOnPlane : ARBase
{
    [SerializeField]
    private Transform m_ObjectToPlace;
    public GameObject TestCube;

    public bool OnePlay = true;
    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            var camera = GetCamera();

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

			int layerMask = 1 << LayerMask.NameToLayer("ARGameObject"); // Planes are in layer ARGameObject

            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, float.MaxValue, layerMask))
            {
                if (OnePlay == true)
                {
                    OnePlay = false;
                    m_ObjectToPlace.transform.position = rayHit.point;
                    m_ObjectToPlace.transform.rotation = rayHit.transform.rotation;
                }
            }
        }
    }

    public void PlaceObject()
    {
        GameObject clone = Instantiate(TestCube, m_ObjectToPlace.GetComponent<MeshRenderer>().bounds.center, m_ObjectToPlace.transform.rotation);
        clone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Destroy(m_ObjectToPlace.gameObject,5f);
    }
}
