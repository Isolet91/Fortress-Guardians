using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleLvlUp : MonoBehaviour
{

    // 성 레벨 업 오브젝트
    public GameObject CastleUp;
    // 업그레이드 비용
    public int Price = 500;

    // 레벨 업 메서드
    public void LevelUp()
    {
        // Main Camera 게임 오브젝트를 찾음
        GameObject res = GameObject.Find("Main Camera");

        // ResourcesCastle 스크립트를 가져와서 현재 코인과 비교
        if (res.GetComponent<ResourcesCastle>().Coins >= Price)
        {
            // 코인이 충분하면 비용을 차감하고 성 레벨 업 오브젝트 활성화
            res.GetComponent<ResourcesCastle>().Coins -= Price;
            CastleUp.SetActive(true);
            // 성 업그레이드 버튼 파괴
            Destroy(gameObject);
        }
    }
}