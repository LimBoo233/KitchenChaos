using UnityEngine;

public class DeliveryCounter : BaseCounter
{
	public override void Interact(Player player)
	{
		if (player.HasKitchenObject())
		{
			if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
			{
				// 仅接受plates
				
				DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
				
				plateKitchenObject.DestroySelf();
			}
		}
	}
}
