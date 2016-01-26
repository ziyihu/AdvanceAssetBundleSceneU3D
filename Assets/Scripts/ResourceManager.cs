﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ResourceManager : MonoBehaviour {

	static Dictionary<string, AssetBundle> m_BundlesMap = new Dictionary<string, AssetBundle>();
	public AssetBundle LoadBundle(string name){
		if (m_BundlesMap.ContainsKey (name) && m_BundlesMap [name] != null) {
			return m_BundlesMap [name];
		} else {
			byte[] stream = null;
			AssetBundle bundle = null;
			string url = Util.DataPath + name + ".assetbundle";
			stream = File.ReadAllBytes (url);

			bundle = AssetBundle.LoadFromMemory (stream);
			if (m_BundlesMap.ContainsKey (name)) {
				m_BundlesMap [name] = bundle;
			} else {
				m_BundlesMap.Add (name, bundle);
			}
			return bundle;
		}
	}

	void Start(){

	}
}
