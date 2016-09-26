using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	[SerializeField]
	private Text centerText;

	[SerializeField]
	private Text timerText;

	void Start()
	{
		if (centerText)
		{
			centerText.gameObject.SetActive(false);
			timerText.gameObject.SetActive(false);
		}
	}

	public void CenterPrint(string message, float time = 1f)
	{
		StartCoroutine(CenterPrintRoutine(message, time));
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

	public void SetTimer(float value)
	{
		timerText.text = value.ToString("0.000");
	}

	public void EnableTimer(bool enable)
	{
		timerText.gameObject.SetActive(enable);
	}
	
	void Update()
	{

	}
}