using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button playButton;
	[SerializeField] private Button quitButton;

	private void Awake()
	{
		playButton.onClick.AddListener(() =>
		{
			// click
			Loader.Load(Loader.Scene.GameScene);
		});
		
		quitButton.onClick.AddListener(() =>
		{
			// 在editor模式里，这个方法不会起作用
			Application.Quit();
		});
	}
	
}
