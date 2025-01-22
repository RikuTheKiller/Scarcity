using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scarcity
{
    public class TowerPlacementButton : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset towerAsset;

        public TextMeshProUGUI dpsValue;
        public TextMeshProUGUI rangeValue;
        public TextMeshProUGUI costValue;

        private void Start()
        {
            SetTowerAsset(towerAsset);
        }

        public void SetTowerAsset(TowerAsset towerAsset)
        {
            this.towerAsset = towerAsset;

            if (!towerAsset) return;

            dpsValue.text = towerAsset.GetDPS().ToString();
            rangeValue.text = towerAsset.GetRange().ToString();
            costValue.text = "$" + towerAsset.GetCost().ToString();
        }

        public void OnPointerDown(PointerEventData data)
        {
            var towerInstance = Instantiate(towerAsset.towerPrefab);
            StartCoroutine(DragTower(towerInstance));
        }

        private IEnumerator DragTower(Tower tower)
        {
            var camera = Camera.main;

            while (Input.Attack)
            {
                var worldPoint = camera.ScreenToWorldPoint(Input.Point.Value);
                tower.transform.position = new(Mathf.Round(worldPoint.x - 0.5f) + 0.5f, Mathf.Round(worldPoint.y - 0.5f) + 0.5f, 0);
                yield return null;
            }
        }
    }
}
