using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter {

	public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
	public class OnProgressChangedEventArgs {
		public float progressNormalized;
	}

	public EventHandler OnCut; 

	[SerializeField] private CuttingRecipeSO[] cuttingRecipeArray;

	private int cuttingProgress;
	
	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// counter中没有KitchenObject
			if (player.HasKitchenObject()) {
				// 如果player中有KitchenObject
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
					// player携带的KitchenObject可以被切割
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;

					CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

					OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
						progressNormalized = (float) cuttingProgress / cuttingRecipeSo.GetCuttingProgressMax()
					});
				}
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

	public override void InteractAlternate(Player player) {
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
			// counter中有KitchenObject 并且 KitchenObject可以被切割
			cuttingProgress++;
			
			OnCut?.Invoke(this, EventArgs.Empty);
			
			CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

			OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
				progressNormalized = (float) cuttingProgress / cuttingRecipeSo.GetCuttingProgressMax()
			});
			
			CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

			if (cuttingProgress >= cuttingRecipeSO.GetCuttingProgressMax()) {
				KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
			
				GetKitchenObject().DestroySelf();
			
				KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
			}
		}
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
		return GetCuttingRecipeSOWithInput(inputKitchenObjectSO) != null;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
		CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
		if (cuttingRecipeSo != null) {
			return cuttingRecipeSo.GetOutput();
		} else {
			return null;
		}
	}

	private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArray) {
			if (cuttingRecipeSO.GetInput() == inputKitchenObjectSO) {
				return cuttingRecipeSO;
			}
		}
		return null;
	}
}
