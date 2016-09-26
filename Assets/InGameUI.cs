using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	[SerializeField]
	private Text centerText;

	void Start()
	{
		if (centerText)
		{
			centerText.gameObject.SetActive(false);
		}
	}

	public void CenterPrint(string message, float time = 1f)
	{
		StartCoroutine(message, time);
	}

	IEnumerator CenterPrintRoutine(string message, float time)
	{
		if (centerText)
		{
			centerText.gameObject.SetActive(true);
			centerText.text = message;

			yield return new WaitForSeconds(time);

			centerText.gameObject.SetActive(false);
		}
	}
	
	void Update()
	{

	}
}