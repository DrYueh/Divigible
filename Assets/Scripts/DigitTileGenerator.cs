using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitTileGenerator : MonoBehaviour
{
	public GameObject prefab;
	public int numberOfObjects = 10;
	public Vector2 start;
	public Vector2 end;

	void Start() 
	{
		for (int i = 0; i < numberOfObjects; i++)
		{
			Vector3 pos = new Vector3(xCoord(i), start.y, 0.3f);
			generateTile(pos, i);
		}
	}

	float xCoord(int i)
	{
		float totalDistance = end.x - start.x;
		float fraction = i / (float) numberOfObjects;
		return fraction * totalDistance + start.x;
	}

	public void generateTile(Vector3 location, int value)
	{
		GameObject obj = Instantiate(prefab, location, Quaternion.identity);
		obj.GetComponent<Tile>().value = value;
		obj.GetComponent<Tile>().layerToTarget = 15;
		obj.transform.localScale = new Vector3(((value / 10) * 0.1f + 1), 1, 0.2f);
		
		if ((value < 10) && (value >= 0))
		{
			GameObject digit = (GameObject)Instantiate(Resources.Load("Prefabs/" + value.ToString()));
			digit.GetComponent<DigitValue>().integerValue = value;
			digit.GetComponent<DigitValue>().orderOfMagnitude = 1;
			digit.transform.parent = obj.transform;
			digit.transform.position = obj.transform.position;
		}

		if (value > 10)
		{
			largerValue(value, obj);
		}
	}

	// WIP code
	private void largerValue(int value, GameObject parent)
	{
		string valueAsString = value.ToString();
		int magnitude = valueAsString.Length;

		int index = 0;
		foreach (char digitText in valueAsString)
		{
			GameObject digit = (GameObject)Instantiate(Resources.Load("Prefabs/" + digitText));
			digit.GetComponent<DigitValue>().integerValue = Int32.Parse(digitText.ToString());
			digit.GetComponent<DigitValue>().orderOfMagnitude = magnitude;

			digit.transform.parent = parent.transform;
			digit.transform.localPosition = new Vector3(digitXOffset(index), 0);

			index += 1;
		}
		
	}
	
	private float digitXOffset(int index)
	{
		Transform template = ((GameObject)Instantiate(Resources.Load("Prefabs/0"))).transform.GetChild(0);
		float width = template.gameObject.GetComponent<Renderer>().bounds.size.x;
		return width * index;
	}
}
