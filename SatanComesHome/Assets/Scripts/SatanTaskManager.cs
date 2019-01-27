using System.Collections;
using UnityEngine;

public class SatanTaskManager : MonoBehaviour
{
    public string CurrentTask;

    public SpriteRenderer cloudRenderer;
    public SpriteRenderer cloudRendererImageLocation;

    private Coroutine hideCloudRoutine;

    public void AssignRandomItemTask(PickupScriptableObject[] pickupOptions)
    {
        PickupScriptableObject itemToAssign = pickupOptions[Random.Range(0, pickupOptions.Length - 1)];
        AssignNewItemTask(itemToAssign);
    }

    public void AssignNewItemTask(PickupScriptableObject newItemPickup)
    {
        CurrentTask = newItemPickup.Type;
        cloudRendererImageLocation.sprite = newItemPickup.mySprite;
        ShowSpeechCloud();

        if (hideCloudRoutine != null)
            StopCoroutine(hideCloudRoutine);

        hideCloudRoutine = StartCoroutine(HideSpeechBubbleAfterX(2));
    }

    public void HideSpeechCloud()
    {
        cloudRenderer.gameObject.SetActive(false);
        cloudRendererImageLocation.gameObject.SetActive(false);
    }

    public void ShowSpeechCloud()
    {
        cloudRenderer.gameObject.SetActive(true);
        cloudRendererImageLocation.gameObject.SetActive(true);
    }

	public void RemindWantedItem()
	{
		ShowSpeechCloud();
		StartCoroutine(HideSpeechBubbleAfterX(2));
	}

    private IEnumerator HideSpeechBubbleAfterX(float timeToHide)
    {
        yield return new WaitForSeconds(timeToHide);

        HideSpeechCloud();
    }
}
