using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResultTile : MonoBehaviour
{
	public int result;

	// Use this for initialization
	void Start ()
	{
		DigitTileGenerator generator = GameObject.Find("TileCreator").GetComponent<DigitTileGenerator>();
		generator.generateTile(transform.position, result);
	}
}
