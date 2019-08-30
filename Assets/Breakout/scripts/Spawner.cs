using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    GameObject linePref;
    [SerializeField]
    int maxLines = 4;

    List<GameObject> Lines = new List<GameObject>();


    void Start() {
        LineVariation.AnyLineEmpty += RemoveLine;

        for (int i = 0; i <= maxLines; i++)
            NewLine();
    }


    private void RemoveLine(GameObject obj) {
        Lines.Remove(obj);
        LeanPool.Despawn(obj);
        NewLine((int)obj.transform.position.y);
    }


    bool isP = false;
    void NewLine(int y = 0) {
        if (isP || Lines.Count >= maxLines) {
            return;
        }

        isP = true;
        foreach (var item in Lines.Where(l => l.transform.position.y > y))
            item.transform.DOLocalMoveY(-1, 0.6f).SetEase(Ease.OutExpo).SetRelative(true);

        var newLine = LeanPool.Spawn(linePref, transform.localPosition, Quaternion.identity);
        newLine.transform.DOLocalMoveY(8, 0.6f).SetDelay(0.2f).SetEase(Ease.OutExpo).From().SetRelative(true)
            .OnComplete(() => { isP = false; NewLine(y); });

        Lines.Add(newLine);
    }


}