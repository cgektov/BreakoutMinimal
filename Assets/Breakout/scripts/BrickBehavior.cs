using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class BrickBehavior : MonoBehaviour {

    [SerializeField]
    int maxHp = 1;

    int curHp;
    Tween death, dmg;


    private void OnEnable() {
        curHp = maxHp;
        death = transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutBounce).Pause();
        dmg = transform.DOShakePosition(0.5f, new Vector3(0.1f, 0.1f, 0.1f)).SetAutoKill(false).Pause();
    }

    private void Start() {
        BallPhysics.BallLost += Death;
    }


    public void SetDamage(int val) {
        curHp -= val;

        if (curHp <= 0)
            Death();
        else
            dmg.Restart();
    }

    private void Death() {
        if (gameObject.activeSelf)
            death.Play().OnComplete(() => {
                death.Rewind();
                LeanPool.Despawn(this);
            });
    }


    private void OnDisable() {
        dmg.Kill();
    }

}