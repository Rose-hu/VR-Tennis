using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct PaddleData
{
    public int id;
    public Vector3 paddleVelocity;
    public Vector3 paddlePosition;  
    public Quaternion parentRotation;
}

[System.Serializable]
public struct BallData
{
    public int id;
    public Vector3 ballPosition;
    public int blockNumX;
    public int blockNumZ;
}

[System.Serializable]
public struct BallTrajectoryData
{
    public int id;
    public Vector3 ballPosition;
    public Vector3 ballVelocity;
}

[System.Serializable]
public class DataStorage : MonoBehaviour
// public class DataStorage
{
    public List<PaddleData> paddleDataList = new List<PaddleData>();

    public List<BallData> ballDataList = new List<BallData>();

    public List<BallTrajectoryData> ballTrajectoryDataList = new List<BallTrajectoryData>();

    public void AddPaddleData(int id, Vector3 paddleVelocity, Vector3 paddlePosition, Quaternion parentRotation)
    {
        PaddleData data = new PaddleData();
        data.id = id;
        data.paddleVelocity = paddleVelocity;
        data.paddlePosition = paddlePosition;
        data.parentRotation = parentRotation;

        paddleDataList.Add(data);
    }

    public void AddBallData(int id, Vector3 ballPosition, int blockNumX, int blockNumZ)
    {
        BallData ball_data = new BallData();
        ball_data.id = id;
        ball_data.ballPosition = ballPosition;
        ball_data.blockNumX = blockNumX;
        ball_data.blockNumZ = blockNumZ;

        ballDataList.Add(ball_data);
    }

    public void AddBallTrajectoryData(int id, Vector3 ballPosition, Vector3 ballVelocity)
    {
        BallTrajectoryData tra_data = new BallTrajectoryData();
        tra_data.id = id;
        tra_data.ballPosition = ballPosition;
        tra_data.ballVelocity = ballVelocity;

        ballTrajectoryDataList.Add(tra_data);
    }

    public void showData()
    {
        StringBuilder dataString = new StringBuilder();

        foreach (PaddleData data in paddleDataList)
        {
            dataString.AppendLine(data.ToString());
        }
    }

    public void SavePaddleData()
    {
        // paddleDataList = 
        string filePath = "C:/Users/rose/Desktop/论文/data.csv";

        Debug.Log("进入");
        Debug.Log(paddleDataList.Count);

        // 创建或追加到现有文件
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            // 添加列名行
            bool isFileEmpty = new FileInfo(filePath).Length == 0;

            if (isFileEmpty)
            {
                // 列名为空，添加列名行
                writer.WriteLine("index,PaddleVelocityX,PaddleVelocityY,PaddleVelocityZ,PaddlePositionX,PaddlePositionY,PaddlePositionZ,ParentRotationX,ParentRotationY,ParentRotationZ,ParentRotationW");
            }

            // 添加数据行
            foreach (PaddleData data in paddleDataList)
            {
                Debug.Log(data.id);
                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                    data.id,
                    data.paddleVelocity.x, data.paddleVelocity.y, data.paddleVelocity.z,
                    data.paddlePosition.x, data.paddlePosition.y, data.paddlePosition.z,
                    data.parentRotation.x, data.parentRotation.y, data.parentRotation.z, data.parentRotation.w);

                writer.WriteLine(line);
            }



            writer.Close();
        }
    }
    
    public void BallLandingData()
    {
        // paddleDataList = 
        string filePath = "C:/Users/rose/Desktop/论文/balldata.csv";

        // Debug.Log(paddleDataList[0].id);

        // 创建或追加到现有文件
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            // 添加列名行
            bool isFileEmpty = new FileInfo(filePath).Length == 0;

            if (isFileEmpty)
            {
                // 添加列名行
                writer.WriteLine("index,BallPositionX,BallPositionY,BallPositionZ,BlockNumberX,BlockNumberZ");
            }

            // 添加数据行
            foreach (BallData data in ballDataList)
            {

                Debug.Log(data.id);
                string line = string.Format("{0},{1},{2},{3},{4},{5}",
                    data.id,
                    data.ballPosition.x, data.ballPosition.y, data.ballPosition.z,
                    data.blockNumX, data.blockNumZ);

                writer.WriteLine(line);
            }

            writer.Close();
        }
    }

    public void SaveBallTrajec()
    {
        // paddleDataList = 
        string filePath = "C:/Users/rose/Desktop/论文/ballTrajecData.csv";

        // 创建或追加到现有文件
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            // 添加列名行
            bool isFileEmpty = new FileInfo(filePath).Length == 0;

            if (isFileEmpty)
            {
                // 添加列名行
                writer.WriteLine("index,BallPositionX,BallPositionY,BallPositionZ,BallVelocityX,BallVelocityY,BallVelocityZ");
            }

            // 添加数据行
            foreach (BallTrajectoryData data in ballTrajectoryDataList)
            {

                // Debug.Log(data.id);
                string line = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                    data.id,
                    data.ballPosition.x, data.ballPosition.y, data.ballPosition.z,
                    data.ballVelocity.x, data.ballVelocity.y, data.ballVelocity.z);

                writer.WriteLine(line);
            }

            writer.Close();
        }
        // foreach (GameObject obj in gameObjects) {
        //     obj = null; // 清空列表中每个GameObject实例
        // }
    }

    public void SavePaddleSingle(int id, Vector3 paddleVelocity, Vector3 paddlePosition, Quaternion parentRotation)
    {
        // paddleDataList = 
        string filePath = "C:/Users/rose/Desktop/论文/paddleDataSingle.csv";

        // 创建或追加到现有文件
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            // 添加列名行
            bool isFileEmpty = new FileInfo(filePath).Length == 0;

            if (isFileEmpty)
            {
                // 列名为空，添加列名行
                writer.WriteLine("index,PaddleVelocityX,PaddleVelocityY,PaddleVelocityZ,PaddlePositionX,PaddlePositionY,PaddlePositionZ,ParentRotationX,ParentRotationY,ParentRotationZ,ParentRotationW");
            }

            string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                id,
                paddleVelocity.x, paddleVelocity.y, paddleVelocity.z,
                paddlePosition.x, paddlePosition.y, paddlePosition.z,
                parentRotation.x, parentRotation.y, parentRotation.z, parentRotation.w);

            writer.WriteLine(line);

            writer.Close();
        }
    }

    public void BallLandingSingle(int id, Vector3 ballPosition, int blockNumX, int blockNumZ)
    {
        // paddleDataList = 
        string filePath = "C:/Users/rose/Desktop/论文/ballLandingSingle.csv";

        // 创建或追加到现有文件
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            // 添加列名行
            bool isFileEmpty = new FileInfo(filePath).Length == 0;

            if (isFileEmpty)
            {
                // 列名为空，添加列名行
                writer.WriteLine("index,BallPositionX,BallPositionY,BallPositionZ,BlockNumberX,BlockNumberZ");
            }

            // 添加数据行
            string line = string.Format("{0},{1},{2},{3},{4},{5}",
                id,
                ballPosition.x, ballPosition.y, ballPosition.z,
                blockNumX, blockNumZ);

            writer.WriteLine(line);

            writer.Close();
        }
    }

    public void BallTrajecSingle(int id, Vector3 ballPosition, Vector3 ballVelocity)
    {
        // paddleDataList = 
        string filePath = "C:/Users/rose/Desktop/论文/ballTrajecSingle.csv";

        // 创建或追加到现有文件
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            // 添加列名行
            bool isFileEmpty = new FileInfo(filePath).Length == 0;

            if (isFileEmpty)
            {
                // 列名为空，添加列名行
                writer.WriteLine("index,BallPositionX,BallPositionY,BallPositionZ,BallVelocityX,BallVelocityY,BallVelocityZ");
            }

            // 添加数据行
            string line = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                id,
                ballPosition.x, ballPosition.y, ballPosition.z,
                ballVelocity.x, ballVelocity.y, ballVelocity.z);

            writer.WriteLine(line);

            writer.Close();
        }
    }



    public void CheckLength()
    {
        Debug.Log(paddleDataList.Count);
    }
}
