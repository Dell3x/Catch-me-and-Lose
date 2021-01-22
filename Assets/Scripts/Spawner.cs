using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static float speed = 0.5f;

    public StateGame state;

    private float spawnDelay = 2f;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform startPositionObject;
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;

    private void OnEnable()
    {
        GameState.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(StateGame obj)
    {
        state = obj;

        switch (obj)
        {
            case StateGame.MainMenu:
                break;
            case StateGame.Play:
                break;
            case StateGame.Pause:
                break;
            case StateGame.Lose:
                speed = 0.5f;
                break;
            case StateGame.Setting:
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        GameState.GameStateChanged -= OnGameStateChanged;
    }

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.0f, spawnDelay);
    }



    public void Spawn()
    {
        if (state != StateGame.Play)
            return;


        float xPos = Random.Range(leftPosition.position.x, rightPosition.transform.position.x);
        GameObject newCube = Instantiate(cubePrefab, new Vector3(xPos, startPositionObject.position.y, 0), Quaternion.identity);

        CubeMover cube = newCube.GetComponent<CubeMover>();
        cube.SetStartSpeed(speed);
        newCube.transform.SetParent(transform);
    }
}