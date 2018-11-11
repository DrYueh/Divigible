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
		
		
		Debug.Log(value);
		GameObject digit = (GameObject)Instantiate(Resources.Load("Prefabs/" + value.ToString()));
		digit.GetComponent<DigitValue>().integerValue = value;
		digit.GetComponent<DigitValue>().orderOfMagnitude = 1;
		digit.transform.parent = obj.transform;
		digit.transform.position = obj.transform.position;
	}
}
