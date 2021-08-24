using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    // Start is called before the first frame update
    void Start()
    {
        playerNameText.text = XSceneManager.instance.currentPlayerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu() {
        SceneManager.LoadScene( 0 );
    }
}
