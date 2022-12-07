using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TrapEffects;

public class SpikeTrapBehaviorScript : MonoBehaviour
{
    public int damage;
    
    void OnTriggerEnter2D(Collider2D other)
    {
      Debug.Log(string.Format("haha idiot you stepped in the KNIFE PILE and took {0} damage!", damage));
      //method from TrapEffects
    }
}
