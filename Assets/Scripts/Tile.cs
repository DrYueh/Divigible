using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	private int _distanceFromCamera = 10;
	
	public int _value;
	
	public Vector3 homePosition; // where it will slide back to if not over a valid slot
	public Vector3 targetPosition; // if over a valid slot, will slide to this point.

	// Update is called once per frame
	void Update ()
	{
		if (!_dragging)
		{
			slide();
		}
	}

	private Boolean _dragging;
	void OnMouseDrag()
	{
		_dragging = true;
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceFromCamera);
		Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		transform.position = objectPosition;
		rayCastToTarget();
	}

	void OnMouseUp()
	{
		_dragging = false;
	}

	void slide()
	{
		Vector3 position = _overTarget ? targetPosition : homePosition;
		slideToPoint(position);
	}

	private Boolean _overTarget;
	void rayCastToTarget()
	{
		// Bit shift the index of the layer (15) to get a bit mask
		int layerMask = 1 << 15;
		RaycastHit hit;
		_overTarget = Physics.Raycast(
			transform.position, 
			transform.TransformDirection(Vector3.forward), 
			out hit, Mathf.Infinity, 
			layerMask
		);

		if (_overTarget)
		{
			targetPosition = hit.transform.position;
		}
	}

	void slideToPoint(Vector3 point)
	{
		transform.position = Vector3.MoveTowards(transform.position, point, 1);
	}
}
