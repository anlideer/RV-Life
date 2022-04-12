using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentDisplay : MonoBehaviour
{
    [Header("Sprites from empty to full amount")]
    public Sprite[] sprites;
    public float warningAmount = 0.05f;

    // set amount 0-1
    public void SetAmount(float amount)
    {
        int len = sprites.Length - 1;   // except the "empty" one
        float cell = 1f / len;
        
        if (amount <= warningAmount)
        {
            GetComponent<Image>().sprite = sprites[0];
            return;
        }

        for (int i = len; i > 0; i--)
        {
            if (amount >= (i-1)*cell)
            {
                GetComponent<Image>().sprite = sprites[i];
                break;
            }
        }
    }
}
