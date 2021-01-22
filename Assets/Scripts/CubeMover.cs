using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CubeMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2d;

    private void OnEnable()
    {
        GameState.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(StateGame obj)
    {
        switch (obj)
        {
            case StateGame.MainMenu:
                break;
            case StateGame.Play:
                rigidbody2d.simulated = true;
                rigidbody2d.isKinematic = false;
                break;
            case StateGame.Pause:
                break;
            case StateGame.Lose:
                rigidbody2d.simulated = false;
                rigidbody2d.isKinematic = true;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("FinishLine"))
        {
            Destroy(gameObject);
        }
    }

    internal void SetStartSpeed(float speed)
    {
        rigidbody2d.gravityScale = speed;
    }
}