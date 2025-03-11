using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

	public event EventHandler OnPlayerGrabbedObject;
	
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	
	public override void Interact(Player player) {
		if (!player.HasKitchenObject()) {
			// player没有携带任何kitchenObject
			// Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.getPrefab());
			// kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
			KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
		
			OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
		}
	}
	
}