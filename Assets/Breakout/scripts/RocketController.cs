using DG.Tweening;
using Lean.Touch;
using UnityEngine;

public class RocketController : MonoBehaviour {
    public BallPhysics ball;
    public Transform leftLimit;
    public Transform rightLimit;
    public float Dampening = 25f;


    private Vector2 targetPoint;
    private Vector3 newPosition = Vector3.zero;
    private float factor = 0;


    protected virtual void Start() {

    }

    protected virtual void FixedUpdate() {
        /* 		if (ball.IsOnSpawn)
        			return; */
        var fingers = LeanTouch.GetFingers(false, false, 0);
        if (fingers.Count > 0) {
            targetPoint = LeanGesture.GetScreenCenter(fingers);
            newPosition = Camera.main.ScreenToWorldPoint(new Vector3(targetPoint.x, targetPoint.y, -Camera.main.transform.position.z));

            newPosition.y = transform.position.y;
            newPosition.x = Mathf.Clamp(newPosition.x, leftLimit.position.x, rightLimit.position.x);
            factor = LeanTouch.GetDampenFactor(Dampening, Time.fixedDeltaTime);

        }
        transform.position = Vector3.Lerp(transform.position, newPosition, factor);
    }

}