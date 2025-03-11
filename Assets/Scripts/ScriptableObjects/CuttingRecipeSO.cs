using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject {
	
	[SerializeField] private KitchenObjectSO input;
	[SerializeField] private KitchenObjectSO output;
	[SerializeField] private int cuttingProgressMax;
	
	public KitchenObjectSO GetInput() {
		return input;
	}
	
	public KitchenObjectSO GetOutput() {
		return output;
	}
	
	public int GetCuttingProgressMax() {
		return cuttingProgressMax;
	}

	
}
