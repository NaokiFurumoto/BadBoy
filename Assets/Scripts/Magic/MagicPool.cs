using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//魔法Poolクラス。削除せずに生成したものを使いまわす。
//弾の生成。リストで管理。余った弾があれば使いまわす。
public class MagicPool : MonoBehaviour
{
    //変数：生成する魔法のprefabを格納する配列
    //生成した魔法を管理するリスト3つ
    //スポーン判定用
    public static MagicPool instance;

    [SerializeField]
    private GameObject[] magics;

    private List<Magic> fireMagic = new List<Magic>();
    private List<Magic> iceMagic = new List<Magic>();
    private List<Magic> thunderMagic = new List<Magic>();

    //発生判定
    private bool magicSpawned;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

   /// <summary>
   /// 射撃を検知したときに呼ばれる
   /// </summary>
   /// <param name="magicIndex">魔法の識別</param>
   /// <param name="spawnPos">発生位置</param>
   /// <param name="magicRotation">魔法の回転角度</param>
   /// <param name="magicDirection">魔法の方向</param>
    public void Fire(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirection)
    {
        //生成完了していない
        magicSpawned = false;

        TakeMagicPool(magicIndex, spawnPos, magicRotation, magicDirection);
    }

    private void TakeMagicPool(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirection)
    {
        if (magicIndex == 0)//fire
        {
            for (int i = 0; i < fireMagic.Count; i++)
            {
                //非アクティブのオブジェクトがある場合
                if (!fireMagic[i].gameObject.activeInHierarchy)
                {
                    fireMagic[i].gameObject.SetActive(true);
                    fireMagic[i].gameObject.transform.position = spawnPos;
                    fireMagic[i].gameObject.transform.rotation = magicRotation;
                    fireMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;//for文から抜ける
                }
            }
        }

        if (magicIndex == 1)//Ice
        {
            for (int i = 0; i < iceMagic.Count; i++)
            {
                //非アクティブのオブジェクトがある場合
                if (!iceMagic[i].gameObject.activeInHierarchy)
                {
                    iceMagic[i].gameObject.SetActive(true);
                    iceMagic[i].gameObject.transform.position = spawnPos;
                    iceMagic[i].gameObject.transform.rotation = magicRotation;
                    iceMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;//for文から抜ける
                }
            }
        }

        if (magicIndex == 2)//thunder
        {
            for (int i = 0; i < thunderMagic.Count; i++)
            {
                //非アクティブのオブジェクトがある場合
                if (!thunderMagic[i].gameObject.activeInHierarchy)
                {
                    thunderMagic[i].gameObject.SetActive(true);
                    thunderMagic[i].gameObject.transform.position = spawnPos;
                    thunderMagic[i].gameObject.transform.rotation = magicRotation;
                    thunderMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;//for文から抜ける
                }
            }
        }

        //足りない場合は生成
        if (!magicSpawned)
        {
            //生成
            CreateMagic(magicIndex, spawnPos, magicRotation, magicDirection);
        }
    }


    private void CreateMagic(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirection)
    {
        GameObject newMagic = Instantiate(magics[magicIndex], spawnPos, magicRotation);
        newMagic.transform.SetParent(transform);//hierarchyで見やすくする
        newMagic.GetComponent<Magic>().MoveDirection(magicDirection);

        if (magicIndex == 0)
        {
            fireMagic.Add(newMagic.GetComponent<Magic>());
        }

        if (magicIndex == 1)
        {
            iceMagic.Add(newMagic.GetComponent<Magic>());
        }

        if (magicIndex == 2)
        {
            thunderMagic.Add(newMagic.GetComponent<Magic>());
        }
    }




}
