using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarVisualizer : MonoBehaviour
{
    public Image healthBar;

    public void UpdateHealthBar(int maxHealth, int curHealth)
    {
        if (curHealth <= 0)
            return;

        float healthPercentage = (float)curHealth / (float)maxHealth;
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);

        //setColor
        if(healthPercentage <= .25f)
        {
            healthBar.color = Color.red;
        }
        else if(healthPercentage <= .5f)
        {
            healthBar.color = Color.yellow;
        }
    }
}
