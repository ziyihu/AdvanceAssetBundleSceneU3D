using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

public class Packager {

	public static string platform = string.Empty;
	static List<string> paths = new List<string>();
	static List<string> files = new List<string>();

	[MenuItem("AssetsBundle/Build iPhone Bundle")]
	public static void BuildiPhoneBundle(){
		BuildTarget target;
		target = BuildTarget.iOS;
		BuildAssetResource (target, false);
	}

	[MenuItem("AssetsBundle/Build Android Bundle")]
	public static void BuildAndriodeBundle(){
		BuildTarget target;
		target = BuildTarget.Android;
		BuildAssetResource (target, false);
	}

	[MenuItem("AssetsBundle/Build Windows Bundle")]
	public static void BuildWindowsBundle(){
		BuildTarget target;
		target = BuildTarget.StandaloneWindows;
		BuildAssetResource (target, false);
	}

	[MenuItem("AssetsBundle/Build Mac Bundle")]
	public static void BuildMacBundle(){
		BuildTarget target;
		target = BuildTarget.StandaloneOSXUniversal;
		BuildAssetResource (target, false);
	}

	public static void BuildAssetResource(BuildTarget target, bool isWin){
		string dataPath = Application.dataPath+"/Resources"+"/";
		if (Directory.Exists (dataPath)) {
	//		Directory.Delete(dataPath, true);
		}
		string assetFile = string.Empty;
		string resPath = Application.dataPath + "/StreamingAssets" + "/";
		if (!Directory.Exists (resPath))
			Directory.CreateDirectory (resPath);

		BuildPipeline.BuildAssetBundles (resPath, BuildAssetBundleOptions.None, target);

		AssetDatabase.Refresh ();
	}

	static void Recursive(string path){
		string[] names = Directory.GetFiles (path);
		string[] dirs = Directory.GetDirectories (path);
		foreach (string filename in names) {
			string ext = Path.GetExtension (filename);
			if (ext.Equals (".meta"))
				continue;
			files.Add (filename.Replace ('\\', '/'));
		}
		foreach (string dir in dirs) {
			paths.Add (dir.Replace ('\\', '/'));
			Recursive (dir);
		}
	}

	static void UpdateProgress(int progress, int progressMax, string desc) {
		string title = "Processing..[" + progress + "-" + progressMax + "]";
		float value = (float)progress / (float)progressMax;
		EditorUtility.DisplayProgressBar (title, desc, value);
	}
}
