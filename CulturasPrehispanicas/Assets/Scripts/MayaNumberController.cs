using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MayaNumberController : MonoBehaviour {
	public Button btnNumber;
	public Text totalNumber;
	public int mayaNumber;
	private MayaGameController mgc;
	public Camera cam;
	public GameObject [] masks;
	private int lifes;
	public Sprite [] numbers;

	void Awake() {
		mgc = cam.GetComponent<MayaGameController>();
		mayaNumber = Random.Range (1, 11);
		setMayaNumber ();
		lifes = masks.Length-1;
	}

	private void gameOver(){
		Debug.Log ("Juego terminado");
	}

	private void changeCornStates(){
		int rows = mgc.cornRows, cols = mgc.cornCols;
		int count = 0;
		GameObject [,] corns = mgc.cornObjects;
		for (int i=0; i<rows; i++) {
			for(int j=0;j<cols;j++){
				CornController cornCont = corns[i,j].GetComponent<CornController>();
				if(cornCont.selected == 1 && cornCont.state == 0){
					cornCont.setCornState(Random.Range(1,5));
					count++;
				}
				cornCont.selected = 0;
				cornCont.changeSprite();
			}
		}
		if (lifes >= 0) {
			//Debug.Log("count: "+count);
			if (count == mayaNumber) {
				Debug.Log ("Correcto");
				int tot;
				int.TryParse (totalNumber.text, out tot);
				tot -= mayaNumber;
				if (tot < 0) {
					tot = 0;
					gameOver ();
				}
				totalNumber.text = "" + tot; 
			} else {
				Debug.Log ("Incorrecto");
				masks [lifes].SetActive (false);
				lifes--;
				if (lifes < 0) {
					gameOver ();
				} 
			}
		}
	}

	public void setMayaNumber(){
		btnNumber.image.overrideSprite = numbers[mayaNumber];
	}
	public void generateNewMayaNumber(){
		changeCornStates ();
		int n = Random.Range (1, 11);
		while (n==mayaNumber)
			n = Random.Range (1, 11);
		mayaNumber = n;
		setMayaNumber ();
	}
}
