using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
	private Player player;
	private float footStepTimer;
	private float footStepTimerMax = .1f;

	public void Awake()
	{
		player = GetComponent<Player>();
	}
	
	private void Update()
	{
		footStepTimer -= Time.deltaTime;
		if (footStepTimer < 0f)
		{
			footStepTimer = footStepTimerMax;

			if (player.IsWalking())
			{
				float volumn = 1f;
				SoundManager.Instance.PlayFootStepSound(player.transform.position, volumn);
			}
		}
	}
}
