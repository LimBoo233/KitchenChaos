using System;
using UnityEngine;

public class StoveCounter : BaseCounter {

	private enum State {
		Idle,
		Frying,
		Fried,
		Burned,
	}
	
	[SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
	[SerializeField] private BurningRecipeSO[] burningRecipeSOArray;  
	
	private State state;
	private float fryingTimer;
	private float burningTimer;
	private FryingRecipeSO fryingRecipeSO;
	private BurningRecipeSO burningRecipeSO;
	
	
	private void Start() {
		state = State.Idle;
	}

	private void Update() {
		if (HasKitchenObject()) {
			switch (state) {
				case State.Idle:
					break;
				case State.Frying:
					fryingTimer += Time.deltaTime;
					if (fryingTimer >= fryingRecipeSO.GetFryingTimerMax()) {
						// Fried
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(fryingRecipeSO.GetOutput(), this);
						
						state = State.Fried;
						burningTimer = 0f;
						burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
					}
					break;
				case State.Fried:
					burningTimer += Time.deltaTime;
					if (burningTimer >= burningRecipeSO.GetBurningTimerMax()) {
						// Burned
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(burningRecipeSO.GetOutput(), this);
						
						state = State.Burned;
					}
					break;
				case State.Burned:
					break;
			}
		}
	}

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// counter中没有KitchenObject
			if (player.HasKitchenObject()) {
				// 如果player中有KitchenObject
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
					// player携带的KitchenObject可以被烹煮
					player.GetKitchenObject().SetKitchenObjectParent(this);
					
					fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
					fryingTimer = 0f;
					state = State.Frying;
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

				state = State.Idle;
			}
		}
	}
	

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
		return GetFryingRecipeSOWithInput(inputKitchenObjectSO) != null;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
		FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
		if (fryingRecipeSO != null) {
			return fryingRecipeSO.GetOutput();
		} else {
			return null;
		}
	}

	private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
		foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
			if (fryingRecipeSO.GetInput() == inputKitchenObjectSO) {
				return fryingRecipeSO;
			}
		}
		return null;
	}

	
	private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO inputKitchenObjectSO) {
		foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
			if (burningRecipeSO.GetInput() == inputKitchenObjectSO) {
				return burningRecipeSO;
			}
		}
		return null;
	}
}
