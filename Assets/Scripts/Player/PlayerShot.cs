using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの射撃　キー入力検知、射撃感覚設定
/// </summary>
public class PlayerShot : MonoBehaviour
{
    //変数：射撃タイマー　次の射撃までの時間
    //発射ポジ、魔法陣管理スクリプト
    [SerializeField]
    private float shootTimer, shootTimerDelay = 0.2f;

    [SerializeField]
    private Transform magicSpawnPos;

    private int dell = 1;

    private PlayerMagicSquareManager playerMagicSquareManager;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        playerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }

    private void Update()
    {
        Shooting();
    }

    //関数：発射
    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //プレイ時間超えたら打てるように
            if(Time.time > shootTimer)
            {
                //撃つ時間の更新
                shootTimer = Time.time + shootTimerDelay;

                playerMagicSquareManager.Shoot(magicSpawnPos.position);
            }

        }
    }
}
