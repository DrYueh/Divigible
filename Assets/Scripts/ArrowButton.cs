using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowButton : MonoBehaviour
{
	public Boolean up;

	private void OnMouseUpAsButton()
	{
		//throw new System.NotImplementedException();
		if (up)
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().changeTestByDraggingUp(true);
			print("move up");
		}
		else
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().changeTestByDraggingUp(false);
			print("move down");
		}
	}
}
