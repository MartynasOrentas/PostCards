using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth;
    public int enemyMaxHealth;
    public Card[] enemyAttackCard;
    public Card[] enemyDefenseCard;
    public Card[] enemySpellCard;
    public TextMeshProUGUI enemyHealthText; // Enemy Health 

    public Health healthScript;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<Health>();
    }
    public Card PlayRandomCard()
    {
        if ((healthScript.currentHealth / healthScript.maxHealth) * 100 > 50)
        {
            return enemyAttackCard[Random.Range(0, enemyAttackCard.Length)];
        }
        if ((healthScript.currentHealth / healthScript.maxHealth) * 100 <= 50)
        {

            return enemySpellCard[Random.Range(0, enemySpellCard.Length)];
        }
        if ((healthScript.currentHealth / healthScript.maxHealth) * 100 <= 20)
        {
            return enemyDefenseCard[Random.Range(0, enemyDefenseCard.Length)];
        }
        return enemyAttackCard[Random.Range(0, enemyAttackCard.Length)];
    }

    public void Die()
    {
        Debug.Log("Destroyed the mf");
        Destroy(this.gameObject);
    }
}
