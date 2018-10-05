using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class FocusCircle : MonoBehaviour
{
	const string ANIM_FADEON = "FadeOn";

	[SerializeField] GameObject m_FocusCircleRoot;
	[SerializeField] Animator m_OuterCircleAnim;
	[SerializeField] GameObject m_FocusCircleCenter;
	[SerializeField] GameObject m_ScreenspaceCenter;
	[SerializeField] TrackableType m_FocusCircleTrackableType = TrackableType.PlaneWithinPolygon;

	Vector2 m_CenterScreen;
	ARSessionOrigin m_SessionOrigin;
	ARPlaneManager m_ARPlaneManager;
	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
	static List<ARPlane> s_Planes = new List<ARPlane>();
	bool m_PortraitMode = false;
	
	public Transform FocusCirclePosition
	{
		get { return m_FocusCircleRoot.transform; }
	}

	void Awake()
	{
		m_SessionOrigin = FindObjectOfType<ARSessionOrigin>();
		m_ARPlaneManager = FindObjectOfType<ARPlaneManager>();

		m_PortraitMode = (Input.deviceOrientation == DeviceOrientation.Portrait ||
		                  Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown);
	}

	void Start()
	{
		m_FocusCircleRoot.SetActive(false);
		m_ScreenspaceCenter.SetActive(false);
		
		m_CenterScreen = new Vector2(Screen.width / 2, Screen.height / 2);
	}
	
	void Update () {
		
		// recalculate the center when device orientation changes
		if (m_PortraitMode != (Input.deviceOrientation == DeviceOrientation.Portrait ||
		                       Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown))
		{
			m_CenterScreen = new Vector2(Screen.width / 2, Screen.height / 2);
			m_PortraitMode = !m_PortraitMode;
		}
		

		if (PlanesFoundAndTracking())
		{
			m_FocusCircleRoot.SetActive(true);
			if (m_SessionOrigin.Raycast(m_CenterScreen, s_Hits, m_FocusCircleTrackableType))
			{
				// turn off center cirlce, fade on focus circle, snap focus circle to plane
				m_ScreenspaceCenter.SetActive(false);

				m_FocusCircleCenter.SetActive(true);
				m_OuterCircleAnim.SetBool(ANIM_FADEON, true);

				Pose m_FirstPlanePose = s_Hits[0].pose;

				m_FocusCircleRoot.transform.position = m_FirstPlanePose.position;
				m_FocusCircleRoot.transform.rotation = m_FirstPlanePose.rotation;
			}
			else
			{
				m_FocusCircleRoot.SetActive(false);
				m_ScreenspaceCenter.SetActive(true);
			}
		}
	}
	
	public Vector3 GetCursorPOS()
	{
		return m_FocusCircleRoot.transform.position;
	}

	bool PlanesFoundAndTracking()
	{
		m_ARPlaneManager.GetAllPlanes(s_Planes);		
		return s_Planes.Count > 0;
	}
}
