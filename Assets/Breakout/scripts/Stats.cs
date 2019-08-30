using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    Text t;
    int totalLines = 0;

    void Start() {
        LineVariation.AnyLineEmpty += (gameObject) => UpdateStatsText();
        BallPhysics.BallLost += ResetStast;

        t = gameObject.GetComponent<Text>();
        UpdateStatsText();
    }

    void UpdateStatsText() {
        if (!BallPhysics.IsOnSpawn)
            totalLines++;
        t.text = string.Format("TOTAL LINES: {0}", totalLines);
    }


    void ResetStast() {
        totalLines = -1;
        UpdateStatsText();
    }

}