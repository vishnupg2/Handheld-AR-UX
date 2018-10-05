using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsAtFocusCircle : PlaceObjectsAtTransform {

	[SerializeField] FocusCircle m_FocusCircle;
	
	void Awake()
	{
		placementTransform = m_FocusCircle.FocusCirclePosition;
	}
}
