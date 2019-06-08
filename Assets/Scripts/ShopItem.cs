using UnityEngine;

public enum ShopItemType
{
    BrawlPro,
    Skin,
    Gamemode,
    Maps,
    Color,
    Currency
}

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;

    public ShopItemType shopItemType;

    public int cost;

    public Sprite sprite;

    public Color backgroundColor;

    public Color foregroundColor = Color.white;

    public int itemID;

    public int amount;
}