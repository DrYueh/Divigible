using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	private int _distanceFromCamera = 10;
	
	public int _value;
	
	public Vector2 homePosition; // where it will slide back to if not over a valid slot
	public Vector2 targetPosition; // if over a valid slot, will slide to this point.

	// Update is called once per frame
	void Update ()
	{
		if (!_dragging)
		{
			slide();
		}
	}

	private Boolean _dragging = false;
	void OnMouseDrag()
	{
		_dragging = true;
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceFromCamera);
		Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		transform.position = objectPosition;
	}

	void OnMouseUp()
	{
		_dragging = false;
	}

	void slide()
	{
		if (overTarget())
		{
			slideToPoint(targetPosition);
		}
		else
		{
			slideToPoint(homePosition);
		}
	}

	Boolean overTarget()
	{
		return false;
		// I'm thinking ray-cast back until you hit either a "target" or an invisible "non-target" field.
		// non-target field is a large plane that rests right below any valid targets, so other targets on the (future)
		// wheel won't be able to be targeted, because they should be behind this plane.
	}

	void slideToPoint(Vector3 point)
	{
		transform.position = Vector3.MoveTowards(transform.position, point, 1);
	}
}
