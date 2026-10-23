using UnityEngine;
using System.IO;
public static class SaveScript
{
    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "game.json");
    public static void Save(GameManager gameManager)
    {
        PlayerDataScript data = new PlayerDataScript(gameManager);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"Saved to {SavePath}");
    }

    public static PlayerDataScript Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found.");
            return null;
        }

        string json = File.ReadAllText(SavePath);
        Debug.Log($"Loaded from {SavePath}");
        return JsonUtility.FromJson<PlayerDataScript>(json);
    }

    public static void Delete()
    {
        if (File.Exists(SavePath))
        { 
            File.Delete(SavePath);
            Debug.Log("Save deleted");
        }
    }
}
