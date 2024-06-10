using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    public Text livesText;
    void Start()
    {
        
    }

    void Update()
    {
        livesText.text = PlayerHpPoint.startHP.ToString() + " LIVES";
    }
}
