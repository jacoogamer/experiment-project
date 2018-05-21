using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using Lean.Touch;
using Aidar_;
using Aidar_.StreamingAssets;
using Aidar_.StreamingAssets.ZipArchive;
using System;
public class AddStreamingAssetsContent : MonoBehaviour
{

	public GameObject StreamingButtonPrefab;
	WWW wwwImg;
	DirectoryInfo directoryInfo;
	public GameObject MenuList;
	private Texture2D Text2D;

	string[] paths;byte[] data;

	public GameObject[] GetAllObjects;
	public InputField RefineListInput; 

	//

	int count = 0;
	void Start ()
	{

		Aidar_StreamingAssets.Initialize();
		paths = Aidar_StreamingAssets.GetFiles("Sprites", "*.jpg", SearchOption.AllDirectories).
			Concat(Aidar_StreamingAssets.GetFiles("Sprites", "*.png", SearchOption.AllDirectories)).ToArray(); 

		foreach(string path in paths)
		{
			Debug.Log (path);
			data = Aidar_StreamingAssets.ReadAllBytes(path);
			Text2D = new Texture2D (512,512);
			Text2D.LoadImage (data);
			Text2D.Apply ();
			var s = path;
			var repath = s.Substring(s.IndexOf("/") + 1);
			AddButtons (repath, Text2D);
		}

		RefineListInput.onValueChanged.AddListener
		(
			delegate 
			{
				RefineListByInput (RefineListInput.text);
			}
		);
	}



	[SerializeField]
	private int NumberOfCharsShow;
	void AddButtons (string objectName, Texture2D texture)
	{
		

		GameObject UnderContent = Instantiate (StreamingButtonPrefab);
		UnderContent.transform.parent = gameObject.transform;

		//Get string replace . extersions then set number of characters by linq
		string objectNameRemoveString = System.Text.RegularExpressions.Regex.Replace (objectName, "[.jpg|.png]", "");
		objectNameRemoveString = new string(objectNameRemoveString.Take(NumberOfCharsShow).ToArray());
		UnderContent.name = objectNameRemoveString;
		UnderContent.GetComponentInChildren<Text> ().text = objectNameRemoveString.ToString ();

		UnderContent.GetComponent<Image> ().sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), Vector2.zero);
		UnderContent.GetComponent<RectTransform> ().localScale = new Vector3 (1F, 1F, 1F);
		UnderContent.GetComponent<Button> ().onClick.AddListener (delegate {
			CreateMesh (objectName);

		});


	}

	void CreateMesh (string MeshName)
	{
		
		Debug.Log (MeshName);
		string MeshNameReplace = System.Text.RegularExpressions.Regex.Replace (MeshName, "[.jpg|.png]", "");
		Debug.Log (MeshNameReplace);
		StartCoroutine (loadAsset (MeshNameReplace, MeshNameReplace));

	}



	public Text TextDebug;

	IEnumerator loadAsset (string assetBundleName, string objectNameToLoad)
	{
		var assetBundleCreateRequest = Aidar_StreamingAssets.LoadAssetBundleAsync("AssetBundles/" + objectNameToLoad.ToLower());
		yield return assetBundleCreateRequest;

		AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

		AssetBundleRequest asset = asseBundle.LoadAssetAsync<GameObject> (objectNameToLoad.ToLower());
		yield return asset;

		GameObject loadedAsset = asset.asset as GameObject;
		GameObject obj = (GameObject)Instantiate (loadedAsset);
		GameObject GetParentObject = GameObject.FindGameObjectWithTag ("TopCubeParent");
		obj.transform.parent = GetParentObject.transform;
		obj.AddComponent<LeanRotate> ();
		obj.GetComponent<LeanRotate>().RotateAxis = new Vector3(0f,-1f,0f);
		obj.AddComponent<LeanSelectable> ();
		obj.AddComponent<LeanTranslate> ();
		obj.AddComponent<LeanSelectableRendererColor> ();
		obj.AddComponent<BoxCollider> ();
		//Add button under button
		GameObject CloseButton = (GameObject)Instantiate(Resources.Load("DestroyBtnTopObj"));
		CloseButton.transform.parent = obj.transform;
		CloseButton.AddComponent<DestroyParentCube> ();
		//
	
			obj.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2f;
		asseBundle.Unload (false);
		MenuList.SetActive (false);
	}


	private void RefineListByInput(string inputValue)
	{
		foreach (Transform item in gameObject.GetComponentInChildren<Transform>()) 
		{
			if (item.gameObject.name.IndexOf (inputValue,StringComparison.OrdinalIgnoreCase) != -1)
			{
				item.gameObject.SetActive (true);	
			} else 
			{
				item.gameObject.SetActive (false);
			}

		}
	}

}
