using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager instance;

    // ��� ������ ����ϴ� AudioSource
    public AudioSource backGround;

    // ȭ�� �Ҹ��� ����ϴ� AudioSource���� ����Ʈ
    public List<AudioSource> arrowSounds;

    // ��� ���� ������ �����ϴ� �����̴�
    public Slider backGroundControl;

    // ȿ���� ������ �����ϴ� �����̴�
    public Slider effectControl;

    // �÷��̾��� ���¸� ǥ���ϴ� UI ������Ʈ
    public GameObject stat;

    private void Awake()
    {
        // �̱��� ���� ����: �ν��Ͻ��� �̹� �������� �ʴ� ��� ���� �ν��Ͻ��� �Ҵ�
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // ��� ������ �ʱ� ������ �����̴��� ������ ����
        backGroundControl.value = backGround.volume;

        // ��� ȭ�� �Ҹ��� �ʱ� ������ �����̴��� ������ ����
        foreach (var arrowSound in arrowSounds)
        {
            effectControl.value = arrowSound.volume;
        }
    }

   

    // ��� ����,ȿ���� ���Ұ�
    public void Mute()
    {
        backGround.mute = !backGround.mute;
        
        // ȭ�� �Ҹ� ���Ұ� ������ ��� ���ǰ� �����ϰ�
        foreach (var arrowSound in arrowSounds)
        {
            arrowSound.mute = backGround.mute;
        }

    }

    public void Update()
    {
        // ȿ���� �����̴��� ������ ��� ȭ�� �Ҹ��� ������ ����
        foreach (var arrowSound in arrowSounds)
        {
            arrowSound.volume = effectControl.value;
        }
    }

    // ��� ������ ������ �����̴��� ������ ������Ʈ�ϴ� �Լ�
    public void UpdateBackgroundVolume()
    {
        backGround.volume = backGroundControl.value;
    }

    // �÷��̾��� ���� â�� ���ų� �ݴ� �Լ�
    public void OpenPlayerStat()
    {
        if (stat.activeSelf)
        {
            stat.SetActive(false); // ���� Ȱ��ȭ ���¸� ��Ȱ��ȭ
        }
        else
        {
            stat.SetActive(true); // ���� ��Ȱ��ȭ ���¸� Ȱ��ȭ
        }
    }
}