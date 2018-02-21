using UnityEngine.UI;
using UnityEngine;
using UnityARInterface;
public class DebugPlaneParticles : MonoBehaviour {

    public Toggle DebugToggle;
    GameObject[] clone;
    public GameObject ArRoot;
    void Start ()
    {
        DebugToggle.isOn = false;

    }
	
	
	void Update ()
    {

        if(DebugToggle.isOn == true)
        {
            clone = GameObject.FindGameObjectsWithTag("ArPlane");
            ArRoot.GetComponent<ARPointCloudVisualizer>().m_MaxPointsToShow = 100;
            foreach (GameObject obj in clone)
            {
                obj.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else if (DebugToggle.isOn == false)
        {
            clone = GameObject.FindGameObjectsWithTag("ArPlane");
            ArRoot.GetComponent<ARPointCloudVisualizer>().m_MaxPointsToShow = 0;
            foreach (GameObject obj in clone)
            {
                obj.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
