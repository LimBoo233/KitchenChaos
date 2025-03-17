using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
	[SerializeField] private PlatesCounter platesCounter;
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualPrefab;

	private List<GameObject> plateVisualGameObjectList;

	private void Awake()
	{
		plateVisualGameObjectList = new List<GameObject>();
	}

	private void Start()
	{
		platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
	}

	private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
	{
		GameObject plateVisualGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
		plateVisualGameObjectList.RemoveAt(plateVisualGameObjectList.Count - 1);
		Destroy(plateVisualGameObject);
	}

	private void PlatesCounter_OnPlateSpawn(object sender, EventArgs e)
	{
		Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

		float plateOffSetY = .1f;
		plateVisualTransform.localPosition = new Vector3(0, plateVisualGameObjectList.Count * plateOffSetY, 0);

		plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
	}
}