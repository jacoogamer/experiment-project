using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CaptureScreenshots : MonoBehaviour {

    public GameObject AllUI;
    public bool WithUI = false;
    public int filenum;
	private void Start ()
    {
        filenum = PlayerPrefs.GetInt("filenum", 0);
	}
	
	// Update is called once per frame
	private void Update ()
    {
		
	}

    public void CaptureClick()
    {
        StartCoroutine(CaptureScreenShotCoroutine());
    }

    private IEnumerator CaptureScreenShotCoroutine()
    {
        //Enable or disable ui based on bool withui
        if (WithUI == false)
            AllUI.SetActive(false);
        else
            AllUI.SetActive(true);
        yield return new WaitForEndOfFrame();


        GameObject[] CheckARObjects = GameObject.FindGameObjectsWithTag("ARObjects");
        string screenshotname;
        if (CheckARObjects.Length == 0)
        {
            screenshotname = "/Screenshot";
        }
        else
        {
            screenshotname = "/ScreenshotAR";
        }
        string path = Application.persistentDataPath + screenshotname + ++filenum + ".png";

        PlayerPrefs.SetInt("filenum", filenum);
        PlayerPrefs.Save();

        Debug.Log(path);
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
       
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        for (int i = 0; i < 15; i++)
        {
            yield return null;
        }
        screenImage.Apply();
        for (int i = 0; i < 15; i++)
        {
            yield return null;
        }
        byte[] imageBytes = screenImage.EncodeToPNG();
       
        
        for (int i = 0; i < 15; i++)
        {
            yield return null;
        }
        new System.Threading.Thread(() =>
        {
            System.Threading.Thread.Sleep(100);
            File.WriteAllBytes(path, imageBytes);
        }).Start();

        //Enable ui again
        AllUI.SetActive(true);
    }
}
