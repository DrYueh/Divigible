using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSubmit : MonoBehaviour {
    public PuzzleProvider provider;
    
    public void OnMouseUpAsButton() {
        provider.UpdatePuzzle();
        Debug.Log(provider.puzzle.ToString() + ": " + provider.puzzle.eval());
    }
}

