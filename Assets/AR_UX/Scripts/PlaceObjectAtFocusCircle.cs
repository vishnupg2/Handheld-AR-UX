using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectAtFocusCircle : PlaceObjectAtTransform
{

	[SerializeField] FocusCircle m_FocusCircle;
	
	void Awake()
	{
		placementTransform = m_FocusCircle.FocusCirclePosition;
	}
}
