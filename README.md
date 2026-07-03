# JsonToUnity

A small Unity app that pulls player data from an online API, turns it into C# objects, and shows it in a clean UI.

## What it does

When the app starts, it fetches JSON from an API, reads the data into C# classes, and displays the player's name, level, health, position, and inventory on screen. You can refresh the data or sort the inventory using the buttons.

## API used

https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa

## How it works

The project is split into four scripts so each one has a clear job:

- **JsonModels.cs** - the C# classes that match the shape of the JSON (player, position, inventory items, and metadata).
- **IProfileView.cs** - a small interface that lets the loader talk to the UI without knowing how it works.
- **ProfileLoader.cs** - fetches the JSON from the API using UnityWebRequest and turns it into objects with JsonUtility.
- **ProfilePanel.cs** - takes the data and fills in the UI text and the inventory list.

The data logic (fetching) and the UI logic (displaying) are kept in separate scripts on purpose. This keeps the code tidy and easy to reuse.

## Features

- Fetches and reads JSON data from a live API at runtime.
- Shows the player's name, level, health, position, and full inventory.
- Clean card-style UI with a clear layout and a scrollable inventory list.
- **Refresh button** - reloads the data from the API (uses an event listener).
- **Sort button** - sorts the inventory by weight or by name using LINQ.
- **Error handling** - shows a message if the data fails to load.

## How to run

1. Open the project in Unity 6.
2. Open the MainScene from Assets/Scenes.
3. Press Play.

## Built with

- Unity 6
- C#
- TextMeshPro for the UI text