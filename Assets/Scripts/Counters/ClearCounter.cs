using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class ClearCounter : BaseCounter
{
	[SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
	
	public override void Interact(Player player)
	{
		if (!HasKitchenObject())
		{
			// counter中没有KitchenObject
			if (player.HasKitchenObject())
			{
				// 如果player中有KitchenObject，则尝试将物体放入counter中
				player.GetKitchenObject().SetKitchenObjectParent(this);
			} else
			{
				// player没有携带任何KitchenObject
			}
		} else
		{
			// counter中有KitchenObject
			if (player.HasKitchenObject())
			{
				// player有kitchenObject
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					// player持有一个盘子
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DestroySelf();	
					}
				} else
				{
					// player未持有盘子但是携带了其他物体
					if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
					{
						// counter上有一个盘子
						if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
						{
							player.GetKitchenObject().DestroySelf();
						}
					}
				}
			} else
			{
				// player没有kitchenObject
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}
}