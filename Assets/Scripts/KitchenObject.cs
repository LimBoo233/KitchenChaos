using UnityEngine;

public class KitchenObject : MonoBehaviour {
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	private IKitchenObjectParent kitchenObjectParent;

	public KitchenObjectSO GetKitchenObjectSO() {
		return kitchenObjectSO;
	}

	public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
		if (kitchenObjectParent.HasKitchenObject()) {
			Debug.Log("IKitchenObjectParent已经拥有kitchen object了。");
			return;
		}
		
		if (this.kitchenObjectParent != null) {
			this.kitchenObjectParent.ClearKitchenObject();
		}
		
		this.kitchenObjectParent = kitchenObjectParent;
		kitchenObjectParent.SetKitchenObject(this);
		transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
		transform.localPosition = Vector3.zero;
	}

	public IKitchenObjectParent GetKitchenObjectParent() {
		return kitchenObjectParent;
	}

	public void DestroySelf() {
		GetKitchenObjectParent().ClearKitchenObject();
		
		Destroy(gameObject);
	}

	public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
		Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.getPrefab());
		KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
		kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
		
		return kitchenObject;
	}
}
