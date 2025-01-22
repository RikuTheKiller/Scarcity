using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scarcity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TowerPlacementMenu))]
    public class TowerPlacementMenuEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var towerPlacementMenu = (TowerPlacementMenu)target;

            if (GUILayout.Button("Refresh Asset Cache"))
            {
                var assetGUIDs = AssetDatabase.FindAssets("t:TowerAsset", new[] {"Assets/Tower Assets"});
                List<TowerAsset> assets = new();

                foreach (var assetGUID in assetGUIDs)
                {
                    assets.Add(AssetDatabase.LoadAssetAtPath<TowerAsset>(AssetDatabase.GUIDToAssetPath(assetGUID)));
                }

                towerPlacementMenu.assets = assets.ToArray();

                PrefabUtility.ApplyPrefabInstance(towerPlacementMenu.gameObject, InteractionMode.UserAction);
            }
        }
    }
}
