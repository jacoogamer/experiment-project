using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActiveItSelf : MonoBehaviour
{
	public GameObject MenuList, MenuButton;
	public void OpenMenuList()
	{
		MenuList.SetActive (true);
	}

	void Update()
	{
		if (MenuList.activeSelf == true)
			MenuButton.SetActive (false);
		else
			MenuButton.SetActive (true);
	}
}
