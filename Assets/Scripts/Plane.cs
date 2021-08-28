using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [INHERITANCE]
// - This class inherits from the Vehicle class
public class Plane : Vehicle
{
    public float heightOffset = 20;
    public float distOffsetMultiplier = 1.5f;

    private Vector3 goalPos;

    // [POLYMORPHISM]
    // - Overriding parent's Awake() and changing the default speed. Here it is not calling parent's Awake().
    new void Awake() {
        speed = 15.0f;
    }

    // Start is called before the first frame update
    // [POLYMORPHISM]
    // - Overriding parent's Start() without calling parent's one (base.Start()),
    //   but it is calling the method declared in the parent's class "SetStartGoalSpot()"
    new void Start()
    {
        SetStartGoalSpot();

        float height = startSpot.transform.position.y + heightOffset;

        startSpot.transform.position = new Vector3( 
            startSpot.transform.position.x * distOffsetMultiplier,
            height, 
            startSpot.transform.position.z * distOffsetMultiplier
        );

        goalSpot.transform.position = new Vector3(
            goalSpot.transform.position.x * distOffsetMultiplier,
            height, 
            goalSpot.transform.position.z * distOffsetMultiplier
        );

        // I don't know why but it must assign the value of variant (goalSpot) declared in the parent class
        // to the variant (goalPos) declared in this class, although I am setting clearly by the above statement.
        // Otherwise the values used in the method Update() is not the value set by the above statement.
        // So the MoveTowards and distance calculation happens in Update() uses the wrong values.
        // For examplem, in Update() method "goalSpot.transform.postion.y" returns the value near 1.1,
        // although I am setting around 20 (varaiant "height") to it in the above statement.
        // As a result, the distance calculation does not work correctly and cannot destory the object when it is near.
        goalPos = goalSpot.transform.position;

        transform.position = startSpot.transform.position;
        // Adjustment of head direction
        transform.LookAt( goalSpot.transform );
    }

    // Update is called once per frame
    // [POLYMORPHISM?]
    // - Parent's Update() is private so it is not possible to override,
    //   therefore this Update() is totally new and only for this class (Plane.cs)
    private void Update()
    {
        transform.position = Vector3.MoveTowards( transform.position, goalPos, Time.deltaTime * speed );
        CheckDistanceAndDestroy( goalPos, transform.position );
    }

}
