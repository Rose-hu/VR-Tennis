using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncherTest : MonoBehaviour
{
    public GameObject ballPrefab; // 网球预制体
    public Transform launchPoint; // 发射点位置

    public float launchSpeed = 10f; // 发射速度

    private WaitForSeconds delay = new WaitForSeconds(3f); // 3秒延迟

    private void Start()
    {
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     LaunchBall();
        // }
         // 启动协程，循环发球
        StartCoroutine(LaunchBallCoroutineTest());
    }

    private IEnumerator LaunchBallCoroutineTest()
    {
        while (true)
        {
            // Vector3 initialPosition = transform.position;
            // // 生成随机位置
            // float randomZ = Random.Range(minZ, maxZ);
            // Vector3 newPosition = new Vector3(initialPosition.x, initialPosition.y, randomZ);
            // transform.position = newPosition;
            LaunchBall();
            // transform.position = newPosition;
            // 等待3秒
            yield return delay;
        }
    }

    private void LaunchBall()
    {
        GameObject ball = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // 计算发射方向
        Vector3 launchDirection = transform.forward;
        launchDirection = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * launchDirection;

        // 应用速度和角度
        rb.velocity = launchDirection * launchSpeed;
    }

    void OnCollisionEnter(Collision collision) {
    //    Debug.Log("开始");
     }
 
    // 碰撞结束
    void OnCollisionExit(Collision collision) {
    //    Debug.Log("结束");
 
    }
}

