using UnityEngine;
using System.Collections;

public class MayaGameController : MonoBehaviour {
	
	public GameObject corn;
	private Renderer cornRenderer;
	public int cornCols, cornRows;
	public GameObject[,] cornObjects;

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
		int N = cornRows * cornCols, st;
		for (i=0; i<cornRows; i++) {
			for (j=0; j<cornCols; j++) {
				int value = Random.Range(0,N);
				if(value<N/2) st = 0;
				else if(value<5*N/8) st = 1;
				else if(value<6*N/8) st = 2;
				else if(value<7*N/8) st = 3;
				else st = 4;
				CornController cornScript = cornObjects [i, j].GetComponent<CornController> ();
				cornScript.setCornState(st);
			}
		}
	}

	// Use this for initialization
	void Awake () {
		cornObjects = new GameObject[cornRows, cornCols];
		cornRenderer = corn.GetComponent<Renderer>();
		initCornGrid();
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
					CornController cornScript = cornObjects [i, j].GetComponent<CornController> ();
					cornScript.selectCorn();
					break;
				}
			}
		}
	}


}
