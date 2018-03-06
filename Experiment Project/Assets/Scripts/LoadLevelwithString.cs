using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelwithString : MonoBehaviour
{
    public void LoadLevelWithString(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
	
}
