using System;

[Serializable]
public class PlayerPayload
{
    public PlayerProfile record;
    public BinInfo metadata;
}

[Serializable]
public class PlayerProfile
{
    public string playerName;
    public int level;
    public float health;
    public WorldPoint position;
    public LootEntry[] inventory;
}

[Serializable]
public class WorldPoint
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class LootEntry
{
    public string itemName;
    public int quantity;
    public float weight;
}

[Serializable]
public class BinInfo
{
    public string id;
    public string createdAt;
    public string name;
}