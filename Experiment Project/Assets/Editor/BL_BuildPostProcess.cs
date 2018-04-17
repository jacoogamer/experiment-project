using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Collections.Generic;
using PlistCS;

public class BL_BuildPostProcess
{

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {

        if (buildTarget == BuildTarget.iOS)
        {
            string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));

            string target = proj.TargetGuidByName("Unity-iPhone");

            proj.SetBuildProperty(target, "ENABLE_BITCODE", "false");

            File.WriteAllText(projPath, proj.WriteToString());



            // Add url schema to plist file
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            Dictionary<string, object> dict;
            dict = (Dictionary<string, object>)Plist.readPlist(plistPath);

            // update plist
            dict["CFBundleURLTypes"] = new List<object> {
        new Dictionary<string,object> {
            { "CFBundleURLName", "google" },
            { "CFBundleURLSchemes", new List<object> { "com.googleusercontent.apps.992185470650-4jaqc4vfhblcoq6dcfbhr5k1ek73qup0" } }
        }
            };


            Plist.writeXml(dict, plistPath);
        }
    }
    
}