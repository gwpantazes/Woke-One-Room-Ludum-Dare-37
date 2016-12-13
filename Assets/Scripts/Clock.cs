using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

    public GameObject gameManager;
    private GameManager gameManagerScript;
    public Sprite[] numbers;    // Array of the number sprites to use with the clock

    // For assigning sprites
    public GameObject tenshour;
    public GameObject hour;
    public GameObject tensminute;
    public GameObject minute;
    public GameObject AM;
    public GameObject PM;
    public GameObject dot;

    private SpriteRenderer tensHourSpriteRenderer;
    private SpriteRenderer hourSpriteRenderer;
    private SpriteRenderer tensminuteSpriteRenderer;
    private SpriteRenderer minuteSpriteRenderer;

    public AudioSource audioSource;

    private float startTime;
    
    public GameObject sun;
    public float sunOffset;

    // We have 360 degrees of rotation to work with, so splitting 360 degrees up into 1440 pieces, we get = 0.002777778 degrees per second
    private float minutesInDay = 1440;     // There are 60 seconds * 60 minutes * 24 hours in a day = 86400
    private float degreesPerMinute = 0.25f;     
    
    private bool alarmRinging;
    private bool alarmRangForTheDay;
    private float totalTimeAlarmRinging;
    public float maxTimeAlarmRinging = 5;

    public float speedup = 0.05f;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        tensHourSpriteRenderer = tenshour.GetComponent<SpriteRenderer>();
        hourSpriteRenderer = hour.GetComponent<SpriteRenderer>();
        tensminuteSpriteRenderer = tensminute.GetComponent<SpriteRenderer>();
        minuteSpriteRenderer = minute.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        gameManagerScript = gameManager.GetComponent<GameManager>();

        alarmRinging = false;
        alarmRangForTheDay = false;
        totalTimeAlarmRinging = 0;
    }
	
	void Update () {

        if(gameManagerScript.gameOverState)
        {
            return;
        }


        // We want to split up the 360 degrees of rotation into a full days worth of alarm clock time
        float sunTime = ((sun.transform.rotation.eulerAngles.y + sunOffset) % 360) / degreesPerMinute;
        int hours = Mathf.FloorToInt(sunTime / 60f);
    
        tensHourSpriteRenderer.sprite = numbers[Mathf.FloorToInt((hours%12) / 10)];
        hourSpriteRenderer.sprite = numbers[(hours%12) % 10];

        int minutes = Mathf.FloorToInt(sunTime % 60);
        tensminuteSpriteRenderer.sprite = numbers[Mathf.FloorToInt(minutes / 10)];
        minuteSpriteRenderer.sprite = numbers[minutes % 10];

        // check for AM / PM here
        if(hours < 12)
        {
            AM.SetActive(true);
            PM.SetActive(false);
        }
        else
        {
            AM.SetActive(false);
            PM.SetActive(true);
        }

        if (hours == 8 && !alarmRinging && !alarmRangForTheDay)
        {    
            StartCoroutine("ringAlarm");
        }
        else if (hours > 8)
        {
            alarmRangForTheDay = false;
        }
        

        if(totalTimeAlarmRinging >= maxTimeAlarmRinging && !gameManagerScript.gameOverState)
        {
            alarmOff();
            gameManagerScript.GameOver();
        }
	}

    private IEnumerator ringAlarm()
    {
        alarmRangForTheDay = true;
        alarmRinging = true;
        audioSource.Play();
        while(totalTimeAlarmRinging < maxTimeAlarmRinging)
        {
            if (!alarmRinging)
            {
                
                yield break;
            }
            else
            {
                totalTimeAlarmRinging += Time.deltaTime;
                dot.SetActive(Time.time % 2 == 0);
            }
            yield return null;
        }
    }

    public void alarmOff()
    {
        alarmRinging = false;
        audioSource.Stop();
        dot.SetActive(true);
        sun.GetComponent<LightRotator>().rotationSpeed += speedup;
        gameManager.GetComponent<AudioSource>().pitch += speedup;
    }
}
