using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningGameManager : MonoBehaviour
{
    public Text scoreText;
    private int totalScore = 0;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnDragItemFinished += OnDragItemFinishedEvent;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnDragItemFinished -= OnDragItemFinishedEvent;
    }

    private void OnDragItemFinishedEvent(int score)
    {
        totalScore += score;
        scoreText.text = "Score: " + totalScore.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
