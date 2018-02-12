using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollItemManager : MonoBehaviour
{
    public GameObject ScrollListTop;
    public GameObject CloseButton, OpenButton;
    public GameObject[] ArrayOfScrollButtons;
    public GameObject CubePrefab;
    public Material CubeColor;
    private void Start()
    {
        CloseButton.SetActive(false);
        OpenButton.SetActive(true);
        ScrollListTop.SetActive(false);

        
    }
    public void OpenMenu()
    {
        OpenButton.SetActive(false);
        ScrollListTop.SetActive(true);
        CloseButton.SetActive(true);
        
    }
    public void CloseMenu()
    {
        ScrollListTop.SetActive(false);
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
        GameObject clone = Instantiate(CubePrefab, Camera.main.transform.position + MainCamOffset, Quaternion.identity);
        ArrayOfScrollButtons[BtnNum].GetComponent<Shadow>().enabled = true;
        CubeColor.color = ArrayOfScrollButtons[BtnNum].GetComponent<Image>().color;

        GameObject GetParentObject = GameObject.FindGameObjectWithTag("TopCubeParent");
        clone.transform.parent = GetParentObject.transform;
    }

   
}
