using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectCounterVisual : MonoBehaviour {

	[SerializeField] private BaseCounter baseCounter;
	[SerializeField] private GameObject[] visualGameObjectArray;
	
	private void Start() {
		Player.Instance.OnSelectedCounterChanged += Player_OnOnSelectedCounterChanged;
	}

	private void Player_OnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
		if (e.selectedCounter == baseCounter) {
			Show();
		} else {
			Hide();	
		}
	}

	private void Show() {
		foreach (GameObject visualGameObject in visualGameObjectArray) {
			visualGameObject.SetActive(true);
		}
	}
	
	private void Hide() {
		foreach (GameObject visualGameObject in visualGameObjectArray) {
			visualGameObject.SetActive(false);
		}
	}
}
