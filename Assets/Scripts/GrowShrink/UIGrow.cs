using UnityEngine;

public class UIGrow : MonoBehaviour
{
   public RectTransform rectTransform;

   public float growthRate;

   public virtual void Grow()
   {
      rectTransform.localScale *= growthRate;
   }
}
