using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Component player;
	private Goal goal;
	private InGameUI ui;

	private float timer;
	private float endTime;

	private Scene nextScene;
	private Scene menuScene;

	private enum GameState { None, LevelStarted, LevelEnd }
	private GameState state = GameState.None;

	void Start()
	{
		//player = FindObjectOfType<Player>();
		goal = FindObjectOfType<Goal>();
		ui = transform.GetComponentInChildren<InGameUI>();
	}

	IEnumerator EndLevelRoutine()
	{
		ui.CenterPrint("You have completed level " + SceneManager.GetActiveScene().buildIndex);
		yield return new WaitForSeconds(1f);

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

		yield return new WaitForSeconds(3.5f);

		if (menuScene.IsValid())
		{
			SceneManager.LoadScene(menuScene.buildIndex);
		}
	}
	
	void Update()
	{
		switch (state)
		{
			case GameState.LevelStarted:

				timer += Time.deltaTime;

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