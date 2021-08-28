using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vehicle : MonoBehaviour
{
    // The type or member can be accessed by any other code in the same assembly or another assembly that references it.
    // public

    // The type or member can be accessed only by code in the same class, or in a class that is derived from that class.
    protected Vector3 m_TargetPos;

    // The type or member can be accessed only by code in the same class or struct.
    private float m_Speed = 3.0f;
    private NavMeshAgent m_Agent;
    private GameObject[] spotPrefabs;

    protected GameObject startSpot;
    protected GameObject goalSpot;

    // Properties
    // [ENCAPSULATION]
    // - It cannot set the negative value for the variant speed
    public float speed{
        get { return m_Speed; }
        set {
            if (value < 0.0f ) {
                Debug.LogError( "[Internal Error] You can't set a netative value for the vehicle speed" );
            }
            else {
                m_Speed = value;
            }
        }
    }

    // Awake is called when the script instance is being loaded.
    // * It seems be better to initialize fields in Awake() than Starrt(). 
    //   In Awake(), it would be ok to get the referrence to other scripts or game objects,
    //   but it should avoid to get the value, because the processing of Awake() in referrenced objects might not be finished.
    
    // [POLYMORPHISM]
    // - In order to make this overridden in the child class "protected" is used for this method
    protected void Awake() {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = m_Speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
    }

    // Start is called before the first frame update
    // [POLYMORPHISM]
    // - In order to make this overridden in the child class "protected" is used for this method
    protected void Start()
    {
        // [ABSTRACTION]
        SetStartGoalSpot();

        // Setting the spawn position
        transform.position = startSpot.transform.position;

        // Setting the target position
        m_Agent.destination = goalSpot.transform.position;
    }

    // Update is called once per frame
    // Update() is not supposed to be overridden in the child class so it is "private"
    private void Update()
    {
        // [ABSTRACTION]
        CheckDistanceAndDestroy( goalSpot.transform.position, transform.position);
    }

    protected void SetStartGoalSpot() {
        // Getting all objects tagged "Spot".
        spotPrefabs = GameObject.FindGameObjectsWithTag( "Spot" );

        // Converting the array to the list in order to make it removable
        // It is necessary to renew the list because it is removed below.
        List<GameObject> spotList = new List<GameObject>( spotPrefabs );

        // Getting 2 spots without duplicate
        int spotRandomIndex01 = Random.Range( 0, spotList.Count );
        startSpot = spotList[spotRandomIndex01];
        spotList.RemoveAt( spotRandomIndex01 );

        int spotRandomIndex02 = Random.Range( 0, spotList.Count );
        goalSpot = spotList[spotRandomIndex02];
    }

    protected void CheckDistanceAndDestroy( Vector3 target, Vector3 current ) {
        float distance = Vector3.Distance( target, current );

        if ( distance < 2.0f ) {
            Destroy( gameObject );
        }
    }

}
