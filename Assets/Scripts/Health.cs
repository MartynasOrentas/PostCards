using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 Offset = new Vector3(0.0f, 1f, 0.0f);

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
    }
    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void UpdateHealthBar(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color =
            Color.Lerp(low, high, slider.normalizedValue);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        UpdateHealthBar(currentHealth, maxHealth);
    }

    void Die()
    {
        // Insert code for player death here
        Debug.Log("Destroyed the mf");
        Destroy(this.gameObject);
    }
}
