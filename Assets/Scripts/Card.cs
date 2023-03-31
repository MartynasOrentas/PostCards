using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public int manaCost;
    public Sprite artwork;
    public string description;
    public int attack;
    public int defense;
    public CardType cardType;
    public Rarity rarity;
}

public enum CardType
{
    Attack,
    Defense,
    Spell
}

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
