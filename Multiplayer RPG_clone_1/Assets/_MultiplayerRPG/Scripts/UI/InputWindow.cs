using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputWindow : MonoBehaviour
{
	private static InputWindow instance;

	private Button okButton;
	private Button cancelButton;
	private TextMeshProUGUI titleText;
	private TMP_InputField inputField;

	private void Awake()
	{
		okButton = transform.Find("OKButton").GetComponent<Button>();
		cancelButton = transform.Find("CancelButton").GetComponent<Button>();
		titleText = transform.Find("titleText").GetComponent<TextMeshProUGUI>();
		inputField = transform.Find("inputField").GetComponent<TMP_InputField>();

		Hide();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			//OKButton click
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//CancelButton click
		}
	}

	private void Show(string titleString, string inputString, string validCharacters, int characterLimit)
	{
		gameObject.SetActive(true);
		transform.SetAsLastSibling();

		titleText.text = titleString;

		inputField.characterLimit = characterLimit;
		inputField.onValidateInput = (string text, int charindex, char addedChar) =>
		{
			return ValidateChar(validCharacters, addedChar);
		};

		inputField.text = inputString;
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}

	private char ValidateChar(string validCharacters, char addedChar)
	{
		if(validCharacters.IndexOf(addedChar) != -1)
		{
			//Valid
			return addedChar;
		}else
		{
			//Invalid
			return '\0';
		}
	}

	public static void Show_Static(string titleString, string inputString, string validCharacters, int characterLimit)
	{
		instance.Show(titleString, inputString, validCharacters, characterLimit);
	}

	public static void Show_Static(string titleString, int defaultInt)
	{
		instance.Show(titleString, defaultInt.ToString(), "0123456789-", 20);
	}
}
