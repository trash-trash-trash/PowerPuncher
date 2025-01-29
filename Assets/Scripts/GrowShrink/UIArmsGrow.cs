using UnityEngine;
using UnityEngine.UI;

public class UIArmsGrow : UIGrow
{
   public Image armImage;
   
   public bool left;

   public float moveAmount;

   public float alphaDecayAmount;

   public override void Grow()
   { 
      base.Grow();

      moveAmount += 1.5f* rectTransform.localScale.x;
      
      if (left)
         rectTransform.position = new Vector2(rectTransform.position.x - moveAmount, rectTransform.position.y + moveAmount);
      else
         rectTransform.position = new Vector2(rectTransform.position.x + moveAmount, rectTransform.position.y + moveAmount);

      armImage.color = new Color(armImage.color.r, armImage.color.g, armImage.color.b, armImage.color.a - alphaDecayAmount);
   }
}
