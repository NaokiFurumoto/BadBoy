using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラをランダムに揺らす
/// </summary>
public class CameraShake : MonoBehaviour
{
    //揺れる強さ
    [SerializeField]
    private float shakeAmount = 0.007f;

    //カメラポジ
    private Vector3 camPos;

    private int test = 1;

    //格納：実際に揺らす数値
    private float cameraShakingOffset_X, cameraShakingOffset_Y;

    /// <summary>
    /// 揺らすランダムな数値設定
    /// </summary>
    private void StartCameraShaking()
    {
        //揺れる数値を取得
        if (shakeAmount > 0)
        {
            camPos = transform.position;
            cameraShakingOffset_X = (Random.value * shakeAmount * 2) - shakeAmount;
            cameraShakingOffset_Y = (Random.value * shakeAmount * 2) - shakeAmount;

            camPos.x += cameraShakingOffset_X;
            camPos.y += cameraShakingOffset_Y;

            transform.position = camPos;
        }
    }

    /// <summary>
    /// 指定時間カメラを揺らす
    /// </summary>
    /// <param name="shakeTime"></param>
    public void ShakeCamera(float shakeTime)
    {
        //指定の関数を連続で呼ぶ：最初の呼び出し時間 / 呼び出し間隔
        InvokeRepeating("StartCameraShaking", 0f, 0.01f);

        Invoke("StopCameraShaking", shakeTime);
    }

    /// <summary>
    /// カメラを止める
    /// </summary>
    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");

        //カメラ位置の初期化
        transform.localPosition = Vector3.zero;
    }

}
