using UnityEngine.UI;
using UnityEngine;
using UnityARInterface;
public class DebugPlaneParticles : MonoBehaviour {

    public Toggle DebugToggle;
    GameObject[] clone;
    public GameObject ArRoot;
    public Material YellowParticles;
    void Start ()
    {
        DebugToggle.isOn = false;

    }


    void Update()
    {

        if (DebugToggle.isOn == true)
        {
            clone = GameObject.FindGameObjectsWithTag("ArPlane");
            YellowParticles.SetColor("_TintColor", new Color32(255, 255, 0, 111));
            foreach (GameObject obj in clone)
            {
                obj.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else if (DebugToggle.isOn == false)
        {
            clone = GameObject.FindGameObjectsWithTag("ArPlane");
            YellowParticles.SetColor("_TintColor", new Color32(255, 255, 0, 0));
            foreach (GameObject obj in clone)
            {
                obj.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
