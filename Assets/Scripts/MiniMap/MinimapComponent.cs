using UnityEngine;

public enum MinimapType
{
    Player,
    Enemy,
    Powerup,
    Objective
}

public class MinimapComponent : MonoBehaviour
{
    public MinimapType myType;
    
    public SpriteRenderer spr;
    
    void OnEnable()
    {
        switch (myType)
        {
            case MinimapType.Player:
             spr.color = Color.green;
                break;
            case MinimapType.Enemy:
                spr.color = Color.red;
                break;
            case MinimapType.Powerup:
                spr.color = Color.blue;
                break;
            case MinimapType.Objective:
                spr.color = Color.yellow;
                break;
        }
    }
}
