using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	[SerializeField]
	private float timerStartValue = 60.0f;
	private float timer = 0;
	private int score = 0;

	[SerializeField]
	private int numberOfCollectablesToSpawn = 25;

	private List<Collectable> collectables = new List<Collectable>();

    void Start()
    {
		Random.InitState((int)System.DateTime.Now.Ticks);

		timer = timerStartValue;
    }

    void Update()
    {
		TickTimer();

		//If timer ticks all the way down, end game

		//Update UI

		//Scoring
			//If target is returned
				//Assign new target
    }

	private void TickTimer()
	{
		timer -= Time.deltaTime;
	}

	private void GenerateLevel()
	{
		for (int x = 0; x < numberOfCollectablesToSpawn; x++)
		{

		}
		//Randomly place X amount of objects
	}

	private void EndGame()
	{
		//Calculate score
		
		//Go to score screen
	}

	public void ChangeTimer(float delta)
	{
		timer += delta;
	}
}
