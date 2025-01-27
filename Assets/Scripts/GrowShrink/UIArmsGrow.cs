using UnityEngine;
using UnityEngine.UI;

public class UIArmsGrow : UIGrow
{
   public Image armImage;
   
   public bool left;

   public int armMoveDist;

   public float alphaDecayAmount;

   public override void Grow()
   {
      base.Grow();

      if (left)
         rectTransform.position = new Vector2(rectTransform.position.x - armMoveDist, rectTransform.position.y +armMoveDist);
      else
         rectTransform.position = new Vector2(rectTransform.position.x + armMoveDist, rectTransform.position.y + armMoveDist);

      armImage.color = new Color(armImage.color.r, armImage.color.g, armImage.color.b, armImage.color.a - alphaDecayAmount);
   }
}
