using System;
using UnityEngine;

public class NormalCan : Can
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}