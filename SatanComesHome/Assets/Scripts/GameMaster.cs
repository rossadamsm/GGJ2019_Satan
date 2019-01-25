using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	private float timerStartValue = 60.0f;
	private float timer = 0;
	private int score = 0;

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

	}

	private void EndGame()
	{

	}

	public void ChangeTimer(float delta)
	{
		timer += delta;
	}
}
