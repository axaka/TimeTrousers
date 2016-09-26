using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Component player;
	private Goal goal;
	private InGameUI ui;

	private bool started;

	private float timer;
	private float endTime;

	private Scene nextScene;
	private Scene menuScene;

	private enum GameState { None, PreLevelStart, LevelStarted, LevelEnd }
	private GameState state = GameState.PreLevelStart;

	void Start()
	{
		//player = FindObjectOfType<Player>();
		goal = FindObjectOfType<Goal>();
		ui = transform.GetComponentInChildren<InGameUI>();
	}

	IEnumerator StartLevelRoutine()
	{
		started = true;
		int countDownTime = 3;

		while (countDownTime > 0)
		{
			ui.CenterPrint(countDownTime.ToString(), 0.35f);
			yield return new WaitForSeconds(0.35f);
			countDownTime--;
		}

		state = GameState.LevelStarted;
	}

	IEnumerator EndLevelRoutine()
	{
		ui.CenterPrint("You have completed level " + SceneManager.GetActiveScene().buildIndex);
		yield return new WaitForSeconds(1f);

		started = false;

		if (nextScene.IsValid())
		{
			SceneManager.LoadScene(nextScene.buildIndex);
		}
		else
		{
			StartCoroutine(WinGameRoutine());
		}
	}

	IEnumerator WinGameRoutine()
	{
		ui.CenterPrint("You have completed all levels!", 3f);

		yield return new WaitForSeconds(3f);

		if (menuScene.IsValid())
		{
			SceneManager.LoadScene(menuScene.buildIndex);
		}
	}

	void Update()
	{
		switch (state)
		{
			case GameState.PreLevelStart:

				if (!started)
				{
					StartCoroutine(StartLevelRoutine());
				}

				break;

			case GameState.LevelStarted:

				timer += Time.deltaTime;
				ui.EnableTimer(true);
				ui.SetTimer(timer);

				if (goal.HasPlayer)
				{
					state = GameState.LevelEnd;
					StartCoroutine(EndLevelRoutine());
				}

				break;

			case GameState.LevelEnd:

				endTime = timer;

				break;
		}
	}
}