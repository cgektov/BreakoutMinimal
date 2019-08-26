using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    GameObject linePref;

    List<GameObject> Lines = new List<GameObject>();

    int maxLines = 4;
    //int currentLines = 0;

    Tween delay;

    void Start() {

        //BallPhysics.BallLost += RemoveAllLines;
        LineVariation.AnyLineEmpty += RemoveLine;

        //the coolest delayed invoke implementation :D
        transform.DOMove(transform.position, 0.4f).SetLoops(maxLines).Play().OnStepComplete(() => NewLine());
    }


    private void RemoveLine(GameObject obj) {
        Lines.Remove(obj);
        LeanPool.Despawn(obj);
        NewLine((int)obj.transform.position.y);
    }

    /*     void RemoveAllLines() {
            foreach (var item in Lines) {
                LeanPool.Despawn(item);
                Lines.Remove(item);
            }
        }
     */


    public void NewLine(int y = 0) {
        if (Lines.Count > maxLines - 1)
            return;

        foreach (var item in Lines.Where(l => l.transform.position.y > y))
            item.transform.DOLocalMoveY(-1, 0.4f).SetEase(Ease.OutBounce).SetRelative(true);

        var newLine = LeanPool.Spawn(linePref, transform.localPosition, Quaternion.identity);
        newLine.transform.DOLocalMoveY(8, 0.4f).SetEase(Ease.OutBounce).From().SetRelative(true);
        Lines.Add(newLine);
    }


}