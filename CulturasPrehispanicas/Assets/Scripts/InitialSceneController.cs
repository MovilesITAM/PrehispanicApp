using UnityEngine;
using System.Collections;

public class InitialSceneController : MonoBehaviour {

    public void start(){
        StartCoroutine(lol());
    }

	public IEnumerator lol(){
        float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel("SeleccionPeriodo");
    }
}
