using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class ClearCounter : BaseCounter {
	
	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// counter中没有KitchenObject
			if (player.HasKitchenObject()) {
				// 如果player中有KitchenObject，则尝试将物体放入counter中
				player.GetKitchenObject().SetKitchenObjectParent(this);
			} else {
				// player没有携带任何KitchenObject
			}
		} else {
			// counter中有KitchenObject
			if (player.HasKitchenObject()) {
				// player有kitchenObject
			} else {
				// player没有kitchenObject
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}
}
