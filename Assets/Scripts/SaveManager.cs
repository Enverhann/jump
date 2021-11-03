using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }

    // Save the whole state of this script to the player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    // Load the previous saved state from player pref
    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found");
        }
    }
}
