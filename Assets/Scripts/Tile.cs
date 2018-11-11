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
		Vector3 position = overTarget() ? targetPosition : homePosition;
		slideToPoint(position);
	}

	Boolean overTarget()
	{
		// Bit shift the index of the layer (15) to get a bit mask
		int layerMask = 1 << 15;

		// This will cast rays only against colliders in layer 15.
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			Debug.Log("Did Hit");
		}
		else
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
			Debug.Log("Did not Hit");
		}
		
		// I'm thinking ray-cast back until you hit either a "target" or an invisible "non-target" field.
		// non-target field is a large plane that rests right below any valid targets, so other targets on the (future)
		// wheel won't be able to be targeted, because they should be behind this plane.
		return false;
	}

	void slideToPoint(Vector3 point)
	{
		transform.position = Vector3.MoveTowards(transform.position, point, 1);
	}
}
