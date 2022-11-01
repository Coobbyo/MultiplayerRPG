using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble3D : MonoBehaviour
{
	public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text)
	{
		Transform chatBubbleTransform = Instantiate(GameAssets.Instance.chatBubble3DPrefab, parent);
		chatBubbleTransform.localPosition = localPosition;

		chatBubbleTransform.GetComponent<ChatBubble3D>().SetUp(iconType, text);

		Destroy(chatBubbleTransform.gameObject, 5f);
	}


	public enum IconType
	{
		Happy,
		Neutral,
		Angry,
	}

	[SerializeField] private Sprite happyIconSprite = null;
	[SerializeField] private Sprite neutralIconSprite = null;
	[SerializeField] private Sprite angryIconSprite = null;

	private SpriteRenderer backgroundSpriteRenderer;
	private Transform backgroundCube;
	private SpriteRenderer iconSpriteRenederer;
	private TextMeshPro textMeshPro;

	private void Awake()
	{
		backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
		backgroundCube = transform.Find("BackgroundCube");
		iconSpriteRenederer =  transform.Find("Icon").GetComponent<SpriteRenderer>();
		textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
	}

	private void SetUp(IconType iconType, string text)
	{
		textMeshPro.SetText(text);
		textMeshPro.ForceMeshUpdate();
		Vector2 textSize = textMeshPro.GetRenderedValues(false);
		
		Vector2 padding = new Vector2(10f, 4f);
		backgroundSpriteRenderer.size = textSize + padding;
		backgroundCube.localScale = textSize + padding * .5f;

		Vector3 offset = new Vector3(-14.5f, 0f);
		backgroundSpriteRenderer.transform.localPosition =
			new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;
		backgroundCube.localPosition =
            new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f, +0.51f) + offset;

		iconSpriteRenederer.sprite = GetIconSprite(iconType);

		//TextWriter.AddWriter_Static(textMeshPro, text, 0.03f, true, true, () => { });
	}

	private Sprite GetIconSprite(IconType iconType)
	{
		switch(iconType)
		{
			default:
			case IconType.Happy: return happyIconSprite;
			case IconType.Neutral: return neutralIconSprite;
			case IconType.Angry: return angryIconSprite;
		}
	}
}
