 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private float timer = 0f;
    private float destroyTime = 10f;

    private Vector3 previousPosition;

    private Vector3 ballPosition;

    private Vector3 ballVelocity;

    private DataStorage dataStorage;

    public bool hasCollided = false;

    private bool hasHitPaddle = false;

    private BallLauncher ballLauncher;

    private Rigidbody ball;


    // private int index = 0;


    void Start()
    {
        // 保存初始位置作为上一帧的位置
        previousPosition = transform.position;

        ballPosition = transform.position;

        dataStorage = GameObject.Find("Data Manager").GetComponent<DataStorage>();

        ballLauncher = FindObjectOfType<BallLauncher>(); // 获取BallLauncher实例

        ball = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 保存当前位置作为下一帧的上一帧位置
        previousPosition = transform.position;

        ballPosition = transform.position;

        ballVelocity = ball.velocity;

        timer += Time.deltaTime;

        // if (!hasHitPaddle)
        // {
        //     dataStorage.AddBallTrajectoryData(PaddleController.index-1, ballPosition, ballVelocity);
        //     ballLauncher.ReceiveTrajecFromBallController(ballPosition, ballVelocity);
        // }

        if (timer >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // 被击中
            hasHitPaddle = true;
        }   
        if (collision.gameObject.CompareTag("Ground") && hasCollided==false && hasHitPaddle==true)
        {
            // 处理球拍和球的碰撞
            Vector3 position = transform.position;
            int X = GetBlockNumberX(position);
            int Z = GetBlockNumberZ(position);
            dataStorage.AddBallData(PaddleController.index-1, position, X, Z);
            ballLauncher.ReceiveDataFromBallController(position, X, Z);
            // index += 1;
            hasCollided = true;
        }
    }

    public int GetBlockNumberX(Vector3 ballPosition)
    {
        int blockNumX; // 默认值，表示未找到匹配的区域

        // 根据球的位置范围判断区域
        if (ballPosition.x <= 0)
        {
            // 未过网
            blockNumX = -1;
        }
        else if (ballPosition.x > 0f && ballPosition.x <= 1f)
        {
            blockNumX = 0;
        }
        else if (ballPosition.x > 1f && ballPosition.x <= 2f)
        {
            blockNumX = 1;
        }
        else if (ballPosition.x > 2f && ballPosition.x <= 3f)
        {
            blockNumX = 2;
        }
        else if (ballPosition.x > 3f && ballPosition.x <= 4f)
        {
            blockNumX = 3;
        }
        else if (ballPosition.x > 4f && ballPosition.x <= 5f)
        {
            blockNumX = 4;
        }
        else if (ballPosition.x > 5f && ballPosition.x <= 6f)
        {
            blockNumX = 5;
        }
        else if (ballPosition.x > 6f && ballPosition.x <= 7f)
        {
            blockNumX = 6;
        }
        else if (ballPosition.x > 7f && ballPosition.x <= 8f)
        {
            blockNumX = 7;
        }
        else if (ballPosition.x > 8f && ballPosition.x <= 9f)
        {
            blockNumX = 8;
        }
        else if (ballPosition.x > 9f && ballPosition.x <= 10f)
        {
            blockNumX = 9;
        }
        else if (ballPosition.x > 10f && ballPosition.x <= 11f)
        {
            blockNumX = 10;
        }
        else if (ballPosition.x > 11f && ballPosition.x <= 11.8f)
        {
            blockNumX = 11;
        }
        else
        {
            // 出界（出底线）
            blockNumX = -2;
        }

        // 添加其他区域的判断条件...

        return blockNumX;
    }

    public int GetBlockNumberZ(Vector3 ballPosition)
    {
        int blockNumZ = -1; // 默认值，表示未找到匹配的区域

        // 根据球的位置范围判断区域
        if (ballPosition.z <= -5.5 || ballPosition.z >= 5.5)
        {
            // 出界
            blockNumZ = -1;
        }
        else if (ballPosition.z > -5.5f && ballPosition.z <= -5f)
        {
            blockNumZ = 0;
        }
        else if (ballPosition.z > -5f && ballPosition.z <= -4f)
        {
            blockNumZ = 1;
        }
        else if (ballPosition.z > -4f && ballPosition.z <= -3f)
        {
            blockNumZ = 2;
        }
        else if (ballPosition.z > -3f && ballPosition.z <= -2f)
        {
            blockNumZ = 3;
        }
        else if (ballPosition.z > -2f && ballPosition.z <= -1f)
        {
            blockNumZ = 4;
        }
        else if (ballPosition.z > -1f && ballPosition.z <= 0f)
        {
            blockNumZ = 5;
        }
        else if (ballPosition.z > 0f && ballPosition.z <= 1f)
        {
            blockNumZ = 6;
        }
        else if (ballPosition.z > 1f && ballPosition.z <= 2f)
        {
            blockNumZ = 7;
        }
        else if (ballPosition.z > 2f && ballPosition.z <= 3f)
        {
            blockNumZ = 8;
        }
        else if (ballPosition.z > 3f && ballPosition.z < 4f)
        {
            blockNumZ = 9;
        }
        else if (ballPosition.z > 4f && ballPosition.z < 5f)
        {
            blockNumZ = 10;
        }
        else if (ballPosition.z > 5f && ballPosition.z < 5.5f)
        {
            blockNumZ = 11;
        }

        // 添加其他区域的判断条件...

        return blockNumZ;
    }
}

