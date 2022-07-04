using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���@�w�̊Ǘ�
/// ���@�w�̐؂�ւ�
/// </summary>
public class MagicSquareManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] magicSquares;

    //�\�����̖��@�w
    private int currentMagicSquare;

    //�֐��̍쐬�F���ׂĂ̖��@�w���\���A����̖��@�w������\��
    private void Start()
    {
        DeActivateAllMagicSquare();
    }

    /// <summary>
    /// ���ׂĂ̖��@�w���\��
    /// �����͂��ƁB���ʏ�������
    /// </summary>
    private void DeActivateAllMagicSquare()
    {
        for (int i = 0; i < magicSquares.Length; i++)
        {
            magicSquares[i].SetActive(false);
        }
    }

    /// <summary>
    /// ����̖��@�w������\��
    /// </summary>
    /// <param name="newMagicSquare"></param>
    public void ActivateMagicSquare(int newMagicSquare)
    {
        //�\�����̖��@�w���\��
        magicSquares[currentMagicSquare].SetActive(false);
        //�l�̍X�V
        currentMagicSquare = newMagicSquare;
        //�����炵�����@�w�̕\��
        magicSquares[newMagicSquare].SetActive(true);
    }

}
