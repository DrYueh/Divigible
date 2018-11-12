using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	private int _distanceFromCamera = 14;
	
	public int value;
	public string tileOperator = null;
	public int layerToTarget;
	
    public bool canDrag = true; // Is the tile allowed to move
	private Vector3 _homePosition; // where it will slide back to if not over a valid slot
	public Vector3 targetPosition; // if over a valid slot, will slide to this point.
	private GameObject target;

	public AudioClip clickInSound;

    public void SetTarget(GameObject obj) {
        _overTarget = true;
        target = obj; 
		if (obj == null) {
            transform.SetParent(null);
            targetPosition = _homePosition;
        } else {
            transform.SetParent(obj.transform);
            targetPosition = target.transform.position;
        }
    }

	void Start()
	{
		_homePosition = transform.position;
		GetComponent<AudioSource>().clip = clickInSound;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!_dragging && !lockedIn())
		{
			slide();
		}

		if (target != null)
		{
			targetPosition = target.transform.position;
		} else {
            targetPosition = _homePosition;
        }
	}

	private Boolean _dragging;
	void OnMouseDrag()
	{
        if (!canDrag) return;

		_dragging = true;
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceFromCamera);
		Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		transform.position = objectPosition;
		rayCastToTarget();
		if (!_overTarget)
		{
			SetTarget(null);
			targetPosition = _homePosition;
		}
	}

	void OnMouseUp()
	{
		_dragging = false;
		if (_weShouldPlayClickSound)
		{
			playClickSound();
		}
	}

	void slide()
	{
		slideToPoint(targetPosition);
	}

	void slideToPoint(Vector3 point)
	{
		transform.position = Vector3.MoveTowards(transform.position, point, 1.5f);
	}
	
	private Boolean _overTarget;
	private Boolean _weShouldPlayClickSound;
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

		if (_overTarget && hitNotOccupied(hit))
		{
			SetTarget(hit.transform.gameObject);
			_weShouldPlayClickSound = true;
		}
		else
		{
            SetTarget(null);
			_weShouldPlayClickSound = false;
		}
	}

	private Boolean hitNotOccupied(RaycastHit hit)
	{
		return (hit.transform.childCount < 1) || ((hit.transform.childCount == 1) && (hit.transform.GetChild(0) == transform));
	}

	private Boolean lockedIn()
	{
		return transform.position == targetPosition;
	}

	private void playClickSound()
	{
		GetComponent<AudioSource>().Play();
	}
}
