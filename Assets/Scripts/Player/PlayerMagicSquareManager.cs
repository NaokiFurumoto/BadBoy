using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装備中の魔法陣の管理
/// </summary>
public class PlayerMagicSquareManager : MonoBehaviour
{
    [SerializeField]
    private MagicSquareManager[] playerMagicSquares;

    private int magicSquareIndex;

    //魔法の球を格納する配列
    [SerializeField]
    private GameObject[] magics;

    //撃つべき場所/方向/発射位置
    private Vector2 targetPos, direction, magicSpawnPos;

    //カメラ
    private Camera cam;

    //魔法の回転
    private Quaternion magicRotation;

    private CameraShake cameraShake;

    //カメラを揺らす時間
    [SerializeField]
    private float cameraShakeTimer = 0.2f;

    private void Awake()
    {
        //最初は装備していない
        magicSquareIndex = 0;
        playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);
        cam = Camera.main;
        cameraShake = cam.GetComponent<CameraShake>();
    }

    private void Update()
    {
        ChangeMagic();
    }

    /// <summary>
    /// 魔法陣の切り替え
    /// </summary>
    private void ChangeMagic()
    {
        //マウスの回転を検知する
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //一旦消す
            playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);
            magicSquareIndex++;

            if (magicSquareIndex >= playerMagicSquares.Length)
            {
                magicSquareIndex = 0;
            }

            playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);

        } //逆に回す
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);
            magicSquareIndex--;

            if (magicSquareIndex < 0)
            {
                magicSquareIndex = playerMagicSquares.Length - 1;
            }

            playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);
        }

        for (int i = 0; i < playerMagicSquares.Length; i++)
        {
            if(Input.GetKeyDown((i + 1).ToString()))
            {
                playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);
                magicSquareIndex=i;
                playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);

                break;//for文を抜ける
            }
        }
    }

    /// <summary>
    /// 適切な魔法陣を有効に
    /// </summary>
    /// <param name="dirIndex"></param>
    public void Activate(int dirIndex)
    {
        playerMagicSquares[magicSquareIndex].ActivateMagicSquare(dirIndex);
    }

    /// <summary>
    /// 弾を生成して打ち出す
    /// </summary>
    /// <param name="spawnPos">発射位置</param>
    public void Shoot(Vector2 spawnPos)
    {
        targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
        magicSpawnPos = spawnPos;
        direction = (targetPos - magicSpawnPos).normalized;

        //角度指定：度数に変換
        magicRotation = Quaternion.Euler(0, 0,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        ////一旦poolを無視して生成
        //GameObject newMagic = Instantiate(magics[magicSquareIndex], spawnPos, magicRotation);

        ////弾を打ち出す
        //newMagic.GetComponent<Magic>().MoveDirection(direction);

        //生成したものをPool
        MagicPool.instance.Fire(magicSquareIndex, spawnPos, magicRotation, direction);

        cameraShake.ShakeCamera(cameraShakeTimer);
    }
}
