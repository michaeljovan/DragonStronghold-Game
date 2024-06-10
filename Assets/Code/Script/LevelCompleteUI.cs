using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    public void OnNextLevelButtonPressed()
    {
        LevelManager.main.OnNextLevelButtonPressed();
    }
}
