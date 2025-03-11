using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject {

	[SerializeField] private KitchenObjectSO input;
	[SerializeField] private KitchenObjectSO output;
	[SerializeField] private float fryingTimerMax;
	
	public KitchenObjectSO GetInput() {
		return input;
	}
	
	public KitchenObjectSO GetOutput() {
		return output;
	}
	
	public float GetFryingTimerMax() {
		return fryingTimerMax;
	}
}
