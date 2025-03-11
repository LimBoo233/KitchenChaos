using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
	
	[SerializeField] private Transform CounterTopPoint;
	
	private KitchenObject kitchenObject;
	
	public virtual void Interact(Player player) {
		Debug.Log("BaseCounter.Interact()永远不该被直接调用。");
	}

	public virtual void InteractAlternate(Player player) {
		Debug.Log("BaseCounter.InteractAlternate()永远不该被直接调用。");
	}
	
	public Transform GetKitchenObjectFollowTransform() {
		return CounterTopPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject) {
		this.kitchenObject = kitchenObject;
	}
	
	public KitchenObject GetKitchenObject() {
		return kitchenObject;
	}

	public void ClearKitchenObject() {
		kitchenObject = null;
	}

	public bool HasKitchenObject() {
		return kitchenObject != null;
	}
}
