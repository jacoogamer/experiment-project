using UnityEngine;

public class ARPlaneFind : MonoBehaviour
{
    public GameObject TestCube;
    private GameObject GetParentObject;
    void Start ()
    {
		
	}
	
	
	void Update ()
    {
       
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "ArPlane")
                    {
                        Debug.Log("ArPlane ");
                        GameObject clone = Instantiate(TestCube, hit.point, Quaternion.identity);
                        GetParentObject = GameObject.FindGameObjectWithTag("TopCubeParent");
                        clone.transform.parent = GetParentObject.transform;

                    }
                }
            }
        }


    }
}

