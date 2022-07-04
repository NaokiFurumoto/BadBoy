using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���@Pool�N���X�B�폜�����ɐ����������̂��g���܂킷�B
//�e�̐����B���X�g�ŊǗ��B�]�����e������Ύg���܂킷�B
public class MagicPool : MonoBehaviour
{
    //�ϐ��F�������閂�@��prefab���i�[����z��
    //�����������@���Ǘ����郊�X�g3��
    //�X�|�[������p
    public static MagicPool instance;

    [SerializeField]
    private GameObject[] magics;

    private List<Magic> fireMagic = new List<Magic>();
    private List<Magic> iceMagic = new List<Magic>();
    private List<Magic> thunderMagic = new List<Magic>();

    //��������
    private bool magicSpawned;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

   /// <summary>
   /// �ˌ������m�����Ƃ��ɌĂ΂��
   /// </summary>
   /// <param name="magicIndex">���@�̎���</param>
   /// <param name="spawnPos">�����ʒu</param>
   /// <param name="magicRotation">���@�̉�]�p�x</param>
   /// <param name="magicDirection">���@�̕���</param>
    public void Fire(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirection)
    {
        //�����������Ă��Ȃ�
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
                //��A�N�e�B�u�̃I�u�W�F�N�g������ꍇ
                if (!fireMagic[i].gameObject.activeInHierarchy)
                {
                    fireMagic[i].gameObject.SetActive(true);
                    fireMagic[i].gameObject.transform.position = spawnPos;
                    fireMagic[i].gameObject.transform.rotation = magicRotation;
                    fireMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;//for�����甲����
                }
            }
        }

        if (magicIndex == 1)//Ice
        {
            for (int i = 0; i < iceMagic.Count; i++)
            {
                //��A�N�e�B�u�̃I�u�W�F�N�g������ꍇ
                if (!iceMagic[i].gameObject.activeInHierarchy)
                {
                    iceMagic[i].gameObject.SetActive(true);
                    iceMagic[i].gameObject.transform.position = spawnPos;
                    iceMagic[i].gameObject.transform.rotation = magicRotation;
                    iceMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;//for�����甲����
                }
            }
        }

        if (magicIndex == 2)//thunder
        {
            for (int i = 0; i < thunderMagic.Count; i++)
            {
                //��A�N�e�B�u�̃I�u�W�F�N�g������ꍇ
                if (!thunderMagic[i].gameObject.activeInHierarchy)
                {
                    thunderMagic[i].gameObject.SetActive(true);
                    thunderMagic[i].gameObject.transform.position = spawnPos;
                    thunderMagic[i].gameObject.transform.rotation = magicRotation;
                    thunderMagic[i].MoveDirection(magicDirection);

                    magicSpawned = true;
                    break;//for�����甲����
                }
            }
        }

        //����Ȃ��ꍇ�͐���
        if (!magicSpawned)
        {
            //����
            CreateMagic(magicIndex, spawnPos, magicRotation, magicDirection);
        }
    }


    private void CreateMagic(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirection)
    {
        GameObject newMagic = Instantiate(magics[magicIndex], spawnPos, magicRotation);
        newMagic.transform.SetParent(transform);//hierarchy�Ō��₷������
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
