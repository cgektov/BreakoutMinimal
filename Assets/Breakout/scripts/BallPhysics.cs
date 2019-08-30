using System;
using Lean.Touch;
using UnityEngine;

public class BallPhysics : MonoBehaviour {

    public static Action BallLost;

    Rigidbody2D body;
    float speed = 15f;
    static bool isOnSpawn = true;
    public static bool IsOnSpawn { get => isOnSpawn; }

    public int angleStuckCorrectionX = 1;
    public int angleStuckCorrectionY = 3;
    public float initSpd = 3f;
    public float maxSpd = 11f;
    public Transform spawn;


    void Start() {
        LeanTouch.OnFingerUp += (finger) =>
            Pew(finger.GetWorldPosition(10, Camera.main) - transform.position);


        body = GetComponent<Rigidbody2D>();
        Reset();
    }


    private void Update() {
        //if (Input.GetMouseButtonUp(1))Reset();
    }

    void FixedUpdate() {
        if (isOnSpawn) {
            transform.position = spawn.position;
            return;
        }


        speed = speed < maxSpd ? speed + Time.deltaTime / 10f : speed;


        var newVelocity = new Vector2(
            Mathf.Sign(body.velocity.x) * Mathf.Clamp(Mathf.Abs(body.velocity.x), angleStuckCorrectionX, speed),
            Mathf.Sign(body.velocity.y) * Mathf.Clamp(Mathf.Abs(body.velocity.y), angleStuckCorrectionY, speed));

        body.velocity = newVelocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        body.angularVelocity = -body.angularVelocity;
        if (other.collider.tag == "Brick")
            other.collider.gameObject.SendMessage("SetDamage", 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Pit")
            BallLost();
        Reset();
    }

    public void Pew(Vector2? dir = null) {
        if (!isOnSpawn)
            return;
        var newVelocity = (dir ?? Vector2.up).normalized * speed;
        newVelocity.y = newVelocity.y < 0 ? -newVelocity.y : newVelocity.y;

        body.velocity = newVelocity;
        body.angularVelocity = 450f;
        isOnSpawn = false;
    }

    public void Reset() {
        speed = initSpd;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0f;

        transform.position = spawn.position;
        isOnSpawn = true;

    }

}