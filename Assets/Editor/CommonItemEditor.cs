using UnityEngine;
using UnityEditor;
using System.IO;

public class CommonItemEditor : EditorWindow
{
    private string itemName = "NewCommonItem";
    private Sprite itemImageSprite;

    [MenuItem("Assets/Create/Create Window CommonItem")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<CommonItemEditor>("CommonItemEditor");
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        GUILayout.Label("Create a New Common Item", EditorStyles.boldLabel);
        itemName = EditorGUILayout.TextField("Item Name", itemName);
        itemImageSprite = (Sprite)EditorGUILayout.ObjectField("Item Image", itemImageSprite, typeof(Sprite), false);
        
        if (GUILayout.Button("Create"))
        {
            CreateCommonItem();
        }
    }

    private void CreateCommonItem()
    {
        if (string.IsNullOrEmpty(itemName))
        {
            EditorUtility.DisplayDialog("Error", "Item Name cannot be empty!", "OK");
            return;
        }

        if (itemImageSprite == null)
        {
            EditorUtility.DisplayDialog("Error", "Item Sprite cannot be null!", "OK");
            return;
        }

        // 创建 ScriptableObject
        CommonItem newItem = ScriptableObject.CreateInstance<CommonItem>();
        newItem.itemName = itemName;
        newItem.itemImageSprite = itemImageSprite;

        // 确保路径存在
        string folderPath = "Assets/CommonItems";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            AssetDatabase.Refresh();
        }

        // 设置保存路径
        string assetPath = $"{folderPath}/{itemName}.asset";

        // 检查是否已存在同名资产
        if (File.Exists(assetPath))
        {
            if (!EditorUtility.DisplayDialog("Warning", $"A Common Item with the name '{itemName}' already exists. Overwrite?", "Yes", "No"))
            {
                return;
            }
        }

        // 保存资产
        AssetDatabase.CreateAsset(newItem, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Success", "Common Item created successfully!", "OK");
    }
}
