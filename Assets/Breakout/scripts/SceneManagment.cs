using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour {

    void Start() {
        //BallPhysics.BallLost += () => Reload();

    }
    void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}