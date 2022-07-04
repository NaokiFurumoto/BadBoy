using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������̖��@�w�̊Ǘ�
/// </summary>
public class PlayerMagicSquareManager : MonoBehaviour
{
    [SerializeField]
    private MagicSquareManager[] playerMagicSquares;

    private int magicSquareIndex;

    //���@�̋����i�[����z��
    [SerializeField]
    private GameObject[] magics;

    //���ׂ��ꏊ/����/���ˈʒu
    private Vector2 targetPos, direction, magicSpawnPos;

    //�J����
    private Camera cam;

    //���@�̉�]
    private Quaternion magicRotation;

    private CameraShake cameraShake;

    //�J������h�炷����
    [SerializeField]
    private float cameraShakeTimer = 0.2f;

    private void Awake()
    {
        //�ŏ��͑������Ă��Ȃ�
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
    /// ���@�w�̐؂�ւ�
    /// </summary>
    private void ChangeMagic()
    {
        //�}�E�X�̉�]�����m����
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //��U����
            playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);
            magicSquareIndex++;

            if (magicSquareIndex >= playerMagicSquares.Length)
            {
                magicSquareIndex = 0;
            }

            playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);

        } //�t�ɉ�
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

                break;//for���𔲂���
            }
        }
    }

    /// <summary>
    /// �K�؂Ȗ��@�w��L����
    /// </summary>
    /// <param name="dirIndex"></param>
    public void Activate(int dirIndex)
    {
        playerMagicSquares[magicSquareIndex].ActivateMagicSquare(dirIndex);
    }

    /// <summary>
    /// �e�𐶐����đł��o��
    /// </summary>
    /// <param name="spawnPos">���ˈʒu</param>
    public void Shoot(Vector2 spawnPos)
    {
        targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
        magicSpawnPos = spawnPos;
        direction = (targetPos - magicSpawnPos).normalized;

        //�p�x�w��F�x���ɕϊ�
        magicRotation = Quaternion.Euler(0, 0,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        ////��Upool�𖳎����Đ���
        //GameObject newMagic = Instantiate(magics[magicSquareIndex], spawnPos, magicRotation);

        ////�e��ł��o��
        //newMagic.GetComponent<Magic>().MoveDirection(direction);

        //�����������̂�Pool
        MagicPool.instance.Fire(magicSquareIndex, spawnPos, magicRotation, direction);

        cameraShake.ShakeCamera(cameraShakeTimer);
    }
}
