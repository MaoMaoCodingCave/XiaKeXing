using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarController : MonoBehaviour
{
    public List<Image> toolImage;
    public List<Sprite> toolSprite;
    public bool ableToDig = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < toolSprite.Count; i++)
        {
            toolImage[i].sprite = toolSprite[i];
        }
        toolImage[0].color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            toolImage[0].color = Color.red;
            ClearOtherColors(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            toolImage[1].color = Color.red;
            ClearOtherColors(1);
            ableToDig = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            toolImage[2].color = Color.red;
            ClearOtherColors(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            toolImage[3].color = Color.red;
            ClearOtherColors(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            toolImage[4].color = Color.red;
            ClearOtherColors(4);
        }
    }

    void ClearOtherColors(int index)
    {
        for (int i = 0; i < toolImage.Count; i++)
        {
            if (i != index)
            {
                toolImage[i].color = Color.white;
            }
        }
        ableToDig = false;
    }
}
