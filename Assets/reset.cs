using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
public class reset : MonoBehaviour
{
    public bool reset_micon = true;
    public int is_reset = 0;

    private SerialPort serialPort;
    private Thread serialThread;
    private bool keepReading;

    //private string portName = "COM4"; // M5StickC Plus2では実際のポートを利用
    //private int baudRate = 115200; // ボーレート
    BluetoothReceiver target_script;

    private string receivedData = ""; // 受信データを格納
    private object dataLock = new object(); // データロックオブジェクト

    void Start()
    {
        if (reset_micon)
        {
            // シリアルポートを開く
            //serialPort = new SerialPort(portName, baudRate);
            //serialPort.ReadTimeout = 500; // タイムアウト設定

            //try
            //{
            //    Debug.Log("Serial port opened");
            //    serialPort.Open();
            //    keepReading = true;  // データの読み取りを開始する
            //    serialThread = new Thread(ReadSerial);  // 新しいスレッドを作成
            //    serialThread.Start();  // スレッドを開始
            //}
            //catch (Exception ex)
            //{
            //    Debug.LogError($"シリアルポートを開けませんでした: {ex.Message}");
            //    return;
            //}
        }
    }

    void Update()
    {
        
        
        if (reset_micon)
        {
            // メインスレッドで画面表示やゲームオブジェクトの更新を行う
            //string dataToProcess = null;

            lock (dataLock) {
                ProcessData("a");
            }
            //{
            //    if (!string.IsNullOrEmpty(receivedData))
            //    {
            //        dataToProcess = receivedData;
            //        receivedData = ""; // 受信データをクリア
            //    }
            //    else
            //    {
            //        Debug.Log("reset cannot received");
            //    }
            //}

                //if (dataToProcess != null)
                //{
                //    // 受け取ったデータの処理
                //    Debug.Log("data original: " + dataToProcess);
                //    ProcessData(dataToProcess);
                //}
                //else
                //{
                //    Debug.Log("Cannot get data");
                //}
        }
        else
        {
            if (Input.GetKey(KeyCode.Return))
            {
                Cursor target_script = GameObject.Find("Cursor").GetComponent<Cursor>();
                target_script.X = 0;
                target_script.Y = 0;

                target_script.transform.position = new Vector3(0, 0, 5f);
            }
        }

    }

    // シリアルデータを受信するスレッドの処理
    //void ReadSerial()
    //{
    //    while (keepReading && serialPort != null && serialPort.IsOpen)
    //    {
    //        try
    //        {
    //            string data = serialPort.ReadLine();

    //            lock (dataLock)
    //            {
    //                receivedData = data; // 受信したデータを格納
    //            }
    //        }
    //        catch (TimeoutException)
    //        {
    //            Debug.Log("timeout");
    //            // タイムアウト処理（必要に応じて）
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.Log($"データ読み込みエラー: {ex.Message}");
    //        }
    //    }
    //}

    void ProcessData(string data)
    {
        BluetoothReceiver reset_script = GameObject.Find("Reciever").GetComponent<BluetoothReceiver>();
        is_reset = reset_script.is_reset;

        if (is_reset == 1)
        {
            Cursor target_script = GameObject.Find("Cursor").GetComponent<Cursor>();
            target_script.X = 0;
            target_script.Y = 0;

            target_script.transform.position = new Vector3(0, 0, 5f);
        }

    }

    void OnApplicationQuit()
    {
        // スレッドを停止し、シリアルポートを閉じる
        keepReading = false;

        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join(); // スレッドが終了するまで待機
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close(); // シリアルポートを閉じる
        }
    }
}
