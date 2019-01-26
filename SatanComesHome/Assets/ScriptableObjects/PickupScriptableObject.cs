using UnityEngine;

[CreateAssetMenu(menuName = "Pickup")]
public class PickupScriptableObject : ScriptableObject
{
    public Sprite mySprite;
    public float TimeToAdd;
    public float SpeedModifier;
    public int PointValue;
    public string Type;
    public AnimatorOverrideController animationOverrideController;
}
