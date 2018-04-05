using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ARSessionManager : MonoBehaviour
{

    public int ObjectsSessionNum;
    public GameObject A4SessionCube;
    public List<Vector3> Pos;
    public List<Vector3> Rot;




    public void LoadLastSession()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ARSessionCube"))
        {
            Destroy(obj);
        }

            Vector3[] PostListTemp = PlayerPrefsArray.GetVector3Array("PosList");
        Vector3[] RotListTemp = PlayerPrefsArray.GetVector3Array("RotList");
        Debug.Log(PostListTemp.Length);
        int count = PlayerPrefs.GetInt("ObjectsSessionNum",0);
        int i = 0;
        while (i < count)
        {
            GameObject clone = Instantiate(A4SessionCube);

            GameObject GetParentObject = GameObject.FindGameObjectWithTag("TopCubeParent");
            clone.transform.parent = GetParentObject.transform;
            clone.transform.localPosition = PostListTemp[i];
            clone.transform.localRotation = Quaternion.Euler(RotListTemp[i]);
            
           

            

           

            
            i++;
        }
    }
	
	public void  SaveSessionButton ()
    {

        Pos.Clear();
        Rot.Clear();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ARSessionCube"))
        {
            Pos.Add(obj.transform.localPosition);
            Rot.Add(obj.transform.eulerAngles);
        }

        PlayerPrefs.DeleteKey("ObjectsSessionNum");

        ObjectsSessionNum = FindObjectsOfType<ARSessionSave>().Length;
        Debug.Log(ObjectsSessionNum);

        PlayerPrefs.SetInt("ObjectsSessionNum", ObjectsSessionNum);
        PlayerPrefs.Save();

        //Save PosList using linq
        PlayerPrefsArray.SetVector3Array("PosList", Pos.ToArray());
        //Save RotList using linq
        PlayerPrefsArray.SetVector3Array("RotList", Rot.ToArray());
    }



    private void OnDestroy()
    {
        

       
    }
}
