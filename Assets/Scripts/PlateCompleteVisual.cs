using System;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
	[Serializable]
	public struct KitchenObjectSO_GameObject
	{
		public KitchenObjectSO kitchenObjectSO;
		public GameObject gameObject;
	}
	
	[SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private KitchenObjectSO_GameObject[] kitchenObjectSOGameObjectList;

	private void Start()
	{
		plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

		void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}


}
