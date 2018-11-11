using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	private int _distanceFromCamera = 10;
	
	public int value;
	public string tileOperator = null;
	public int layerToTarget;
	
	private Vector3 _homePosition; // where it will slide back to if not over a valid slot
	public Vector3 targetPosition; // if over a valid slot, will slide to this point.

	void Start()
	{
		_homePosition = transform.position;
	}
	
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
		Vector3 position = _overTarget ? targetPosition : _homePosition;
		slideToPoint(position);
	}

	void slideToPoint(Vector3 point)
	{
		transform.position = Vector3.MoveTowards(transform.position, point, 1);
	}
	
	private Boolean _overTarget;
	void rayCastToTarget()
	{
		int layerMask = 1 << layerToTarget;
		RaycastHit hit;
		_overTarget = Physics.Raycast(
			transform.position, 
			Camera.main.transform.TransformDirection(Camera.main.transform.forward), 
			out hit, Mathf.Infinity, 
			layerMask
		);

		if (_overTarget)
		{
			targetPosition = hit.transform.position;
		}
	}

}
