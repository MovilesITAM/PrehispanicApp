using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PeriodController : MonoBehaviour {

    public Text txtPeriod;
    public Sprite[] infoSprites;
    public Image infoImage;
    public GameObject[] periods;
    string[] periodNames = new string[] { "Preclásico", "Clásico", "Posclásico" };
    int currentPeriod;

    void Start () {
        currentPeriod = PlayerPrefs.GetInt("CurrentPeriod",0);
        setValues();
	}

    void setValues(){
        txtPeriod.text = periodNames[currentPeriod];
        infoImage.sprite = infoSprites[currentPeriod];
        for(int i = 0; i < periods.Length; i++){
            periods[i].SetActive(false);
        }periods[currentPeriod].SetActive(true);
    }

	public void changePeriod(int increment){
        currentPeriod += increment;
        if (currentPeriod >= 0 && currentPeriod < periodNames.Length) setValues();
        if (currentPeriod < 0) currentPeriod = 0;
        if (currentPeriod >= periodNames.Length) currentPeriod = periodNames.Length - 1;
        PlayerPrefs.SetInt("CurrentPeriod",currentPeriod);
    }
}
