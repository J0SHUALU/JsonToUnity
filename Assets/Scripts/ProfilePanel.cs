using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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

        Debug.Log(sortedByWeight
            ? "Sort pressed - sorting inventory by weight (high to low)"
            : "Sort pressed - sorting inventory by name (A to Z)");

        BuildInventory(sorted);
    }

    void BuildInventory(LootEntry[] items)
    {
        foreach (Transform child in inventoryContent)
            Destroy(child.gameObject);

        float rowHeight = 50f;
        float spacing = 8f;

        for (int i = 0; i < items.Length; i++)
        {
            GameObject row = Instantiate(inventoryItemPrefab, inventoryContent);

            RectTransform rt = row.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0f, 1f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.pivot = new Vector2(0.5f, 1f);
            rt.offsetMin = new Vector2(0f, rt.offsetMin.y);
            rt.offsetMax = new Vector2(0f, rt.offsetMax.y);
            rt.sizeDelta = new Vector2(0f, rowHeight);
            rt.anchoredPosition = new Vector2(0f, -i * (rowHeight + spacing));

            TMP_Text label = row.GetComponentInChildren<TMP_Text>();
            label.text = items[i].itemName + "  x" + items[i].quantity
                         + "   (weight: " + items[i].weight + ")";
        }
    }

    public void ShowError(string message)
    {
        if (errorText != null)
            errorText.text = message;
    }
}
