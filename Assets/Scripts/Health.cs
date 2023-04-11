using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public Slider slider;
    public GameObject damageText;
    public Color low;
    public Color high;
    public Vector3 Offset = new Vector3(0.0f, 1f, 0.0f);
    private DamagePopup indicator;

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
        indicator =
            Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        indicator.SetDamageText(damageAmount);
        indicator.SetDamageColor(Color.red);
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
        Debug.Log("Changed Color");
        slider.fillRect.GetComponentInChildren<Image>().color =
            Color.Lerp(low, high, slider.normalizedValue);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        indicator =
    Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        indicator.SetDamageText(healAmount);
        indicator.SetDamageColor(Color.green);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar(currentHealth, maxHealth);
    }

    void Die()
    {
        // Insert code for player death here
        Debug.Log("Destroyed the mf");
        Destroy(this.gameObject);
    }
}
