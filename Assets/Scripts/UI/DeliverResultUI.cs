using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverResultUI : MonoBehaviour
{
	private const String POPUP = "Popup";
	
	[SerializeField] private Image background;
	[SerializeField] private Image iconImage;
	[SerializeField] private TextMeshProUGUI messageText;
	[SerializeField] private Color successColor;
	[SerializeField] private Color failColor;
	[SerializeField] private Sprite successSprite;
	[SerializeField] private Sprite failSprite;
	
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	

	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSuccess += Delivery_OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFailed += Delivery_OnRecipeFailed;
		gameObject.SetActive(false);
	}

	private void Delivery_OnRecipeFailed(object sender, EventArgs e)
	{
		gameObject.SetActive(true);
		animator.SetTrigger(POPUP);
		background.color = failColor;
		iconImage.sprite = failSprite;
		messageText.text = "DELIVERY\nFAILED!";
	}

	private void Delivery_OnRecipeSuccess(object sender, EventArgs e)
	{
		gameObject.SetActive(true);
		animator.SetTrigger(POPUP);
		background.color = successColor;
		iconImage.sprite = successSprite;
		messageText.text = "DELIVERY\nSUCCESS!";
	}
	
	
}
