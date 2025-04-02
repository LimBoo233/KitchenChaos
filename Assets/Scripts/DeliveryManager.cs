using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class DeliveryManager : MonoBehaviour
{
	public static DeliveryManager Instance { get; private set; }

	[SerializeField] private RecipeListSO recipeListSO;
	private List<RecipeSO> waitingRecipeSOList;

	private float spawnRecipeTimer;
	private float spawnRecipeTimerMax = 4f;
	private int waitingRecipeMax = 4;

	private Random random;

	private void Awake()
	{
		Instance = this;
		
		random = new Random();
		waitingRecipeSOList = new List<RecipeSO>();
	}

	private void Update()
	{
		spawnRecipeTimer -= Time.deltaTime;
		if (spawnRecipeTimer <= 0f)
		{
			spawnRecipeTimer = spawnRecipeTimerMax;

			if (waitingRecipeSOList.Count < waitingRecipeMax)
			{
				RecipeSO waitingRecipeSO = recipeListSO.GetRecipeSOList()[random.Next(0, recipeListSO.GetRecipeSOList().Count)];
				Debug.Log(waitingRecipeSO.GetRecipeName());
				waitingRecipeSOList.Add(waitingRecipeSO);
			}
		}
	}

	public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
	{
		for (int i = 0; i < waitingRecipeSOList.Count; i++)
		{
			RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

			if (waitingRecipeSO.GetKitchenObjectSOList().Count == plateKitchenObject.GetKitchenObjectSOList().Count)
			{
				bool plateContentsMatchesRecipe = true;
				// 原料数量相同
				foreach (var recipeKitchenObjectSO in waitingRecipeSO.GetKitchenObjectSOList())
				{
					bool ingredientFound = false;
					// 遍历Recipe原料
					foreach (var plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
					{
						// 遍历plate原料
						if (recipeKitchenObjectSO == plateKitchenObjectSO)
						{
							// 原料匹配
							ingredientFound = true;
							break;
						}
					}

					if (!ingredientFound)
					{
						// plate上的原料不匹配
						plateContentsMatchesRecipe = false;
					}
				}

				if (plateContentsMatchesRecipe)
				{
					// 玩家搭配出正确的食谱
					Debug.Log("You delivered the correct recipe!");
					waitingRecipeSOList.RemoveAt(i);
					return;
				}
			}
		}
	}
}
