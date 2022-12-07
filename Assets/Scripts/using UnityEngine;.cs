using UnityEngine;
using System;
using System.Collections.Generic;         //Allows us to use Lists.
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour
{
    public PolygonCollider2D polygonCollider;
    public int numberOfEnemies  = 5;

    void Start()
    {
        polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
    }
}