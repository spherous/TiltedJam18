using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Damagable damagable;

    [SerializeField]
    private Image barImage;

    private void Awake() {
        damagable.OnHealthChanged += UpdateBar;
    }
    private void UpdateBar(int health)
    {
        barImage.fillAmount = (float)health/(float)damagable.maxHealth;
    }
}