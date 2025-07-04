﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectCounter counter = FindObjectOfType<CollectCounter>();
            if (counter != null)
            {
                counter.AddItem();
            }

            Destroy(gameObject);
        }
    }
}