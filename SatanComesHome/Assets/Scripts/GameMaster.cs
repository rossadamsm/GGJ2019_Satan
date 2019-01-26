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
	public int score = 0;
	public int priestSpawnInterval = 100;
	public int priestScoreCounter = 0;

	[SerializeField]
	private int numberOfCollectablesToSpawn = 25;
	[SerializeField]
	private Text timerText;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private SpawnArea[] spawnAreas;
	[SerializeField]
	private GameObject collectablePrefab;
	[SerializeField]
	private PickupScriptableObject[] collectableData;
	[SerializeField]
	private GameObject priestPrefab;

	public List<Collectable> collectables = new List<Collectable>();
    public SatanTaskManager satanTaskManager;

	public static GameMaster instance;

	bool gameOver = false;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;

			Random.InitState((int)System.DateTime.Now.Ticks);

			timer = timerStartValue;
			GenerateLevel();
			satanTaskManager = FindObjectOfType<SatanTaskManager>();
			satanTaskManager.HideSpeechCloud();

			priestScoreCounter = priestSpawnInterval;

			Instantiate(priestPrefab, spawnAreas[Random.Range(0, spawnAreas.Length)].GetPositionWithinArea(), Quaternion.identity);
		}
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void SetNewTarget()
	{
		satanTaskManager.AssignNewItemTask(collectables[Random.Range(0, collectables.Count)].pickupObject);
	}

	public void ChangeScore(int delta)
	{
		score += delta;
		priestScoreCounter -= delta;
		if (priestScoreCounter <= 0)
		{
			priestScoreCounter = priestSpawnInterval;
			Instantiate(priestPrefab, spawnAreas[Random.Range(0, spawnAreas.Length)].GetPositionWithinArea(), Quaternion.identity);
		}
		if (collectables.Count == 0)
		{
			//Happy satan
			EndGame();
		}
	}

    void Update()
    {
		if (!gameOver)
		{
			TickTimer();

			//If timer ticks all the way down, end game
			if (timer <= 0)
			{
				EndGame();
			}

			//Update UI
			timerText.text = ((int)timer).ToString();
			scoreText.text = (score).ToString();
		}

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
			GameObject go = Instantiate(collectablePrefab, spawnAreas[Random.Range(0, spawnAreas.Length)].GetPositionWithinArea(), Quaternion.identity);
			Collectable collectable = go.GetComponent<Collectable>();
			collectable.Init(collectableData[Random.RandomRange(0, collectableData.Length)]);
			collectables.Add(collectable);
		}
		//Randomly place X amount of objects
	}

	private void EndGame()
	{
		//Calculate score
		ScoreManager.instance.score = score;

		//Go to score screen
		gameOver = true;
		SceneManager.LoadScene("GameOver");
	}

	public void ChangeTimer(float delta)
	{
		timer += delta;
	}
}
