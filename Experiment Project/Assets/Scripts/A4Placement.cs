using UnityEngine;
using UnityEngine.UI;
public class A4Placement : MonoBehaviour
{

    public GameObject ReticleTransform, FakeRecticleImage;
    public Image ReticleImage;
    public Sprite Square, TargetIcon;
    public LayerMask mask;
   

    void Start ()
    {
		
	}
	
	void Update ()
    {
        SetPosition();
    }

    public void SetPosition()
    {
        RaycastHit hitInfo;
        if ((Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, float.MaxValue, mask)))
        {           
           
            FakeRecticleImage.GetComponent<Image>().sprite = TargetIcon;
            FakeRecticleImage.transform.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);
        }

        else
        {
            FakeRecticleImage.GetComponent<Image>().sprite = Square;
            FakeRecticleImage.transform.localScale = new Vector3(0.00005f, 0.00005f, 0.00005f);
        }

    }
}
