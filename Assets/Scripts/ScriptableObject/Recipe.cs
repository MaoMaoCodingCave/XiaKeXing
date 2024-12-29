using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public Sprite recipeImageSprite;
    public string recipeName;
    public List<Ingredient> ingredients;
    public string cookedItemName;
}

[System.Serializable]
public class Ingredient
{
    public Sprite ingredientImageSprite;
    public string ingredientName;
    public int quantityRequired;
}
