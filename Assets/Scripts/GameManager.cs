using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public AudioSource bgMusicSource;

    public AudioClip sleepingSong;
    public AudioClip wokeSong;

    public bool gameOverState;


    public GameObject player;
    private Player playerScript;

	// Use this for initialization
	void Start () {
        gameOverState = false;
        playerScript = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void GameOver()
    {
        gameOverState = true;
        Debug.Log("GAME OVER!");

        // Show the player open eyes
        playerScript.openEyes();


        //Change music to game over music
        bgMusicSource.Stop();
        bgMusicSource.clip = wokeSong;
        bgMusicSource.Play();
        
        // Fadeout

        // Show days snoozed
        // Show WOKE title
        // Show By George
        // Show Ludum Dare
        // Show Esc to quit
        // Show Spacebar to restart
    }
}
