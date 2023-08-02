using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private float sphereRadius;
    private Rigidbody ballRigidbody;

    private BallLauncher ballLauncher;

    private DataStorage dataStorage;

    private bool hit = false;

    private bool touchPaddle = false;

    private WaitForSeconds delay = new WaitForSeconds(0.5f); // 3秒延迟


    [SerializeField]
    private int divider = 0;

    private void Start()
    {

        // 获取球的半径和刚体组件
        sphereRadius = GetComponent<SphereCollider>().radius;
        ballRigidbody = GetComponent<Rigidbody>();

        dataStorage = GameObject.Find("Data Manager").GetComponent<DataStorage>();

        ballLauncher = FindObjectOfType<BallLauncher>(); // 获取BallLauncher实例

        hit = false;

        touchPaddle = false;
    }

    private void FixedUpdate()
    {
        // 获取球的当前位置和速度
        Vector3 currentPosition = transform.position;
        Vector3 velocity = ballRigidbody.velocity;

        // Debug.Log(velocity);

        if(!touchPaddle)
        {
            dataStorage.AddBallTrajectoryData(PaddleController.index, currentPosition, velocity);
            // ballLauncher.ReceiveTrajecFromBallController(currentPosition, velocity);
        }

        if (velocity.x >= 50.0f)
        {
            ballRigidbody.velocity = velocity / divider;
        }
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        // Vector3 velocity = ballRigidbody.velocity
        if(hit&&transform.position.x>=0.2f)
        {
            // yield return delay;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Ballcollision");
            Debug.Log(hit);

            ballLauncher.showCanvas();
            hit = false;
        }
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Debug.Log(velocity);
            hit = true;
            touchPaddle = true;
        }
    }
}
