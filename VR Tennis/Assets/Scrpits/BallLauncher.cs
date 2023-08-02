using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab; // 网球预制体
    public Transform launchPoint; // 发射点位置
    private List<GameObject> previousBalls = new List<GameObject>();  //球列表

    public float launchSpeed = 10f; // 发射速度

    private WaitForSeconds delay = new WaitForSeconds(3f); // 3秒延迟

    float minZ = -1f; // 随机范围的最小z值
    float maxZ = 1f; // 随机范围的最大z值

    private DataStorage dataStorage;

    public Canvas canvas; // 引用Canvas对象
    private bool isCanvasshown = false;

    // 从paddlecontroller中传过来的数据
    private int id;
    private Vector3 paddleVelocity;
    private Vector3 paddlePosition;
    private Quaternion parentRotation;
    // 球落点
    private Vector3 ballPosition;
    private int blockNumX;
    private int blockNumZ;
    // 求軌跡
    private Vector3 ballVelocity;

    //引用DATA列表
    public DataStorage data;

    public BallController ballController;

    private void Start()
    {
        dataStorage = GameObject.Find("Data Manager").GetComponent<DataStorage>();
        canvas.gameObject.SetActive(false);

        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     LaunchBall();
        // }
         // 启动协程，循环发球
        // StartCoroutine(LaunchBallCoroutine());

        // StartCoroutine(LaunchBallCoroutineTest());
    }

    private IEnumerator LaunchBallCoroutine()
    {
        while (true)
        {
            Vector3 initialPosition = transform.position;
            // 生成随机位置
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 newPosition = new Vector3(initialPosition.x, initialPosition.y, randomZ);
            transform.position = newPosition;
            LaunchBall();
            // transform.position = newPosition;
            // 等待3秒
            yield return delay;
        }
    }

    private IEnumerator LaunchBallCoroutineTest()
    {
        while (true)
        {
            LaunchBall();
            yield return delay;
        }

        // while(ballController.hasCollided)
        // {
        //     yield return new WaitForSeconds(1f);
        //     LaunchBall();
        // }
    }

    public void LaunchBall()
    {
        // 摧毁之前发射的所有球
        DestroyPreviousBalls();

        data.ballTrajectoryDataList.Clear(); // 清空列表

        GameObject ball = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // 计算发射方向
        Vector3 launchDirection = transform.forward;
        launchDirection = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * launchDirection;

        // 应用速度和角度
        rb.velocity = launchDirection * launchSpeed;

        // 将新球加入到之前发射球的列表中
        previousBalls.Add(ball);
    }

    private void DestroyPreviousBalls()
    {
        foreach (GameObject ball in previousBalls)
        {
            Destroy(ball);
        }

        previousBalls.Clear();
    }

    public void showCanvas()
    {
        canvas.gameObject.SetActive(true);
        isCanvasshown = true;
        Time.timeScale = 0f;
    }

    public void closeCanvas()
    {
        canvas.gameObject.SetActive(false);
        isCanvasshown = false;
        Time.timeScale = 1f;
    }

    public void OnSaveDataButtonClicked()
    {
        // 在这里编写保存数据的逻辑
        // 调用某方法来储存数据
        dataStorage.SavePaddleSingle(id, paddleVelocity, paddlePosition, parentRotation);
        dataStorage.BallLandingSingle(id, ballPosition, blockNumX, blockNumZ);
        dataStorage.SaveBallTrajec();
        data.ballTrajectoryDataList.Clear(); // 清空列表
        closeCanvas();
    }

    public void OnDiscardDataButtonClicked()
    {
        // 在这里编写放弃数据的逻辑
        // DataStorage.DiscardData();
        data.ballTrajectoryDataList.Clear(); // 清空列表
        closeCanvas();
    }

    public void ReceiveDataFromPaddleController(int id, Vector3 paddleVelocity, Vector3 paddlePosition, Quaternion parentRotation)
    {
        this.id = id;
        this.paddleVelocity = paddleVelocity;
        this.paddlePosition = paddlePosition;
        this.parentRotation = parentRotation;
    }

    public void ReceiveDataFromBallController(Vector3 ballPosition, int blockNumX, int blockNumZ)
    {
        this.ballPosition = ballPosition;
        this.blockNumX = blockNumX;
        this.blockNumZ = blockNumZ;
    }

    // public void ReceiveTrajecFromBallController(Vector3 ballPosition, Vector3 ballVelocity)
    // {
    //     this.ballPosition = ballPosition;
    //     this.ballVelocity = ballVelocity;
    // }



    void OnCollisionEnter(Collision collision) {
    //    Debug.Log("开始");
     }
 
    // 碰撞结束
    void OnCollisionExit(Collision collision) {
    //    Debug.Log("结束");
 
    }
}

