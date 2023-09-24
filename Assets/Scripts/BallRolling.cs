using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BallRolling : MonoBehaviour
{
    [SerializeField] private TriangleGeneration triangleGeneration;
    [SerializeField] private GameObject ball;
    
    public float fallSpeed;
    public float radius = 3;

    public Vector3 position;
    public Vector3 velocity;
    
    private Vector3 _oldVelocity;
    private Vector3 accelerationVector;
    
    private float _gravity = 9.81f;
    private float _time;

    private void Start()
    {
        position = new Vector3(0 + radius, 12f + radius, 0 + radius);
        ball.transform.position = position;
    }

    private void FixedUpdate()
    {
        triangleGeneration.GetBarycentricPoint(transform.position);
        BallMovement(Time.fixedDeltaTime);
    }

    private void BallMovement(float deltaTime)
    {
        if (CheckCollision()) // Ball is sliding on plane
        {
            // Normal vector for triangle ball is on
            Vector3 normal = triangleGeneration.normalVector;
            //print("Normal vector: " + normal);
            
            Vector3 gravity = new Vector3(0, -_gravity, 0); // Gravity-force
            Vector3 normalForce = -Vector3.Dot(gravity, normal) * normal; // Normal force
            accelerationVector = gravity + normalForce;

            // Updates the velocity of the ball using equation 8.14
            velocity = _oldVelocity + accelerationVector * deltaTime;
            
            // Updates the position of the ball using equation 8.15
            position = transform.position + velocity * deltaTime;
            transform.position = new Vector3(position.x, triangleGeneration.height + radius, position.z);
            
            _oldVelocity = velocity;
        }
        else
        {
            BallFalling(deltaTime);
        }
    }

    private void BallFalling(float deltaTime)
    {
        float yPos = fallSpeed * deltaTime + 0.5f * -_gravity * deltaTime * deltaTime;
        fallSpeed = fallSpeed + -_gravity * deltaTime;
        transform.Translate(0, yPos, 0);
    }

    private bool CheckCollision()
    {
        // Using (k = C + ((S - C) . n) * n when |(S - C) . n| <= r) to calculate the collision point
        Vector3 pos = transform.position; // C
        Vector3 barycentricPos = triangleGeneration.GetBarycentricPoint(new Vector2(pos.x, pos.z)); // S
        Vector3 normalVec = triangleGeneration.normalVector; // n

        float dotProduct = Vector3.Dot(barycentricPos - pos, normalVec);

        if (Mathf.Abs(dotProduct) <= radius)
        {
            Vector3 collisionPos = pos + dotProduct * normalVec;
            //print("Absolute value: " + Mathf.Abs(dotProduct) + "\nCollision point: " + collisionPos);
            return true;
        }
        
        return false;
    }
}
