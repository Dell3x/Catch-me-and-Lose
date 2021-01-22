using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StateGame
{
    MainMenu,
    Play,
    Pause,
    Lose,
    Setting
}

public class GameState : MonoBehaviour
{
    public static event Action<StateGame> GameStateChanged;

    [SerializeField] private StateGame _stateGame;

    public StateGame StateCurrentGame => _stateGame;

    private float speedAfterSpawn = 0.1f;

    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject mainPanel;

    void Start()
    {
        StartCoroutine(IncreaseSpeed());
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerLose += ShowLoseScreen;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerLose -= ShowLoseScreen;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (Spawner.speed + speedAfterSpawn < 4f)
            {
                Spawner.speed += speedAfterSpawn;
            }
            else
            {
                break;
            }
        }
    }


    public void mainMenuButton()
    {
        SceneManager.LoadScene("TestLevel", LoadSceneMode.Single);
    }

    public void PlayButton()
    {
        mainPanel.SetActive(false);
        ChangeState(StateGame.Play);

    }



    public void ShowLoseScreen()
    {
        losePanel.SetActive(true);
        ChangeState(StateGame.Lose);
    }

    public void ChangeState(StateGame state)
    {
        _stateGame = state;
        GameStateChanged?.Invoke(_stateGame);
    }

    public void ExitButton()
    {
        Application.Quit();
    }


    #region Debug

    public StateGame newState;

    [ContextMenu("Change current state")]
    public void Change()
    {
        ChangeState(newState);
    }
    #endregion

}