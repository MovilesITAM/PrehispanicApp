using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class periodoController : MonoBehaviour {

    Text period;
    public Text info;
    int selected;
    int pastOne;

    void Start () {
        period = GetComponent<Text>();
        period.text = "Preclásico";
        selected = pastOne = 0;
        info.text = "Primer periodo estudiado.";
	}

    public void changePeriod(int selection) {
        selected += selection;
        if (selected < 0) selected = 0;
        if (selected > 2) selected = 2;

        if (selected != pastOne)
        {
            if (selected == 0) {
                period.text = "Preclásico";
                info.text = "Primer periodo estudiado.";
            } else if (selected == 1) {
                period.text = "Clásico";
                info.text = "Segundo periodo estudiado.";
            } else if (selected == 2) {
                period.text = "Posclásico";
                info.text = "Tercer periodo estudiado.";
            }
            pastOne = selected;
        }
    }

	void Update () {
	
	}
}
