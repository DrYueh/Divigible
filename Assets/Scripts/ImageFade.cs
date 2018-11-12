using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour {
    public GameManager gameManager;
    public float fadePeriod;
    private float startFade;
    private bool fading;
    
    public Color win = Color.white;
    public Color lose = Color.black;

	// Update is called once per frame
	void Update () {
        if (!fading && (Time.time + fadePeriod > gameManager.timeLimit || gameManager.GameOver())) {
            fading = true;
            startFade = Time.time;
        }
        if (fading) {
            Image image = GetComponent<Image>();
            Color color = gameManager.puzzleSolved() ? win : lose;
            image.color = new Color(color.r, color.g, color.b, Mathf.Clamp((Time.time - startFade) / fadePeriod, 0, 1));
        }
	}
}
