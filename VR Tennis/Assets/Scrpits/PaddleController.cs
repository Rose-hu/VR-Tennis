using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleController : MonoBehaviour
{
    private Vector3 previousPosition; // 上一帧的位置
    private Vector3 currentVelocity; // 当前速度
    private Vector3 previousVelocity; // 上一帧的速度
    private Vector3 currentAcceleration; // 当前加速度

    public float hitForce; // 击球的力大小
    // private float mass = 0.3f;

    private bool hasCollided = false;

    private DataStorage dataStorage;

    private BallLauncher ballLauncher;

    public static int index = 0;

    // private Vector3 maxVelocity;
    // DataStorage dataStorage = GetComponent<DataStorage>();

    // List<PaddleProperties> paddlePropertiesList = new List<PaddleProperties>();
    private GameObject racket2;

    private InputDevice rightController;
    public XRController leftHandController;
    public GameObject rightHandController;

    public GameObject father;

    private Rigidbody racketRigidbody;

    // [Serilized]
    public bool isTest = true;

    private void Start()
    {
        previousPosition = transform.position;
        currentVelocity = Vector3.zero;
        previousVelocity = Vector3.zero; // 初始化为零向量


        // 创建DataStorage对象的实例
        // dataStorage = new DataStorage();
        dataStorage = GameObject.Find("Data Manager").GetComponent<DataStorage>();

        GameObject racket2 = gameObject;

        // 获取右手控制器

        racketRigidbody = GetComponent<Rigidbody>();

        ballLauncher = FindObjectOfType<BallLauncher>(); // 获取BallLauncher实例

        // Rigidbody racketRigidbody = racket2.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Debug.Log(rightHandController.transform.rotation);
        // Vector3 leftHandPosition = leftHandController.transform.position;
        // Vector3 rightHandPosition = rightHandController.transform.position;
        // Debug.Log(racket2.transform.position);
        Vector3 fatherPos = father.transform.position;

        Vector3 rightHandPosition = rightHandController.transform.position;
        Quaternion rightHandRotation = rightHandController.transform.rotation;

        // racket2.GetComponent<Rigidbody>().MovePosition(fatherPos);

        // 测试
        if (isTest){
            racketRigidbody.MovePosition(fatherPos);
        }else{
            rightHandPosition = new Vector3(rightHandPosition.x, rightHandPosition.y - 0.07f, rightHandPosition.z - 0.07f);
            racketRigidbody.MovePosition(rightHandPosition);
            racketRigidbody.MoveRotation(rightHandRotation * Quaternion.Euler(0, -90, -45));
        }
        // racketRigidbody.MovePosition(fatherPos);
        // 手柄
        // racketRigidbody.MovePosition(rightHandPosition);
        // racketRigidbody.MoveRotation(rightHandRotation * Quaternion.Euler(0, -60, -45));
        // Debug.Log(fatherPos);

        // if (rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position))
        // {
        //     racket2.GetComponent<Rigidbody>().MovePosition(position);
        //         // 使用position进行位置信息的处理或应用
        //     Debug.Log("nmsl");
        // }
        // racket2.GetComponent<Rigidbody>().MovePosition(Pvr_UnitySDKAPI.Controller.UPvr_GetControllerPOS(0)+ground.transform.position);
        // racket2.GetComponent<Rigidbody>().MoveRotation(Pvr_UnitySDKAPI.Controller.UPvr_GetControllerQUA(0) * Quaternion.Euler(90, 0, 0));
        
        hasCollided = false;

        // 计算当前位置与上一帧位置之间的位移，单位是m
        Vector3 displacement = transform.position - previousPosition;
        // Debug.Log(displacement);

        // 计算当前速度（位移除以时间间隔）
        currentVelocity = displacement / Time.fixedDeltaTime;
        // Debug.Log(currentVelocity);

        // if (currentVelocity.x > maxVelocity.x)
        // {
        //     currentVelocity.x = maxVelocity.x;
        // }
        // if (currentVelocity.y > maxVelocity.y)
        // {
        //     currentVelocity.y = maxVelocity.y;
        // }
        // if (currentVelocity.z > maxVelocity.z)
        // {
        //     currentVelocity.z = maxVelocity.z;
        // }

        // 计算当前加速度（速度变化除以时间间隔）
        currentAcceleration = (currentVelocity - previousVelocity) / Time.fixedDeltaTime;
        // Debug.Log(currentVelocity - previousVelocity);

        // 更新上一帧的位置
        previousPosition = transform.position;
        previousVelocity = currentVelocity;

        // Debug.Log(GetPaddleVelocity());
        // Debug.Log(GetPaddleAcceleration());

        // hitForce = mass * currentAcceleration;
        // CalculatePaddleForce(currentAcceleration);
    }

    // 获取球拍的实时速度
    public Vector3 GetPaddleVelocity()
    {
        return currentVelocity;
    }

    // 获取球拍的实时加速度
    public Vector3 GetPaddleAcceleration()
    {
        return currentAcceleration;
    }

    // 计算球拍上的力
    public float CalculatePaddleForce(Vector3 acceleration)
    {
        // 根据牛顿第二定律计算力
        hitForce = acceleration.x * 0.0575f / 1000;

        return hitForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && hasCollided==false )
        {

            // 获取碰撞对象的刚体组件
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // 计算球拍击球时施加的力的方向
            Vector3 hitDirection = collision.contacts[0].normal;

            hasCollided = true;
            // Debug.Log(hitForce);

            // 处理球拍和球的碰撞
            Vector3 position = transform.position;
            Quaternion rotation = rightHandController.transform.rotation;
            dataStorage.AddPaddleData(index, currentVelocity, position, rotation);
            ballLauncher.ReceiveDataFromPaddleController(index, currentVelocity, position, rotation);
            index += 1;
        }
    }

    public void RecordPaddleProperties()
    {
        dataStorage.CheckLength();
        // 创建PaddleProperties对象并添加到列表中
        // PaddleProperties paddleProperties = new PaddleProperties(angle, position);
        // paddlePropertiesList.Add(paddleProperties);

        // 将列表保存为JSON格式的字符串，然后写入文件
        // string json = JsonUtility.ToJson(paddlePropertiesList);
        // string filePath = "path/to/your/file.txt";
        // File.WriteAllText(filePath, json);

        // 在这里你可以将角度、位置等属性记录到你选择的数据结构中
        // 例如，你可以使用List来保存每次碰撞的属性记录
    }

}

