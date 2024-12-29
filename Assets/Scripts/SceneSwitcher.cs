using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnBattleWithEnermy += SwitchToBattleScene;
        EventHandler.OnEnermyDead += SwitchToMainScene;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnBattleWithEnermy -= SwitchToBattleScene;
        EventHandler.OnEnermyDead -= SwitchToMainScene;
    }

    private void SwitchToMainScene()
    {
        Debug.Log("SwitchToMainScene");
        SwitchScene("MainScene");
    }

    private void SwitchToBattleScene()
    {
        SwitchScene("NewBattleScene");
    }
    public void SwitchScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
