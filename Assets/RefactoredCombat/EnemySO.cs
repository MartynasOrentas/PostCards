using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "ScriptableObjects/Enemy")]

public class EnemySO : ScriptableObject
{
    public string enemyName;

    public int rarity;
    public int actionSpeed;
    public int health;

    public Sprite artwork;

    public Card[] attackCards;
    public Card[] defCards;
    public Card[] spellCards;
}
