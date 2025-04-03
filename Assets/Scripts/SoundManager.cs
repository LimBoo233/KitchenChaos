using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }
	
	[SerializeField] private AudioClipRefsSO audioClipRefsSO;
	
	private void Awake()
	{
		Instance = this;
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
	
	
	private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClip, position, volume);
	}

	private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
	{
		PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
	}

	public void PlayFootStepSound(Vector3 position, float volume = 1f)
	{
		PlaySound(audioClipRefsSO.GetFootstep, position, volume);
	}

}