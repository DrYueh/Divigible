using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour {
    public GameManager gameManager;
    public float timeToFade;
    public float fadePeriod;

	// Update is called once per frame
	void Update () {
        if (timeToFade < Time.time && !gameManager.puzzleSolved()) {
            Image image = GetComponent<Image>();
            Color color = image.color;
            image.color = new Color(color.r, color.g, color.b, Mathf.Clamp((Time.time - timeToFade) / fadePeriod, 0, 1));
        }
	}
}
