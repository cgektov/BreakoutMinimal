using Lean.Pool;
using UnityEngine;

public class LineVariation : MonoBehaviour {

    public static event System.Action<GameObject> AnyLineEmpty = delegate { };

    [SerializeField]
    GameObject[] brickVariants;


    private void OnEnable() {
        for (int i = 0; i < 5; i++) {
            LeanPool.Spawn(brickVariants[Random.Range(0, 3)], new Vector3(-2 + i, 0, 0), Quaternion.identity, transform);
        }
    }



    void Update() {
        if (transform.childCount == 0)
            AnyLineEmpty(gameObject);
    }



    private void OnDisable() {

    }
}