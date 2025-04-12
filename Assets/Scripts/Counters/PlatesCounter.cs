using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
	public event EventHandler OnPlateSpawn;
	public event EventHandler OnPlateRemoved;

	private float spawnPlateTimer;
	private float spawnPlateTimerMax = 5f;
	private int plateSpawnAmount;
	private int plateSpawnAmountMax = 4;

	[SerializeField] private KitchenObjectSO plateKitchenObjectSO;

	private void Update()
	{
		spawnPlateTimer += Time.deltaTime;
		if (spawnPlateTimer > spawnPlateTimerMax)
		{
			spawnPlateTimer = 0f;
			if (KitchenGameManager.Instance.IsGamePlaying() && plateSpawnAmount < plateSpawnAmountMax)
			{
				plateSpawnAmount++;
				OnPlateSpawn?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public override void Interact(Player player)
	{
		if (!player.HasKitchenObject())
		{
			// Player has no kitchen object
			if (plateSpawnAmount > 0)
			{
				// 存在至少一个盘子
				plateSpawnAmount--;
				KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
				OnPlateRemoved?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}