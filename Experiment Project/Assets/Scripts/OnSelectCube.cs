using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelectCube : MonoBehaviour {

    private void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
    public void OnSelect()
    {
        GetComponent<Rigidbody>().useGravity = false;
      
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }

    public void OnDeSelect()
    {
        GetComponent<Rigidbody>().useGravity = false;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }
}
