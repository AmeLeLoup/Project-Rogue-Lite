using UnityEngine;

public interface IDamageble 
{
   void Damage(float damage);

   void Die();
   float MaxHealth { get; set; }
   
   float CurrentHealth { get; set; }
}
