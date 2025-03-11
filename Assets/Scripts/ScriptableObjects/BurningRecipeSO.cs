using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject {
	
	[SerializeField] private KitchenObjectSO input;
	[SerializeField] private KitchenObjectSO output;
	[SerializeField] private float burningTimerMax;
	
	public KitchenObjectSO GetInput() {
		return input;
	}
	
	public KitchenObjectSO GetOutput() {
		return output;
	}
	
	public float GetBurningTimerMax() {
		return burningTimerMax;
	}
}
