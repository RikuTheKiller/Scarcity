using UnityEngine;
using System.Collections.Generic;

namespace Scarcity
{
    public class TowerPlacementMenu : MonoBehaviour
    {
        public Transform buttonContainer;
        public TowerPlacementButton buttonPrefab;

        [Space]

        public TowerAsset[] assets;

        private void Awake()
        {
            if (!buttonContainer || !buttonPrefab)
            {
                Debug.LogError("Tower placement menu failed to initialize due to missing fields.");
                return;
            }

            foreach (var asset in assets)
            {
                var button = Instantiate(buttonPrefab, buttonContainer);
                button.SetTowerAsset(asset);
            }
        }
    }
}
