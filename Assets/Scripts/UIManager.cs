using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private ObjManager objMgr;
	private ResourceManager resMgr;

	private bool isGirlShow = true;
	private bool isBoyShow = true;

	void Start(){
		objMgr = Facade.Instance.AddManager<ObjManager> ("ObjManager");
		resMgr = Facade.Instance.AddManager<ResourceManager> ("ResourceManager");
	}

	public void OnClickFirstPanelBtn(){
		DeleteChara ();

		GameObject panel2 = objMgr.GetPanel ("secondpanel");
		GameObject panel3 = objMgr.GetPanel ("thirdpanel");
		if (panel2 != null) {
			panel2.SetActive (false);
		}
		if (panel3 != null) {
			panel3.SetActive (false);
		}
		GameObject panel1 = objMgr.GetPanel ("firstpanel");
		if (panel1 == null) {
			GameObject obj = objMgr.CreateAndLoadObj ("firstpanel", "firstpanel");
			objMgr.SetPanel (obj);
		} else {
			panel1.SetActive (true);
		}
	}

	public void OnClickSecondPanelBtn(){

		GameObject panel1 = objMgr.GetPanel ("firstpanel");
		GameObject panel3 = objMgr.GetPanel ("thirdpanel");
		if (panel1 != null) {
			panel1.SetActive (false);
		}
		if (panel3 != null) {
			panel3.SetActive (false);
		}
		GameObject panel2 = objMgr.GetPanel ("secondpanel");
		GameObject girl = objMgr.GetPanel ("girlidle");
		GameObject boy = objMgr.GetPanel ("boyidle");
		if (girl != null) {
			girl.SetActive (true);
		} else {
			objMgr.CreateAndLoadObj ("girlidle", "girlidle");
		}
		if (boy != null) {
			boy.SetActive (true);
		} else {
			objMgr.CreateAndLoadObj ("boyidle", "boyidle");
		}
		if (panel2 == null) {
			GameObject obj = objMgr.CreateAndLoadObj ("secondpanel", "secondpanel");
			objMgr.SetPanel (obj);
			obj.GetComponentInChildren<Image> ().sprite = null;
		} else {
			panel2.SetActive (true);
		}
	}

	public void OnChangeSprite(){
		GameObject panel2 = objMgr.GetPanel ("secondpanel");
		if (panel2.GetComponentInChildren<Image> ().sprite == null) {
			objMgr.AddSpriteTexture (panel2, "secondbg", "secondbg");
		} else {
			panel2.GetComponentInChildren<Image> ().sprite = null;
		}
	}

	public void OnClickThirdPanelBtn(){
		DeleteChara ();

		GameObject panel1 = objMgr.GetPanel ("firstpanel");
		GameObject panel2 = objMgr.GetPanel ("secondpanel");
		if (panel1 != null) {				
			panel1.SetActive (false);
		
		}
		if (panel2 != null) {
			panel2.SetActive (false);
		}
		GameObject panel3 = objMgr.GetPanel ("thirdpanel");
		if (panel3 == null) {
			GameObject obj = objMgr.CreateAndLoadObj ("thirdpanel", "thirdpanel");
			objMgr.SetPanel (obj);
		} else {
			panel3.SetActive (true);
		}
	}

	private void DeleteChara(){
		GameObject girl = objMgr.GetPanel ("girlidle");
		GameObject boy = objMgr.GetPanel ("boyidle");
		if (girl != null) {
			girl.SetActive (false);
		}
		if (boy != null) {
			boy.SetActive (false);
		}
	}

	public void DeleteBoy(){
		GameObject boy = objMgr.GetPanel ("boyidle");
		if (isBoyShow) {
			boy.SetActive (false);
			isBoyShow = false;
		} else {
			boy.SetActive (true);
			isBoyShow = true;
		}
	}

	public void DeleteGirl(){
		GameObject girl = objMgr.GetPanel ("girlidle");
		if (isGirlShow) {
			girl.SetActive (false);
			isGirlShow = false;
		} else {
			girl.SetActive (true);
			isGirlShow = true;
		}
	}
}
