using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

// �÷��̾��� ���� ����ü
[Serializable]
public struct PlayerStat
{
    public float attack;         //���ݷ�
    public float speed;          //����
    public float attackGold;     //���ݷ� ���׷��̵� ���
    public float speedGold;      //���� ���׷��̵� ���
}

public class StatUpgrade : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static StatUpgrade instance;

    [SerializeField]
    private GameObject statPanel;
    [SerializeField]
    private TextMeshProUGUI attackText;
    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    private TextMeshProUGUI attackGoldText;
    [SerializeField]
    private TextMeshProUGUI speedGoldText;

    // ���� ���õǾ� �ִ� ĳ������ Bow ��ũ��Ʈ
    private Bow currentBow;

    private void Awake()
    {
        // �̹� �ν��Ͻ��� �����ϴ��� Ȯ���ϰ� ������ ���� �ν��Ͻ��� �Ҵ�
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        // ���׷��̵� �г��� Ȱ��ȭ�Ǿ� �ְ� currentBow�� null�� �ƴ� ��� ���� ������ ������Ʈ
        if (statPanel.activeSelf && currentBow != null)
        {
            attackText.text = $"Attack : {currentBow.playerStat.attack}";
            speedText.text = $"Speed : {currentBow.playerStat.speed.ToString("F3")}";
            attackGoldText.text = $"$ :{currentBow.playerStat.attackGold}";
            speedGoldText.text = $"$ :{currentBow.playerStat.speedGold}";
        }
    }

    public void UpgradeAttack()  //���ݷ� ���׷��̵� �޼���
    {
            //������ ����ϴٸ� ���׷��̵�
        if (ResourcesCastle.instance.Coins >= currentBow.playerStat.attackGold)
        {
            // ���� �÷��̾��� ������ ���� ��� ���� & ���� ���� ����
            currentBow.playerStat.attackGold += 10;
            ResourcesCastle.instance.Coins -= currentBow.playerStat.attackGold;

            // ���� �÷��̾��� ������ ����
            currentBow.playerStat.attack++;
        }
    }

    public void UpgradeSpeed () //���� ���׷��̵�
    {
        if (ResourcesCastle.instance.Coins >= currentBow.playerStat.speedGold && currentBow.playerStat.speed > 0.1f)
        {
            // ���� �÷��̾��� ���� ���� ��� ���� & ���� ���� ����
            currentBow.playerStat.speedGold += 10;
            ResourcesCastle.instance.Coins -= currentBow.playerStat.speedGold;

            // ���� �÷��̾��� ���� ����
            currentBow.playerStat.speed -= 0.05f;
        }
    }

    //���׷��̵� �г� Ȱ��ȭ �޼���
    public void ActiveUpgradePanel(Bow bow) 
    {
        statPanel.SetActive(true);          //���׷��̵� �г��� Ȱ��ȭ�ϰ�
        currentBow = bow;                   //���׷��̵�� ������ �����Ѵ�
        Time.timeScale = 0;                 //���� �Ͻ� ����
    }

    //���� ȭ������ ���ư��� �޼���
    public void Back()
    {
        statPanel.SetActive(false);         //���׷��̵� �г��� ��Ȱ��ȭ�ϰ�
        Time.timeScale = 1;                 //���� �簳
    }
}