using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
	[SerializeField]
	private float timerStartValue = 60.0f;
	private float timer = 0;
	private int score = 0;

	[SerializeField]
	private int numberOfCollectablesToSpawn = 25;
	[SerializeField]
	private Text timerText;
	[SerializeField]
	private SpawnArea[] spawnAreas;
	[SerializeField]
	private GameObject[] collectablePrefabs;

	private List<Collectable> collectables = new List<Collectable>();

    void Start()
    {
		Random.InitState((int)System.DateTime.Now.Ticks);

		timer = timerStartValue;
		GenerateLevel();
	}

    void Update()
    {
		TickTimer();

		//If timer ticks all the way down, end game
		if (timer <= 0)
		{
			EndGame();
		}

		//Update UI
		timerText.text = ((int)timer).ToString();

		//Scoring
			//If target is returned
				//Assign new target
    }

	private void TickTimer()
	{
		timer -= Time.deltaTime;
		if (timer < 0)
		{
			timer = 0;
		}
	}

	private void GenerateLevel()
	{
		for (int x = 0; x < numberOfCollectablesToSpawn; x++)
		{
			GameObject go = Instantiate(collectablePrefabs[Random.Range(0, collectablePrefabs.Length)], spawnAreas[Random.Range(0, spawnAreas.Length)].GetPositionWithinArea(), Quaternion.identity);
			collectables.Add(go.GetComponent<Collectable>());
		}
		//Randomly place X amount of objects
	}

	private void EndGame()
	{
		//Calculate score

		//Go to score screen
		SceneManager.LoadScene("GameOver");
	}

	public void ChangeTimer(float delta)
	{
		timer += delta;
	}
}
