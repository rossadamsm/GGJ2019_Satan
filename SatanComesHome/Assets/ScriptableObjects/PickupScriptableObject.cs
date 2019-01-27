using UnityEngine;

[CreateAssetMenu(menuName = "Pickup")]
public class PickupScriptableObject : ScriptableObject
{
    public Sprite mySprite;
	public WeightCategory weightCategory;
    public string Type;
    public AnimatorOverrideController animationOverrideController;
}

public enum WeightCategory
{
	SuperHeavy,
	Heavy,
	Medium,
	Light,
	VeryLight
}