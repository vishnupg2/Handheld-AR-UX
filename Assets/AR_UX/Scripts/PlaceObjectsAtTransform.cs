using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsAtTransform : MonoBehaviour {

	[SerializeField] GameObject m_PlacedPrefab;

	public GameObject placedPrefab
	{
		get { return m_PlacedPrefab; }
		set { m_PlacedPrefab = value; }
	}

	[SerializeField] Transform m_PlacementTransform;

	public Transform placementTransform
	{
		get { return m_PlacementTransform; }
		set { m_PlacementTransform = value; }
	}
	
	[HideInInspector]
	public List<GameObject> spawnedObjects = new List<GameObject>();

	[Tooltip("Only works if script is on the same object as UI Manager")]
	[SerializeField] bool m_HideUIAfterPlacement = false;

	void Update ()
	{
		if (Input.touchCount > 0)
		{
			Touch m_Touch = Input.GetTouch(0);

			if (m_Touch.phase == TouchPhase.Began)
			{
				if (m_PlacementTransform != null)
				{
					GameObject m_newObj = Instantiate(m_PlacedPrefab, m_PlacementTransform.position, m_PlacementTransform.rotation);
					spawnedObjects.Add(m_newObj);

					// only works if this script is on the same object as UIManager.cs
					if (m_HideUIAfterPlacement)
					{
						UIManager m_Manager = GetComponent<UIManager>();
						if (m_Manager)
						{
							m_Manager.tapToPlaceGameObject.SetActive(false);
						}
					}
				}
			}
		}
	}
}
