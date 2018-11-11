using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDigitTiles : MonoBehaviour
{
	public GameObject prefab;
	public int numberOfObjects = 10;
	public Vector2 start;
	public Vector2 end;

	void Start() 
	{
		for (int i = 0; i < numberOfObjects; i++)
		{
			Vector3 pos = new Vector3(xCoord(i), start.y, 0);
			Instantiate(prefab, pos, Quaternion.identity);
		}
	}

	float xCoord(int i)
	{
		float totalDistance = end.x - start.x;
		float fraction = i / (float) numberOfObjects;
		return fraction * totalDistance + start.x;
	}
}
