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

public class AddStreamingAssetsContent : MonoBehaviour
{

	public GameObject StreamingButtonPrefab;
	WWW wwwImg;
	DirectoryInfo directoryInfo;
	public GameObject MenuList;
	private Texture2D Text2D;
	void Start ()
	{

		Aidar_StreamingAssets.Initialize();
		string[] paths = Aidar_StreamingAssets.GetFiles("Sprites", "*.jpg", SearchOption.AllDirectories).
			Concat(Aidar_StreamingAssets.GetFiles("Sprites", "*.png", SearchOption.AllDirectories)).ToArray(); 

		foreach(string path in paths)
		{
			Debug.Log (path);
			byte[] data = Aidar_StreamingAssets.ReadAllBytes(path);
			Text2D = new Texture2D (512,512);
			Text2D.LoadImage (data);
			Text2D.Apply ();
			var s = path;
			var repath = s.Substring(s.IndexOf("/") + 1);
			AddButtons (repath, Text2D);
		}
	}




	void AddButtons (string objectName, Texture2D texture)
	{

		GameObject UnderContent = Instantiate (StreamingButtonPrefab);
		UnderContent.transform.parent = gameObject.transform;
		UnderContent.name = objectName;
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
		var assetBundleCreateRequest = Aidar_StreamingAssets.LoadAssetBundleAsync("AssetBundles/" + objectNameToLoad);
		yield return assetBundleCreateRequest;

		AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

		AssetBundleRequest asset = asseBundle.LoadAssetAsync<GameObject> (objectNameToLoad);
		yield return asset;

		GameObject loadedAsset = asset.asset as GameObject;
		GameObject obj = (GameObject)Instantiate (loadedAsset);
		GameObject GetParentObject = GameObject.FindGameObjectWithTag ("TopCubeParent");
		obj.transform.parent = GetParentObject.transform;
		obj.AddComponent<LeanRotate> ();
		obj.AddComponent<LeanSelectable> ();
		obj.AddComponent<LeanTranslate> ();
		obj.AddComponent<LeanSelectableRendererColor> ();
		obj.AddComponent<BoxCollider> ();
		obj.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y - 2f, Camera.main.transform.position.z + 5f);
		asseBundle.Unload (false);
		MenuList.SetActive (false);
	}

}
