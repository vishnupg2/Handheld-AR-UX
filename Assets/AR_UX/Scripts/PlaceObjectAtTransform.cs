using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectAtTransform : MonoBehaviour
{

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
	
	public GameObject spawnedObject { get; private set; }

	void Update ()
	{
		if (Input.touchCount > 0)
		{
			if (m_PlacementTransform != null)
			{
				if (spawnedObject == null)
				{
					spawnedObject = Instantiate(m_PlacedPrefab, m_PlacementTransform.position, m_PlacementTransform.rotation);
				}
				else
				{
					spawnedObject.transform.position = m_PlacementTransform.position;
					spawnedObject.transform.rotation = m_PlacementTransform.rotation;
				}
			}
		}
	}
}
