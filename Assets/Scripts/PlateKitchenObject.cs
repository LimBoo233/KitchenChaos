using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

	[SerializeField] private List<KitchenObjectSO> kitchenObjectSOList;

	public void Awake()
	{
		kitchenObjectSOList = new List<KitchenObjectSO>();
	}

	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
	{
		if (kitchenObjectSOList.Contains(kitchenObjectSO))
		{
			return false;
		} else
		{
			kitchenObjectSOList.Add(kitchenObjectSO);
			return true;
		}
	}
}
