using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class Util  {

	public static int Int(object o){
		return Convert.ToInt32 (o);
	}

	public static float Float(object o){
		return (float)Math.Round (Convert.ToSingle (o), 2);
	}

	public static long Long(object o){
		return Convert.ToInt64 (o);
	}

	public static int Random(int min,int max){
		return UnityEngine.Random.Range (min, max);
	}

	public static float Random(float min, float max){
		return UnityEngine.Random.Range (min, max);
	}

	public static string Uid(string uid){
		int position = uid.LastIndexOf ('_');
		return uid.Remove (0,position+1);
	}

	public static long GetTime(){
		TimeSpan ts = new TimeSpan (DateTime.UtcNow.Ticks - new DateTime (1970, 1, 1, 0, 0, 0).Ticks);
		return (long)ts.TotalMilliseconds;
	}

	//get the sub component
	public static T Get<T>(GameObject go, string subnode) where T:Component{
		if (go != null) {
			Transform sub = go.transform.FindChild (subnode);
			if (sub != null) {
				return sub.GetComponent <T> ();
			}
		}
		return null;
	}

	//add sub component to the parent
	public static T Add<T>(GameObject go) where T:Component{
		if (go != null) {
			T[] ts = go.GetComponents<T> ();
			for (int i = 0; i < ts.Length; i++) {
				if (ts [i] != null) {
					GameObject.Destroy (ts[i]);
				}
			}
			return go.gameObject.AddComponent<T> ();
		}
		return null;
	}

	public static T Add<T>(Transform go) where T: Component{
		return Add<T> (go.gameObject);
	}

	//find subnode in the gameObject
	public static GameObject Child(GameObject go, string subnode){
		return Child (go.transform, subnode);
	}
	//find subnode in the gameObject
	public static GameObject Child(Transform go, string subnode){
		Transform trans = go.FindChild (subnode);
		if (trans == null)
			return null;
		return trans.gameObject;
	}

	//find the parallel gameobject
	public static GameObject Para(Transform go, string subnode){
		Transform trans = go.parent.FindChild (subnode);
		if (trans == null)
			return null;
		return trans.gameObject;
	}

	//encode
	public static string Encode(string message){
		byte[] bytes = Encoding.GetEncoding ("utf-8").GetBytes(message);
		return Convert.ToBase64String (bytes);
	}

	//decode
	public static string Decode(string message){
		byte[] bytes = Convert.FromBase64String (message);
		return Encoding.GetEncoding ("utf-8").GetString (bytes);
	}

	//contain a number or not
	public static bool IsNumeric(string str){
		if (str == null || str.Length == 0) {
			return false;
		} 
		for (int i = 0; i < str.Length; i++) {
			if (!Char.IsNumber (str [i])) {
				return false;
			}
		}
		return true;
	}

	//change has value to MD5 value
	public static string HashToMD5(string sourceStr){
		byte[] bytes = Encoding.UTF8.GetBytes (sourceStr);
		using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ()) {
			byte[] result = md5.ComputeHash (bytes);
			StringBuilder builder = new StringBuilder ();
			for (int i = 0; i < result.Length; i++) {
				//add something to make the MD5 safer
				builder.Append (result [i].ToString ("abcdef"));
			}
			return builder.ToString ();
		}
	}

	public static string MD5(string source){
		MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ();
		byte[] bytes = System.Text.Encoding.UTF8.GetBytes (source);
		byte[] md5Date = md5.ComputeHash (bytes, 0, bytes.Length);
		md5.Clear ();
		string destString = "";
		for (int i = 0; i < md5Date.Length; i++) {
			destString += System.Convert.ToString (md5Date [i], 16).PadLeft (2, '0');
		}
		destString = destString.PadLeft (32, '0');
		return destString;
	}

	// MD5 value for the file
	public static string MD5file(string file){
		try{
			FileStream fs = new FileStream(file,FileMode.Open);
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] retVal = md5.ComputeHash(fs);
			fs.Close();
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < retVal.Length ; i++){
				sb.Append(retVal[i].ToString("x2"));
			}
			return sb.ToString();
		}
		catch(Exception ex){
			throw new Exception ("md5 file fail, error"+ ex.Message);
		}
	}

	//clear all the child nodes
	public static void ClearChild(Transform go){
		if (go == null) {
			return;
		}
		for (int i = go.childCount - 1; i >= 0; i--) {
			GameObject.Destroy (go.GetChild (i).gameObject);
		}
	}


	public static string GetKey(string key){
		return AppConst.AppPrefix + "_" + key;
	}

	//get the data saved in the game by using key
	public static int GetInt(string key){
		string name = GetKey (key);
		return PlayerPrefs.GetInt (name);
	}

	//whether have the key or not
	public static bool HasKey(string key){
		string name = GetKey (key);
		return PlayerPrefs.HasKey (name);
	}

	//save the int to the game 
	public static void SetInt(string key,int value){
		string name = GetKey (key);
		PlayerPrefs.DeleteKey (name);
		PlayerPrefs.SetInt (name, value);
	}

	public static string GetString(string key){
		string name = GetKey (key);
		return PlayerPrefs.GetString (name);
	}

	public static void SetString(string key,string value){
		string name = GetKey (key);
		PlayerPrefs.DeleteKey (name);
		PlayerPrefs.SetString (name, value);
	}

	public static void RemoveData(string key){
		string name = GetKey (key);
		PlayerPrefs.DeleteKey (name);
	}

	public static void ClearMemory(){
		GC.Collect ();
		Resources.UnloadUnusedAssets ();
	}

	//the string only contain number
	public static bool IsNumber(string strNumber){
		Regex regex = new Regex ("[^0-9]");
		return !regex.IsMatch (strNumber);
	}

	//data path
	public static string DataPath{
		get{
			string name = AppConst.AppName.ToLower ();
			if(Application.isMobilePlatform)
				return Application.persistentDataPath+"/"+name+"/";
			else 
				return Application.dataPath+"/"+AppConst.AssetDirname+"/";
		}
	}

	public static bool NetAvailable{
		get{
			return Application.internetReachability != NetworkReachability.NotReachable;
		}
	}

	public static bool isWifi{
		get{
			return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
		}
	}

	public static string AppContentPath(){
		string path = string.Empty;
		switch (Application.platform) {
		case RuntimePlatform.Android:
			path = "jar:file//" + Application.dataPath + ":/assets/";
			break;
		case RuntimePlatform.IPhonePlayer:
			path = Application.dataPath + "/Raw/";
			break;
		default:
			path = Application.dataPath + "/" + AppConst.AssetDirname + "/";
			break;
		}
		return path;
	}

	public static void Log(string str){
		Debug.Log (str);
	}

	public static void LogWarning(string str){
		Debug.LogWarning (str);
	}

	public static void LogError(string str){
		Debug.LogError (str);
	}
}
