using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCounter : MonoBehaviour
{
    public int count = 0;
    public TextMeshProUGUI counterText;

    void Start()
    {
        UpdateUI();
    }

    public void AddItem()
    {
        count++;
        UpdateUI();
    }

    void UpdateUI()
    {
        counterText.text = "Collected: " + count;
    }
}
