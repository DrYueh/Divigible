using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PuzzleLight : MonoBehaviour {
    public GameManager gameManager;
	
	// Update is called once per frame
	void Update () {
        if (gameManager.puzzleSolved()) {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        } else {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        }
	}
}
