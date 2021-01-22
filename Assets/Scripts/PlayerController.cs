using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action OnPlayerLose;


    private Vector2 clampedPos;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject character;
    [SerializeField] private float speed = 10;
    [SerializeField] private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width;
        rb = character.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
#if PHONE

        while (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x > screenWidth / 2)
            {
                Movement(1.0f);
            }

            if (Input.GetTouch(0).position.x < screenWidth / 2)
            {
                Movement(-1.0f);
            }
        }

#endif

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > screenWidth / 2)
            {
                Movement(1.0f);
            }

            if (Input.mousePosition.x < screenWidth / 2)
            {
                Movement(-1.0f);
            }
        }

    }
    private void Movement(float horizontalinput)
    {
        Vector2 moveVector = new Vector2(horizontalinput * speed * Time.deltaTime, 0);

        transform.Translate(moveVector, Space.Self);

        clampedPos = new Vector2
           (
           Mathf.Clamp(transform.localPosition.x, -screenWidth, screenWidth),
           transform.localPosition.y
           );
        transform.localPosition = clampedPos;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            OnPlayerLose.Invoke();
        }
    }
}
