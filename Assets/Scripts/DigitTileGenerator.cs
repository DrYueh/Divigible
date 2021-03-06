﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array = UnityScript.Lang.Array;

public class DigitTileGenerator : MonoBehaviour
{
	public GameObject prefab;
	public int numberOfObjects = 10;
	public Vector3 start;
	public Vector3 end;

	private float _charWidth;
	private float _kerning = 0.05f;

	private Transform _template = null;

	void Start() 
	{	
		for (int i = 0; i < numberOfObjects; i++)
		{
			Vector3 pos = new Vector3(xCoord(i), start.y, start.z);
			generateTile(pos, i);
		}
	}

	float xCoord(int i)
	{
		float totalDistance = end.x - start.x;
		float fraction = i / (float) numberOfObjects;
		return fraction * totalDistance + start.x;
	}

	public Tile generateTile(Vector3 location, int value) {
       return generateTile(location, Quaternion.identity, value); 
    }
	public Tile generateTile(Vector3 location, Quaternion rotation, int value)
	{
		// Get width of characters
		_charWidth = getTemplate().gameObject.GetComponent<Renderer>().bounds.size.x;
		GameObject obj = null;

		if ((value < 10) && (value >= 0))
		{		
			obj = Instantiate(prefab, location, rotation);
			obj.GetComponent<Tile>().value = value;
			obj.GetComponent<Tile>().layerToTarget = 15;
			float widthTotal = (value.ToString().Length) * _charWidth;
			float kerningTotal = (value.ToString().Length) * _kerning - 1 + (2 * _kerning); // 2 kernings for beginning and end
			obj.transform.localScale = new Vector3(
				Mathf.Max(1, widthTotal + kerningTotal),
				1, 0.2f
			);

			GameObject digit = (GameObject)Instantiate(Resources.Load("Prefabs/" + value.ToString()));
			digit.GetComponent<DigitValue>().integerValue = value;
			digit.GetComponent<DigitValue>().orderOfMagnitude = 1;
            digit.transform.rotation = obj.transform.rotation; // rotate BEFORE translating
			digit.transform.parent = obj.transform;
			digit.transform.position = obj.transform.position;
		}

		if (value >= 10)
		{
			obj = printLargerNumber(value, location, rotation);
		}

		return (obj != null) ? obj.GetComponent<Tile>() : null;
	}

	// Beware ye who ventures into this janky code and may god have mercy on you
	private GameObject printLargerNumber(int value, Vector3 location, Quaternion rotation)
	{
		string valueAsString = value.ToString();
		double magnitude = Math.Pow(10, valueAsString.Length-1);

		float widthTotal = (value.ToString().Length) * 0.75f;
		float kerningTotal = (value.ToString().Length) * _kerning - 1 + (2 * _kerning); // 2 kernings for beginning and end
		float tileScaleX = Mathf.Max(1, widthTotal + kerningTotal);
		
		Array nums = new Array();
		
		int index = 0;
		foreach (char digitText in valueAsString)
		{
			GameObject digit = (GameObject)Instantiate(Resources.Load("Prefabs/" + digitText));
			digit.GetComponent<DigitValue>().integerValue = Int32.Parse(digitText.ToString());
			digit.GetComponent<DigitValue>().orderOfMagnitude = magnitude;

            digit.transform.rotation = rotation; // rotate BEFORE translating
			digit.transform.localPosition = new Vector3(
				-tileScaleX/2 + (location.x + (index * _charWidth) + _kerning) + _charWidth/2, 
				location.y, location.z
			);
			nums.Add(digit);
			
			index += 1;
			magnitude /= 10;
		}
		
		GameObject obj = Instantiate(prefab, location, rotation);
		obj.GetComponent<Tile>().value = value;
		obj.GetComponent<Tile>().layerToTarget = 15;
		
		obj.transform.localScale = new Vector3(
			tileScaleX,
			1, 0.2f
		);
		
		foreach (GameObject digit in nums)
		{
			digit.transform.parent = obj.transform;
		}

		return obj;
	}
	
	private Transform getTemplate()
	{
		if (!_template) {
			_template = ((GameObject)Instantiate(Resources.Load("Prefabs/0"))).transform.GetChild(0);
			_template.gameObject.SetActive(false);
		}

		return _template;
	}
}
