using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法陣の管理
/// 魔法陣の切り替え
/// </summary>
public class MagicSquareManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] magicSquares;

    //表示中の魔法陣
    private int currentMagicSquare;

    //関数の作成：すべての魔法陣を非表示、特定の魔法陣だけを表示
    private void Start()
    {
        DeActivateAllMagicSquare();
    }

    /// <summary>
    /// すべての魔法陣を非表示
    /// 条件はあと。結果処理から
    /// </summary>
    private void DeActivateAllMagicSquare()
    {
        for (int i = 0; i < magicSquares.Length; i++)
        {
            magicSquares[i].SetActive(false);
        }
    }

    /// <summary>
    /// 特定の魔法陣だけを表示
    /// </summary>
    /// <param name="newMagicSquare"></param>
    public void ActivateMagicSquare(int newMagicSquare)
    {
        //表示中の魔法陣を非表示
        magicSquares[currentMagicSquare].SetActive(false);
        //値の更新
        currentMagicSquare = newMagicSquare;
        //あたらしい魔法陣の表示
        magicSquares[newMagicSquare].SetActive(true);
    }

}
