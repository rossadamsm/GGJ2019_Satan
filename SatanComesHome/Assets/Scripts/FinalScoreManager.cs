using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Amazon.Lambda;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using Amazon.Lambda.Model;
using System.Text.RegularExpressions;
using TMPro;

public class FinalScoreManager : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI playerName;
	public TextMeshProUGUI highscoreText;

	//ScoreObject scoreObject;

	public AudioSource continuesound = null;


	public string IdentityPoolId = "us-east-1:e5dea157-f0fa-4276-868e-f7a55c0b1722";
	public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;
	private RegionEndpoint _CognitoIdentityRegion
	{
		get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
	}
	public string LambdaRegion = RegionEndpoint.USEast1.SystemName;
	private RegionEndpoint _LambdaRegion
	{
		get { return RegionEndpoint.GetBySystemName(LambdaRegion); }
	}

	//public Text ResultText = null;

	void Start()
	{
		//scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreObject>();
		scoreText.text = ScoreManager.instance.score.ToString();

		//continuesound = GetComponent<AudioSource>();

		UnityInitializer.AttachToGameObject(this.gameObject);
		//InvokeButton.onClick.AddListener(() => { Invoke(); });

		AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

		PopulateHighscores();
        SoundManager.instance.PlayCelebrate();
		Cursor.visible = true;
	}

	#region private members

	private IAmazonLambda _lambdaClient;
	private AWSCredentials _credentials;

	private AWSCredentials Credentials
	{
		get
		{
			if (_credentials == null)
				_credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
			return _credentials;
		}
	}

	private IAmazonLambda Client
	{
		get
		{
			if (_lambdaClient == null)
			{
				_lambdaClient = new AmazonLambdaClient(Credentials, _LambdaRegion);
			}
			return _lambdaClient;
		}
	}

	#endregion

	#region Invoke
	/// <summary>
	/// Example method to demostrate Invoke. Invokes the Lambda function with the specified
	/// function name (e.g. helloWorld) with the parameters specified in the Event JSON.
	/// Because no InvokationType is specified, the default 'RequestResponse' is used, meaning
	/// that we expect the AWS Lambda function to return a value.
	/// </summary>
	//public void Invoke()
	//{
	//	ResultText.text = "Invoking '" + FunctionNameText.text + " function in Lambda... \n";
	//	Client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
	//	{
	//		FunctionName = FunctionNameText.text,
	//		Payload = EventText.text
	//	},
	//	(responseObject) =>
	//	{
	//		ResultText.text += "\n";
	//		if (responseObject.Exception == null)
	//		{
	//			ResultText.text += Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()) + "\n";
	//		}
	//		else
	//		{
	//			ResultText.text += responseObject.Exception + "\n";
	//		}
	//	}
	//	);
	//}

	#endregion

	public void SaveScore()
	{
		//Debug.Log("{\"name\" : \"" + playerName.text + "\", \"score\" : " + "}");
		if (playerName.text != "")
		{
			string username = Regex.Replace(playerName.text, "[@&'(\\s)<>#{}\":]", "");
			//ResultText.text = "Invoking '" + FunctionNameText.text + " function in Lambda... \n";
			Client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
			{
				FunctionName = "WriteHighscore",
				Payload = "{\"Name\" : \"" + username.Replace(@"\\u200b","") + "\", \"score\" : " + ScoreManager.instance.score.ToString() + "}"
			},
			   (responseObject) =>
			   {
				//ResultText.text += "\n";
				if (responseObject.Exception == null)
				   {
					   Debug.Log(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()) + "\n");
					   playerName.text = "";

					   PopulateHighscores();

                       //ScoreManager.firstLoad = true;
                       //SimpleSceneFader.ChangeSceneWithFade("MainMenu", 2.0f);
                       //SceneManager.LoadScene("MainMenu");
                   }
                   else
				   {
					   Debug.Log(responseObject.Exception + "\n");

					   playerName.text = "";
					   //SceneManager.LoadScene("MainMenu");
				   }
			   }
			   );
		}

	}

	public void PopulateHighscores()
	{
		//Debug.Log("{\"name\" : \"" + playerName.text + "\", \"score\" : " + "}");
		//ResultText.text = "Invoking '" + FunctionNameText.text + " function in Lambda... \n";
		Client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
		{
			FunctionName = "GetHighscores",
			Payload = "{ \"scores\": 10}"
		},
		(responseObject) =>
		{
			//ResultText.text += "\n";
			if (responseObject.Exception == null)
			{
				string response = Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray());

				Debug.Log(response);
				//highscorePanel
				string scoreString = response.Substring(response.IndexOf('[') + 1, response.LastIndexOf(']') - response.IndexOf('[') - 1);
				string[] scores = scoreString.Split(',');

				highscoreText.text = "HIGHSCORES:\n\n";

				for (int i = 0; i < scores.Length; i++)
				{
					highscoreText.text += (i + 1).ToString() + ") " + scores[i].Substring(scores[i].IndexOf('"') + 1, scores[i].LastIndexOf('\\') - scores[i].IndexOf('\"') - 1) + "\n\n";
                    highscoreText.text = highscoreText.text.Replace(@"\\u200b", "");
                }

				//SceneManager.LoadScene("MainMenu");
			}
			else
			{
				Debug.Log(responseObject.Exception + "\n");
			}
		}
		);
	}

	public void RestartGame()
	{
        //SceneManager.LoadScene("Main");
        SoundManager.instance.musicSource.Stop();
		SimpleSceneFader.ChangeSceneWithFade("Main", 1f);
	}
}

