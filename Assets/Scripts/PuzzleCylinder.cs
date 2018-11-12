using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCylinder : MonoBehaviour {
    public enum Rot { T, M, B };
    Rot rot = Rot.M;
    Vector3 rotT = new Vector3(-60, 0, 90);
    Vector3 rotM = new Vector3(0, 0, 90);
    Vector3 rotB = new Vector3(60, 0, 90);

    public GameManager gameManager;
    
    public void rotate(Rot rot) {
        switch (rot) {
            case Rot.T: rotTop(); break;
            case Rot.M: rotMid(); break;
            case Rot.B: rotBot(); break;
        }
    }
    
    public void rotUp() {
        switch (rot) {
            case Rot.M: rotTop(); break;
            case Rot.B: rotMid(); break;
        }
    }
    
    public void rotDown() {
        switch (rot) {
            case Rot.T: rotMid(); break;
            case Rot.M: rotBot(); break;
        }
    }
    
	public void rotTop() {
        rot = Rot.T;
        transform.rotation = Quaternion.Euler(rotT);
        gameManager.setCurrentTest(GameManager.TEST_TOP);
    }
    
    public void rotMid() {
        rot = Rot.M;
        transform.rotation = Quaternion.Euler(rotM);
        gameManager.setCurrentTest(GameManager.TEST_MIDDLE);
    }
    
    public void rotBot() {
        rot = Rot.B;
        transform.rotation = Quaternion.Euler(rotB);
        gameManager.setCurrentTest(GameManager.TEST_BOTTOM);
    }
}
