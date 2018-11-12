using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowButton : MonoBehaviour
{
	public Boolean up;
    public PuzzleCylinder puzzleCylinder;

	private void OnMouseUpAsButton()
	{
		//throw new System.NotImplementedException();
		if (up)
		{
            puzzleCylinder.rotUp();
			print("move up");
		}
		else
		{
            puzzleCylinder.rotDown();
			print("move down");
		}
	}
}
