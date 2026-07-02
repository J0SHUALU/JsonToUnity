using System.Linq;
using UnityEngine;
using TMPro;

public class ProfilePanel : MonoBehaviour, IProfileView
{
    public TMP_Text playerNameText;
    public TMP_Text levelText;
    public TMP_Text healthText;
    public TMP_Text positionText;
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;
    public TMP_Text errorText;

    LootEntry[] currentItems;
    bool sortedByWeight;

    public void ShowData(PlayerProfile record)
    {
        if (errorText != null) errorText.text = "";

        playerNameText.text = record.playerName;
        levelText.text = "Level: " + record.level;
        healthText.text = "Health: " + record.health;
        positionText.text = "Position: (" + record.position.x + ", "
                            + record.position.y + ", " + record.position.z + ")";

        currentItems = record.inventory;
        sortedByWeight = false;
        BuildInventory(currentItems);
    }

    public void SortInventory()
    {
        if (currentItems == null) return;

        sortedByWeight = !sortedByWeight;

        LootEntry[] sorted = sortedByWeight
            ? currentItems.OrderByDescending(i => i.weight).ToArray()
            : currentItems.OrderBy(i => i.itemName).ToArray();

        BuildInventory(sorted);
    }

    void BuildInventory(LootEntry[] items)
    {
        foreach (Transform child in inventoryContent)
            Destroy(child.gameObject);

        foreach (LootEntry item in items)
        {
            GameObject row = Instantiate(inventoryItemPrefab, inventoryContent);
            TMP_Text label = row.GetComponentInChildren<TMP_Text>();
            label.text = item.itemName + "  x" + item.quantity + "   (weight: " + item.weight + ")";
        }
    }

    public void ShowError(string message)
    {
        if (errorText != null)
            errorText.text = message;
    }
}