using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Burst.Intrinsics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public Sprite recipeImageSprite;
    public string recipeName;
    public List<Ingredient> ingredients;
    public CommonItem cookedItem;
}

[System.Serializable]
public class Ingredient
{
    public CommonItem commonItem;
    public int quantityRequired;
}
