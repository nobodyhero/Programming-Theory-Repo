using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder( 1000 )]

public class StartMenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerNameInput;

    // Start is called before the first frame update
    void Start() {
        playerNameInput.onEndEdit.AddListener( playerNameInputEnd );

        if ( XSceneManager.instance.currentPlayerName != null ) {
            playerNameInput.text = XSceneManager.instance.currentPlayerName;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    // When the start button is clicked
    public void StartBtnClicked() {
        SceneManager.LoadScene( 1 );
    }

    // When the quit button is clicked
    public void QuitBtnClicked() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void playerNameInputEnd( string playerName ) {
        XSceneManager.instance.currentPlayerName = playerName;
    }
}
