using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollItemManager : MonoBehaviour
{
    public GameObject ScrollListTop;
    public GameObject CloseButton, OpenButton;
    public GameObject[] ArrayOfScrollButtons;
    public GameObject CubePrefab,PlaceOnPlane;
    public Material CubeColor;
    public Animator ScrollViewTop;
    private void Start()
    {
        CloseButton.SetActive(false);
        OpenButton.SetActive(true);
       

        ScrollViewTop.SetBool("InScroll", false);

    }
    public void OpenMenu()
    {
        OpenButton.SetActive(false);
        ScrollViewTop.SetBool("InScroll", true);
        CloseButton.SetActive(true);
        
    }
    public void CloseMenu()
    {
        ScrollViewTop.SetBool("InScroll", false);
        OpenButton.SetActive(true);
        CloseButton.SetActive(false);
    }

    public Vector3 MainCamOffset;
    public void ClickSideMenuBtn(int BtnNum)
    {
         
        foreach (GameObject obj in ArrayOfScrollButtons)
        {
            
            obj.gameObject.GetComponent<Shadow>().enabled = false;
            
            
        }
        if (PlaceOnPlane.GetComponent<MeshRenderer>().enabled == true)
        {
            //GameObject clone = Instantiate(CubePrefab, Camera.main.transform.position + MainCamOffset, Quaternion.identity);
            GameObject clone = Instantiate(CubePrefab, PlaceOnPlane.GetComponent<MeshRenderer>().bounds.center, PlaceOnPlane.transform.rotation);
            PlaceOnPlane.GetComponent<MeshRenderer>().enabled = false;
            ArrayOfScrollButtons[BtnNum].GetComponent<Shadow>().enabled = true;
            CubeColor.color = ArrayOfScrollButtons[BtnNum].GetComponent<Image>().color;

            GameObject GetParentObject = GameObject.FindGameObjectWithTag("TopCubeParent");
            clone.transform.parent = GetParentObject.transform;
        }
    }

   
}
