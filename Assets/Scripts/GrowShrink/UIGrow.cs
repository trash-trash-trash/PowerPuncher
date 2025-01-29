using UnityEngine;

public class UIGrow : MonoBehaviour
{
   public RectTransform rectTransform;

   public float growthRate;

   public int increaseAmount = 10;

   public int currentAmount = 0;

   public bool fulfilled=false;
   
   public virtual void Grow()
   {  
       if(fulfilled)
           return;
       
       rectTransform.localScale *= growthRate;
      
      currentAmount++;   
      if (currentAmount >= increaseAmount)
         fulfilled = true;
   }
}
