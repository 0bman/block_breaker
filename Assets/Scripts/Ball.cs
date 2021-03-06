﻿using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] float randomFactor = 0.8f;

    Vector2 paddleToBallVector;
    Rigidbody2D rb;
    new AudioSource audio;

    bool hasStoped = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    void Update()
    {
        if (hasStoped)
        {
            LaunchOnMouseClick();
            LockBallToPaddle();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStoped = false;
            rb.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasStoped)
        {
            RbRandomVelocity();
            audio.Play();
        }
    }

    private void RbRandomVelocity()
    {
        float randVal = Random.Range(0.0f, randomFactor);
        Vector2 velocityTweak = new Vector2(randVal, randVal);
        rb.velocity += velocityTweak;
    }
}
