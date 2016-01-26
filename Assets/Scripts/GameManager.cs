using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("1");
		Init ();
		ObjManager objMgr = Facade.Instance.GetManager<ObjManager> ("ObjManager");
		GameObject obj = objMgr.CreateAndLoadObj ("firstpanel", "firstpanel");
		objMgr.SetPanel (obj);
		GameObject panel1 = objMgr.GetPanel ("firstpanel");
		panel1.SetActive (true);
//		//set the image null
//		obj.GetComponentInChildren<Image> ().sprite = null;
//		objMgr.CreateAndLoadObj ("boyidle", "boyidle");
//		objMgr.CreateAndLoadObj ("girlidle", "girlidle");
//		objMgr.AddSpriteTexture (obj, "secondbg", "secondbg");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Init(){
		Facade.Instance.AddManager<ObjManager> ("ObjManager");
		Facade.Instance.AddManager<ResourceManager> ("ResourceManager");
	}
}
