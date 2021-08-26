using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class XSceneManager : MonoBehaviour
{
    const string SAVE_FILE_NAME = "/saveFile.json";
    public static XSceneManager instance { get; private set; }
    public string currentPlayerName;

    [System.Serializable]
    class SaveData {
        public string playerName;
    }

    // Called as soon as the object is created
    private void Awake() {

        if ( instance != null ) {
            Destroy( gameObject );
            return;
        }

        instance = this; // the current instance of MainManager
        DontDestroyOnLoad( gameObject );
    }

    public void SaveGameData() {
        SaveData data = new SaveData();
        data.playerName = currentPlayerName;

        string json = JsonUtility.ToJson( data );

        File.WriteAllText( Application.persistentDataPath + SAVE_FILE_NAME, json );
    }

    public void LoadGameData() {

        string path = Application.persistentDataPath + SAVE_FILE_NAME;

        if ( File.Exists( path ) ) {
            string json = File.ReadAllText( path );
            SaveData data = JsonUtility.FromJson<SaveData>( json );

            currentPlayerName = data.playerName;
        }
    }
}
