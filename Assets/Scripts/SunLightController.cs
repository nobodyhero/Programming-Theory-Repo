using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunLightController : MonoBehaviour
{
    public float secInThisWorld = 1;
    public int currentHour {
        get { return m_currentHour; }
        private set {
            if ( value < 0 ) {
                Debug.LogError( "[Internal Error] You can't set a netative value for the current hour" );
            }
            else {
                m_currentHour = value;
            }
        }
    }
    public TextMeshProUGUI dateTimeText;


    private int m_currentHour = 13;
    private float minSec;
    private float hourSec;
    private float daySec;
    private float sunDegreePerSecond;
    private int currentMin = 0;
    private int currentDay = 0;
    private int currentMonth = 0;
    private int currentYear = 0;
    private float nextSecToMin;
    private float nextSecToHour;

    //
    private void Awake() {
        minSec = secInThisWorld * 60;
        hourSec = minSec * 60;
        daySec = hourSec * 24;
        sunDegreePerSecond = 360 / daySec;
        nextSecToMin = minSec;
        nextSecToHour = hourSec;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Degree to rotate in a sec * Seconds of a day = Degree should be rotated in a day
        // Degree should be rotated in a day / 60 sec = Rotate degree in a min
        //rotationDegree = DEGREES_PER_SECOND * DAY / ( dayCycleInMinutes * MINUTE );
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround( Vector3.zero, Vector3.right, sunSpeed * Time.deltaTime );
        //transform.LookAt( Vector3.zero );

        //transform.Rotate( new Vector3( sunDegreePerSecond, 0, 0 ) * Time.deltaTime );

        transform.RotateAround( Vector3.zero, Vector3.right, sunDegreePerSecond * Time.deltaTime );
        transform.LookAt( Vector3.zero );

        UpdateDateTimeText();

    }

    private void UpdateDateTimeText() {

        // If the second is less than 0.01, it is not possible to get the value 0 correctly.
        // i.e. When subtracting the delta time of each frame from "nextSecToMin", it will be much less than 0 (e.g. -0.3 etc),
        //      so counting up the minutes is loosing accuracy.
        // That's why if the "secInThisWorld" is less than  0.01, it is not counting the minutes
        if ( secInThisWorld >= 0.01f ) {
            //Debug.Log( $"nextSecToMin: {nextSecToMin}" );
            if ( nextSecToMin > 0 ) {
                nextSecToMin -= Time.deltaTime;
            }
            else {
                nextSecToMin = minSec;
                currentMin += 1;
            }
        }
        else {
            //Debug.Log( $"nextSecToHour: {nextSecToHour}" );
            if ( nextSecToHour > 0 ) {
                nextSecToHour -= Time.deltaTime;
            }
            else {
                nextSecToHour = hourSec;
                currentHour += 1;
            }
        }

        if ( currentMin >= 60 ) {
            currentMin = 0;
            currentHour += 1;
        }

        if ( currentHour >= 24 ) {
            currentHour = 0;
            currentDay += 1;
        }

        if ( currentDay >= 30 ) {
            currentDay = 0;
            currentMonth += 1;
        }

        if ( currentMonth >= 12 ) {
            currentMonth = 0;
            currentYear += 1;
        }

        dateTimeText.text = $"Year: {currentYear}, Month: {currentMonth}, Day: {currentDay}" +
            $"\nTime: {currentHour.ToString().PadLeft(2, '0') }:{currentMin.ToString().PadLeft( 2, '0' )}";
    }
}
