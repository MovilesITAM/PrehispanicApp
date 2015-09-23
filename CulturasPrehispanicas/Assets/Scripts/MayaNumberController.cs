using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MayaNumberController : MonoBehaviour {

	public Text txtButton;
	public int mayaNumber;

	void Awake() {
		mayaNumber = Random.Range (1, 11);
		txtButton.text = ""+mayaNumber;
	}
	public void setMayaNumber(int number){
		txtButton.text = ""+mayaNumber;
	}
}
