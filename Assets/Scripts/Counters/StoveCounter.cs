using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
	
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
	public class OnStateChangedEventArgs : EventArgs {
		public State state;
	}

	public enum State {
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
					
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						progressNormalized = fryingTimer / fryingRecipeSO.GetFryingTimerMax()
					});
					
					if (fryingTimer >= fryingRecipeSO.GetFryingTimerMax()) {
						// Fried
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(fryingRecipeSO.GetOutput(), this);
						
						state = State.Fried;
						burningTimer = 0f;
						burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
						
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Fried });
					}
					break;
				case State.Fried:
					burningTimer += Time.deltaTime;
					
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						progressNormalized = burningTimer / burningRecipeSO.GetBurningTimerMax()
					});
					
					if (burningTimer >= burningRecipeSO.GetBurningTimerMax()) {
						// Burned
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(burningRecipeSO.GetOutput(), this);
						
						state = State.Burned;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Burned });
						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
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
					
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Frying });
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						progressNormalized = fryingTimer / fryingRecipeSO.GetFryingTimerMax()
					});
				}
			} else {
				// player没有携带任何KitchenObject
			}
		} else {
			// counter中有KitchenObject
			if (player.HasKitchenObject()) {
				// player有kitchenObject
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					// player持有一个盘子
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DestroySelf();
						
						state = State.Idle;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Idle });
						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
					}
				}
			} else {
				// player没有kitchenObject
				GetKitchenObject().SetKitchenObjectParent(player);

				state = State.Idle;
				OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Idle });
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
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

	public bool IsFired()
	{
		return state == State.Fried;
	}
}
