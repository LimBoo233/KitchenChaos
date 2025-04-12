using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }
	
	private const string PLAYER_PREFS_SOUND_VOLUME = "SoundEffectsVolume";
	
	[SerializeField] private AudioClipRefsSO audioClipRefsSO;

	private float volume = 1f;
	
	private void Awake()
	{
		Instance = this;

		volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, 1f);
	}
	
	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSuccess += DeliveryManger_OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFailed += DeliveryManger_OnRecipeFailed;
		CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
		Player.Instance.OnPickSomething += Player_OnPickSomething;
		BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
		TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
	}

	private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
	{
		TrashCounter trashCounter = sender as TrashCounter;
		PlaySound(audioClipRefsSO.GetObjectDrop, trashCounter.transform.position);
	}

	private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
	{
		BaseCounter baseCounter = sender as BaseCounter;
		PlaySound(audioClipRefsSO.GetObjectDrop, baseCounter.transform.position);
	}

	private void Player_OnPickSomething(object sender, EventArgs e)
	{
		PlaySound(audioClipRefsSO.GetObjectPickUp, Player.Instance.transform.position);
	}

	private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
	{
		CuttingCounter cuttingCounter = sender as CuttingCounter;
		PlaySound(audioClipRefsSO.GetChop, cuttingCounter.transform.position);
	}

	private void DeliveryManger_OnRecipeFailed(object sender, EventArgs e)
	{
		DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
		PlaySound(audioClipRefsSO.GetDeliveryFailed, deliveryCounter.transform.position);
	}

	private void DeliveryManger_OnRecipeSuccess(object sender, EventArgs e)
	{
		DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
		PlaySound(audioClipRefsSO.GetDeliverySuccess, deliveryCounter.transform.position);
	}
	
	
	private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
	}

	private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
	{
		PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
	}

	public void PlayFootStepSound(Vector3 position, float volume)
	{
		PlaySound(audioClipRefsSO.GetFootstep, position, volume);
	}

	public void PlayCountDownSound()
	{
		PlaySound(audioClipRefsSO.GetWarning, Vector3.zero);
	}

	public void PlayWarningSound(Vector3 position)
	{
		PlaySound(audioClipRefsSO.GetWarning, position);
	}

	public void ChangeVolume()
	{
		volume += .1f;
		if (volume > 1f)
		{
			volume = 0f;
		}
		
		PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, volume);
		PlayerPrefs.Save();
	}

	public float GetVolume() => volume;

}