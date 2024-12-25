using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public List<Ingredient> ingredients;
    public string cookedItemName;
}

[System.Serializable]
public class Ingredient
{
    public string ingredientName;
    public int quantityRequired;
}
