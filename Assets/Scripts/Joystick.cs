using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour {

	public float minRotation = -60;
	public float maxRotation = 60;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Vector3 currentRotation = transform.localRotation.eulerAngles;
		currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
		transform.localRotation = Quaternion.Euler (currentRotation);
		*/
	}

	private float _startY;
	void OnMouseDown()
	{
		_startY = Input.mousePosition.y;
	}

	private float _endY;
	void OnMouseUp()
	{
		_endY = Input.mousePosition.y;
		if (Mathf.Abs(_endY - _startY) < 0.1) // deadzone
		{
			return;
		}
		if (_endY > _startY)
		{
			// moved up
			GameObject.Find("GameManager").GetComponent<GameManager>().changeTestByDraggingUp(true);
			print("move up");
		} else if (_endY < _startY)
		{
			// moved down
			GameObject.Find("GameManager").GetComponent<GameManager>().changeTestByDraggingUp(false);
			print("move down");
		}
	}
	
	void OnMouseDrag()
	{
		// animate
	}
}
