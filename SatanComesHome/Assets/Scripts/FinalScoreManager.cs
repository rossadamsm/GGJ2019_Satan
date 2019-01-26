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

public class FinalScoreManager : MonoBehaviour
{
	public Text scoreText;
	public Text playerName;

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
				Payload = "{\"Name\" : \"" + username + "\", \"score\" : " + ScoreManager.instance.score.ToString() + "}"
			},
			   (responseObject) =>
			   {
				//ResultText.text += "\n";
				if (responseObject.Exception == null)
				   {
					   Debug.Log(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()) + "\n");
					   //ScoreManager.firstLoad = true;
					//SimpleSceneFader.ChangeSceneWithFade("MainMenu", 2.0f);
					SceneManager.LoadScene("MainMenu");
				   }
				   else
				   {
					   Debug.Log(responseObject.Exception + "\n");
					   SceneManager.LoadScene("MainMenu");
				   }
			   }
			   );
		}
	}
}

