using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxiLifeController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float damagePerCollision = 10f;
    private Slider healthBar;
    private Text lifeText;

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentInChildren<Slider>();
        lifeText = GetComponentInChildren<Text>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (lifeText != null)
        {
            UpdateLifeText();
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(damagePerCollision);
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Asegurarse de que la salud no baje de 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de salud si está asignada
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Actualizar el texto del Slider
        if (lifeText != null)
        {
            UpdateLifeText();
        }

    }

    void UpdateLifeText()
    {
        lifeText.text = $"{currentHealth}/{maxHealth}";
    }
}
