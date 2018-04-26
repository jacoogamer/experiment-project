using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateAssetBundle : MonoBehaviour 
{
    [MenuItem ("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles ()
    {
		#if UNITY_ANDROID
			BuildPipeline.BuildAssetBundles ("Assets/StreamingAssets/AssetBundles",BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.Android);
		#elif UNITY_IOS 
		BuildPipeline.BuildAssetBundles ("Assets/StreamingAssets/AssetBundles",BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.iOS);
		#endif

    }
}