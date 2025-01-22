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

        public SpriteRenderer towerPreviewPrefab;
        public Image iconImage;

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
            StartCoroutine(DragTower());
        }

        private IEnumerator DragTower()
        {
            var camera = Camera.main;

            var towerPreview = Instantiate(towerPreviewPrefab);
            towerPreview.sprite = iconImage.sprite;

            while (Input.Attack)
            {
                towerPreview.transform.position = GetCursorGridPoint(camera);
                yield return null;
            }

            Destroy(towerPreview);

            var tower = Instantiate(towerAsset.towerPrefab);
            tower.transform.position = GetCursorGridPoint(camera);
        }

        private Vector3 GetCursorGridPoint(Camera camera)
        {
            return MainGrid.RoundToCell(camera.ScreenToWorldPoint(Input.Point.Value));
        }
    }
}
