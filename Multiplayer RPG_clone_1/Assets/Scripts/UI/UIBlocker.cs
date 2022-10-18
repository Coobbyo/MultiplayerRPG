using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlocker : MonoBehaviour
{
	private static UIBlocker instance;

	private void Awake()
	{
		instance = this;
	}

	public static void Show_Static()
	{
		instance.gameObject.SetActive(true);
		instance.transform.SetAsLastSibling();
	}

	public static void Hide_Static()
	{
		instance.gameObject.SetActive(false);
	}
}
