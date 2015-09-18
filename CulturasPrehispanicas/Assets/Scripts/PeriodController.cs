using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PeriodController : MonoBehaviour {

    public Text period;
    public Text info;
    string[] periodNames = new string[] { "Preclásico", "Clásico", "Posclásico" };
    string[] periodDescriptions = new string[] { "Primer periodo", "Segundo periodo", "Tercer periodo" };
    int currentPeriod;

    void Start () {
        currentPeriod = PlayerPrefs.GetInt("CurrentPeriod",0);
        setValues();
	}

    void setValues(){
        period.text = periodNames[currentPeriod];
        info.text = periodDescriptions[currentPeriod];
    }

	public void changePeriod(int increment){
        currentPeriod += increment;
        if (currentPeriod >= 0 && currentPeriod < periodNames.Length) setValues();
        if (currentPeriod < 0) currentPeriod = 0;
        if (currentPeriod >= periodNames.Length) currentPeriod = periodNames.Length - 1;
        PlayerPrefs.SetInt("CurrentPeriod",currentPeriod);
    }

	void Update () {
	
	}
}
