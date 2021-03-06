﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;
using UnityEngine.EventSystems;

public class PlaceOnPlane : ARBase
{
    [SerializeField]
    private Transform m_ObjectToPlace;
    public GameObject TestCube;

    public bool OnePlay = true;
	public void PlaneDeSelect()
	{
		gameObject.GetComponent<PlaceOnPlane> ().enabled = true;

	}
    public void PlaneSelect()
    {
		gameObject.GetComponent<PlaceOnPlane> ().enabled = false;


    }
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
                if (IsPointerOverUIObject())
                    return;
                    m_ObjectToPlace.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    m_ObjectToPlace.gameObject.GetComponent<BoxCollider>().enabled = true;
                    m_ObjectToPlace.transform.position = rayHit.point;
                    m_ObjectToPlace.transform.rotation = rayHit.transform.rotation;
                
            }
        }
    }

    public void PlaceObject()
    {
        if (m_ObjectToPlace.gameObject.GetComponent<MeshRenderer>().enabled == true)
        {
            GameObject clone = Instantiate(TestCube, m_ObjectToPlace.GetComponent<MeshRenderer>().bounds.center, m_ObjectToPlace.transform.rotation);
            clone.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            GameObject GetParentObject = GameObject.FindGameObjectWithTag("TopCubeParent");
            clone.transform.parent = GetParentObject.transform;
            //Destroy();
            m_ObjectToPlace.gameObject.GetComponent<MeshRenderer>().enabled = false;
            m_ObjectToPlace.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
