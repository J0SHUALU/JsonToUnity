using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProfileLoader : MonoBehaviour
{
    public string apiUrl = "https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa";
    public ProfilePanel display;
    public Button refreshButton;
    public Button sortButton;

    void Start()
    {
        if (refreshButton != null)
            refreshButton.onClick.AddListener(LoadData);

        if (sortButton != null)
            sortButton.onClick.AddListener(display.SortInventory);

        LoadData();
    }

    public void LoadData()
    {
        StartCoroutine(GetPlayerData());
    }

    IEnumerator GetPlayerData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load data: " + request.error);
            display.ShowError("Could not load data.\n" + request.error);
            yield break;
        }

        string json = request.downloadHandler.text;
        PlayerPayload response = JsonUtility.FromJson<PlayerPayload>(json);

        if (response != null && response.record != null)
            display.ShowData(response.record);
        else
            display.ShowError("Data came back empty or in the wrong format.");
    }
}