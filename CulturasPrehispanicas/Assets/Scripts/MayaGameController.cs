using UnityEngine;
using System.Collections;

public class MayaGameController : MonoBehaviour {

	public GameObject corn;
	private Renderer cornRenderer;
	public int cornCols, cornRows;
	public GameObject[,] cornObjects;
	/* 0 - normal
	   1 - inmaduro
	   2 - quemado
	   3 - gusanos
	   4 - no hay
	   5 - ahogado*/
	private int[,] cornStates;

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
		for(i=0;i<cornRows;i++) for(j=0;j<cornCols;j++) cornStates[i,j]=0;
	}

	// Use this for initialization
	void Awake () {
		cornObjects = new GameObject[cornRows, cornCols];
		cornStates = new int[cornRows, cornCols];
		cornRenderer = corn.GetComponent<Renderer>();
		initCornGrid();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log("Click!");
			for(int i=0;i<cornRows;i++){
				for(int j=0;j<cornCols;j++){
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
					
					if(hit.collider != null &&  hit.collider.transform == cornObjects[i,j].transform)
					{
						Debug.Log ("Corn i: "+i+" j:"+j);
						break;
					}
				}
			}
		}
	}
}
