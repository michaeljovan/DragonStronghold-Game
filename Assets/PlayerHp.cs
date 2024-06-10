using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image playerHpBar;
    public Image playerHpBar2;
    float playerHp;
    float hp2;

    public void Update()
    {
        PlayerBar2(playerHp);
    }
    public void PlayerBar(float hp)
    {
        playerHpBar.fillAmount = hp / 30;
        playerHp = hp;
    }
    public void PlayerBar2(float hp)
    {
        if (hp < hp2)
        {
            hp2 -= 5 * Time.deltaTime;
            playerHpBar2.fillAmount = hp2/30;
        }
    }
}
