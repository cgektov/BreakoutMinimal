using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    Text t;
    int totalLines = -1;

    void Start() {
        BallPhysics.BallLost += Reset;
        LineVariation.AnyLineEmpty += (gameObject) => UpdateStatsText();

        t = gameObject.GetComponent<Text>();
        UpdateStatsText();
    }



    void UpdateStatsText() {
        totalLines++;
        t.text = string.Format("TOTAL LINES: {0}", totalLines);
    }



    void Reset() {
        totalLines = -1;
        UpdateStatsText();
    }

}