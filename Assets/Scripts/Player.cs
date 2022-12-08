using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class Player: MovingObject
{
    private float moveSpeed;
    private int health = 100;
    public TextMeshProUGUI healthUIText;

    protected override void Start()
    {
        moveSpeed = 5f;
        updateHealthText();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        velocity *= moveSpeed;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    public void TakeDamage(int amount)
    {
      health -= amount;
      if (health < 0)
      {
        health = 0;
      }
      Debug.Log("Current health: " + health);
      updateHealthText();
    }

    void updateHealthText()
    {
      Debug.Log("Current health: " + health);
      String healthStr = health.ToString();
      healthUIText.text = healthStr;//string.Format("{0}", health);
      Debug.Log(healthUIText.text);
    }
}
