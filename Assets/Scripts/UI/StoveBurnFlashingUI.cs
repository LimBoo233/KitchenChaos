using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour
{
	private const string IS_Flashing = "IsFlashing";
	
	[SerializeField] private StoveCounter stoveCounter;
	
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	
	private void Start()
	{
		stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
		
		animator.SetBool(IS_Flashing, false);
	}

	private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
	{
		float burnShowProgressAmount = .5f;
		bool show = e.progressNormalized >= burnShowProgressAmount;

		if (stoveCounter.IsFired())
		{
			animator.SetBool(IS_Flashing, show);
		}
	}



}
