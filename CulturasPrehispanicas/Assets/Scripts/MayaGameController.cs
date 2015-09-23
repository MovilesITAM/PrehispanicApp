using UnityEngine;
using System.Collections;

public class MayaGameController : MonoBehaviour {

	public GameObject corn;
	private Renderer cornRenderer;
	public int cornCols, cornRows;
	public GameObject[,] cornObjects;
	public Sprite[] defaultSprites;
	public Sprite[] selectedSprites;
	/* 0 - normal
	   1 - inmaduro
	   2 - quemado
	   3 - gusanos
	   4 - ahogado
	   5 - vacio*/
	private int[,] cornStates;
	private int[,] selectedStates;

	void initCornGrid(){
		int i = 0; int j = cornCols/2;
		cornObjects [i, j] = (GameObject)Instantiate (corn, corn.transform.position, Quaternion.identity);
		j--;
		for (; j>=0; j--) {
			Vector2 prevPos = cornObjects[0,j+1].transform.position;
			cornObjects[0,j] = (GameObject) Instantiate(corn, new Vector2(prevPos.x - cornRenderer.bounds.size.x, corn.transform.position.y),Quaternion.identity );
		}
		j = cornCols / 2 + 1;
		for (; j<cornCols; j++) {
			Vector2 prevPos = cornObjects[0,j-1].transform.position;
			cornObjects[0,j] = (GameObject) Instantiate(corn, new Vector2(prevPos.x + cornRenderer.bounds.size.x, corn.transform.position.y),Quaternion.identity );
		}
		for(i=1;i<cornRows;i++){
			for(j=0;j<cornCols;j++){
				cornObjects[i,j] = (GameObject)Instantiate(corn,new Vector2(cornObjects[i-1,j].transform.position.x,cornObjects[i-1,j].transform.position.y-cornRenderer.bounds.size.y),Quaternion.identity);
			}
		}
		for (i=0; i<cornRows; i++)
			for (j=0; j<cornCols; j++) {
				cornStates [i, j] = Random.Range (0, 6);
				cornObjects [i, j].GetComponent<SpriteRenderer> ().sprite = defaultSprites [cornStates [i, j]];
			}
	}

	// Use this for initialization
	void Awake () {
		cornObjects = new GameObject[cornRows, cornCols];
		cornStates = new int[cornRows, cornCols];
		selectedStates = new int[cornRows, cornCols];
		cornRenderer = corn.GetComponent<Renderer>();
		initCornGrid();
	}

	void updateTouchedCorn(int i, int j){
		if (selectedStates [i, j] == 0) {
			cornObjects [i, j].GetComponent<SpriteRenderer> ().sprite = defaultSprites [cornStates [i, j]];
		} else {
			cornObjects [i, j].GetComponent<SpriteRenderer> ().sprite = selectedSprites [cornStates [i, j]];
		}
	}

	// Update is called once per frame
	void Update () {
		Vector2 inputPosition = new Vector2(0,0);
#if UNITY_EDITOR_WIN
		if (Input.GetMouseButtonDown (0)) {
			inputPosition = Input.mousePosition;
		}
#endif


#if UNITY_ANDROID
		if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			inputPosition = Input.GetTouch(0).position;
		}
#endif
		for(int i=0;i<cornRows;i++){
			for(int j=0;j<cornCols;j++){
				Ray ray = Camera.main.ScreenPointToRay(inputPosition);
				RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
				
				if(hit.collider != null &&  hit.collider.transform == cornObjects[i,j].transform){
					selectedStates[i,j]++; selectedStates[i,j]%=2;
					updateTouchedCorn(i,j);
					break;
				}
			}
		}
	}


}
