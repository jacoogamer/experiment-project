using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIFInnerBoxCollide : MonoBehaviour
{
    public bool BoxCollideTouching = false;
    private void Start()
    {
        BoxCollideTouching = false;
    }
    
    private void OnTriggerStay(Collider other)
{
        if(other.gameObject.tag == "FirstCollidePosition")
        {
            BoxCollideTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
{
       
        if (other.gameObject.tag == "FirstCollidePosition")
        {
            BoxCollideTouching = false;
        }
    }

}
