using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FadePlaneOnBoundryChange : MonoBehaviour
{

	const string FADEOFF_ANIM = "FadeOff";
	
	Animator m_Animator;
	ARPlane m_Plane;

	void Awake()
	{
		m_Plane = GetComponent<ARPlane>();
		m_Animator = GetComponent<Animator>();
		
		m_Plane.boundaryChanged += PlaneOnBoundaryChanged;
	}

	void PlaneOnBoundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
	{
		m_Animator.SetTrigger(FADEOFF_ANIM);
	}
}
