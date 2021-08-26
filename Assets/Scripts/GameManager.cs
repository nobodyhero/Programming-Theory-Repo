using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float spawnVehicleInterval = 3.0f; 
    
    public TextMeshProUGUI playerNameText;
    
    private GameObject[] vehiclePrefabs;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        string playerName;

        if ( XSceneManager.instance != null ) {
            playerName = XSceneManager.instance.currentPlayerName;
        }
        else {
            playerName = "Unknown Player";
        }
        playerNameText.text = $"{playerName}'s City";

        vehiclePrefabs = Resources.LoadAll<GameObject>( "Prefabs/Vehicles" );

        startTime = Time.time;

        StartCoroutine( SponeVehicle( spawnVehicleInterval ) );

    }

    // Update is called once per frame
    void Update()
    {
    }


    private IEnumerator SponeVehicle( float waitTime ) {
        while ( true ) {
            yield return new WaitForSeconds( waitTime );
            int vehicleIndex = Random.Range( 0, vehiclePrefabs.Length );
            Instantiate( vehiclePrefabs[vehicleIndex] );
        }
    }


    public void BackToMenu() {
        SceneManager.LoadScene( 0 );
    }


}
