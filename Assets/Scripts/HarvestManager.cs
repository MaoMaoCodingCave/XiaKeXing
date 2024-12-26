using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestManager : MonoBehaviour
{
    public Canvas harvestPage;
    public Canvas harvestCanvas;
    public Canvas existingItemCanvas;
    public List<Herb> herbList;
    public Text herbNameText;
    private bool isHarvesting = false;
    private int collectedCount = 0;
    public Button startButton;
    public Button stopButton;
    public Button cancelButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartHarvesting);
        stopButton.onClick.AddListener(StopHarvesting);
        cancelButton.onClick.AddListener(CancelHarvesting);
        stopButton.interactable = false;
        foreach (Herb herb in herbList)
        {
            GameObject herbText = Instantiate(herbNameText.gameObject, existingItemCanvas.transform);
            herbText.GetComponent<Text>().text = herb.herbName;
            // 把字体颜色改为红色
            herbText.GetComponent<Text>().color = Color.red;
        }
    }

    // 停止采集并且关闭采集页面
    private void CancelHarvesting()
    {
        isHarvesting = false;
        harvestPage.gameObject.SetActive(false);
    }

    void StartHarvesting()
    {
        isHarvesting = true;
        stopButton.interactable = true;
        startButton.interactable = false;
        StartCoroutine(HarvestHerbs());
    }

    IEnumerator HarvestHerbs()
    {
        while (isHarvesting && collectedCount < 10)
        {
            Herb herb = herbList[Random.Range(0, herbList.Count)];
            collectedCount++;
            Debug.Log("已经收集了" + collectedCount + "个草药");
            GameObject herbText = Instantiate(herbNameText.gameObject, harvestCanvas.transform);
            herbText.GetComponent<Text>().text = herb.herbName;
            herbText.GetComponent<Text>().color = Color.red;
            yield return new WaitForSeconds(3f);
        }
        if (collectedCount >= 10)
        {
            isHarvesting = false;
            stopButton.interactable = false;
            startButton.interactable = true;
        }
    }

    void StopHarvesting()
    {
        isHarvesting = false;
        stopButton.interactable = false;
        startButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
