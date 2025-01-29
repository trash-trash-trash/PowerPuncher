using UnityEngine;
using UnityEngine.UI;

public class UIBackGrow : UIGrow
{
    public Image backImage;

    public float alphaDecayAmount = 0.03f;

    public float moveAmount = 0.5f;
    public override void Grow()
    {
        base.Grow();

        if (fulfilled)
            return;
            
        moveAmount += 1 * rectTransform.localScale.y;

        rectTransform.position = new Vector2(rectTransform.position.x, rectTransform.position.y + moveAmount);
        
        backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, backImage.color.a - alphaDecayAmount);
    }
}
