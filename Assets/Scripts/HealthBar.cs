using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;  // The UI Slider component representing the health bar.

    /// <summary>
    /// Sets the maximum health value for the health bar and initializes its display.
    /// </summary>
    /// <param name="maxHealth">
    /// The maximum health value to set for the health bar.
    /// </param>
    /// <remarks>
    /// This method adjusts the slider's maximum value and sets its current value to the maximum health,
    /// ensuring that the health bar starts at full health.
    /// </remarks>
    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
        // Make sure that slider is gonna start at max health
        slider.value = maxHealth;
    }

    /// <summary>
    /// Updates the current health value displayed on the health bar.
    /// </summary>
    /// <param name="currentHealth">
    /// The current health value to display on the health bar.
    /// </param>
    /// <remarks>
    /// This method adjusts the slider's value to reflect the current health of the player or enemy.
    /// </remarks>
    public void SetHealthValue(float currentHealth) {
        slider.value = currentHealth;
    }
}
