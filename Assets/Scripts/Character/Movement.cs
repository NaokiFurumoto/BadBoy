using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ボス以外のすべてのキャラの移動を行う
/// </summary>
public class Movement : MonoBehaviour
{
    //protectedは継承先から扱うことができる、privateだと自身のみ
    [SerializeField]
    protected float xSpeed = 1.5f, ySpeed = 1.5f;

    //移動量
    private Vector2 moveDelta;

    /// <summary>
    /// キャラクターを移動させる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    protected void CharacterMovement(float x, float y)
    {
        moveDelta = new Vector2(x * xSpeed, y * ySpeed);

        //Time.deltaTime:1フレームの更新時間0.02とか
        transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
    }

    //移動量を返す
    public Vector2 GetMoveDelta()
    {
        return moveDelta;
    }

}
