using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject gameOverGO;
    public GameObject scoreUITextGO;

    public enum GameManagerState
    {
        OPENING,
        GAMEPLAY,
        GAMEOVER,
    }

    GameManagerState GMState;

	// Use this for initialization
	void Start () {
        GMState = GameManagerState.OPENING;
	}
	
	// Update is called once per frame
	void UpdateGameManagerState () {
        switch (GMState)
        {
            case GameManagerState.OPENING:
                //hide game over, set play button visible
                gameOverGO.SetActive(false);
                playButton.SetActive(true);
                break;
            case GameManagerState.GAMEPLAY:
                //reset score, hide play button, set player visible and init lives
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                break;
            case GameManagerState.GAMEOVER:
                //stop enemy spawner, display game over, change game manager state to opening state
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                gameOverGO.SetActive(true);
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
	}

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //starts the game when user clicks play
    public void StartGamePlay()
    {
        GMState = GameManagerState.GAMEPLAY;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.OPENING);
    }
}
