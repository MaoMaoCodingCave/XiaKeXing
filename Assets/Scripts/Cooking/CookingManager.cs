using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class CookingManager : MonoBehaviour
{
    public GameObject cookingPage;
    public InventoryManager inventoryManager;
    public Recipe recipe;
    public List<Recipe> recipeList;
    public GameObject recipeBtn;
    public Canvas recipeCanvas;
    public Image recipeImage;
    public Text recipeNameText;
    public List<Text> ingredientsTexts;
    public List<Text> ingredientsQuantityTexts;
    public List<Image> ingredientsImages;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        cookingPage.SetActive(false);
        recipeNameText.text = recipe.recipeName;
        UpdateRecipe();
        for (int i = 0; i < recipeList.Count; i++)
        {
            GameObject recipeButton = Instantiate(recipeBtn, recipeCanvas.transform);
            recipeButton.GetComponentInChildren<Text>().text = recipeList[i].recipeName;
            Recipe recipe = recipeList[i];
            recipeButton.GetComponent<Button>().onClick.AddListener(delegate { OnRecipeButtonClicked(recipe); });
        }
    }

    private void OnRecipeButtonClicked(Recipe recipe)
    {
        this.recipe = recipe;
        recipeNameText.text = recipe.recipeName;
        recipeImage.sprite = recipe.recipeImageSprite;
        UpdateRecipe();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // 按下 C 键打开或关闭烹饪页面
        // 新的逻辑放到了 Pot.cs 中
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     cookingPage.SetActive(!cookingPage.activeSelf);
        // }
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     cookingPage.SetActive(false);
        // }
    }

    /// <summary>
    /// 点击烹饪按钮时调用
    /// </summary>
    public void onCookButtonClicked()
    {
        // 检查食材是否足够
        bool canCook = true;
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            if (!inventoryManager.inventory.ContainsKey(recipe.ingredients[i].ingredientName) || inventoryManager.inventory[recipe.ingredients[i].ingredientName] < recipe.ingredients[i].quantityRequired)
            {
                canCook = false;
                break;
            }
        }
        // 如果食材足够，烹饪成功
        if (canCook)
        {
            for (int i = 0; i < recipe.ingredients.Count; i++)
            {
                inventoryManager.inventory[recipe.ingredients[i].ingredientName] -= recipe.ingredients[i].quantityRequired;
            }
            if (inventoryManager.inventory.ContainsKey(recipe.cookedItemName))
            {
                inventoryManager.inventory[recipe.cookedItemName]++;
            }
            else
            {
                inventoryManager.inventory[recipe.cookedItemName] = 1;
            }
            UpdateRecipe();
            // TODO: 烹饪成功后背包逻辑的处理
            Debug.Log("烹饪" + recipe.cookedItemName + "成功");
        }
        else
        {
            Debug.Log("食材不足，无法烹饪");
        }
    }

    /// <summary>
    /// 点击退出按钮时调用
    /// </summary>
    public void OnCancelButtonClicked()
    {
        cookingPage.SetActive(false);
    }

    /// <summary>
    /// 在游戏开始根据菜谱更新食材文本和数量文本
    /// </summary>
    private void UpdateRecipe()
    {
        // 让所有的食材文本和数量文本都不显示
        for (int i = 0; i < ingredientsTexts.Count; i++)
        {
            ingredientsTexts[i].enabled = false;
            ingredientsQuantityTexts[i].enabled = false;
            ingredientsImages[i].enabled = false;
        }
        // 只显示所需的食材文本和数量文本
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            ingredientsTexts[i].enabled = true;
            ingredientsQuantityTexts[i].enabled = true;
            ingredientsImages[i].enabled = true;
        }
        // 更新食谱的食材文本和数量文本
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            // ingredientsTexts[i].text = recipe.ingredients[i].ingredientName;
            // ingredientsQuantityTexts[i].text = recipe.ingredients[i].quantityRequired.ToString();
            if (inventoryManager.inventory.ContainsKey(recipe.ingredients[i].ingredientName))
            {
                ingredientsTexts[i].text = recipe.ingredients[i].ingredientName;
                ingredientsImages[i].sprite = recipe.ingredients[i].ingredientImageSprite;
                ingredientsQuantityTexts[i].text = inventoryManager.inventory[recipe.ingredients[i].ingredientName].ToString() + '/' + recipe.ingredients[i].quantityRequired.ToString();
                // 如果食材数量足够，将数量文本颜色设置为绿色，否则设置为红色
                if (inventoryManager.inventory[recipe.ingredients[i].ingredientName] >= recipe.ingredients[i].quantityRequired)
                {
                    ingredientsQuantityTexts[i].color = Color.green;
                }
                else
                {
                    ingredientsQuantityTexts[i].color = Color.red;
                }
            }
            else
            {
                ingredientsTexts[i].text = recipe.ingredients[i].ingredientName;
                ingredientsQuantityTexts[i].text = "0/" + recipe.ingredients[i].quantityRequired.ToString();
                ingredientsQuantityTexts[i].color = Color.red;
            }
        }
    }
}
