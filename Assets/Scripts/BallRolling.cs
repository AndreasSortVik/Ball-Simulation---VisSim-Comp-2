using System;
using UnityEngine;

public class BallRolling : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    //private Vector3 _initialPos;
    public float fallSpeed;

    private void Start()
    {
        ball.transform.position = new Vector3(0, 12.5f, 0);
    }

    private void FixedUpdate()
    {
        BallFalling(Time.deltaTime);
    }

    private void BallFalling(float deltaTime)
    {
        float gravity = 9.81f;
        float yPos = fallSpeed * deltaTime + 0.5f * -gravity * deltaTime * deltaTime;
        fallSpeed = fallSpeed + -gravity * deltaTime;
        
        transform.Translate(0, yPos, 0);
    }
}
