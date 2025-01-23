using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    public class TowerPlacementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public TowerAsset towerAsset;

        public SpriteRenderer towerPreviewPrefab;
        public Image iconImage;

        public TextMeshProUGUI dpsValue;
        public TextMeshProUGUI rangeValue;
        public TextMeshProUGUI costValue;

        private bool dragging = false;

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
            dragging = true;
            StartCoroutine(DragTower());
            Input.Attack.Action.Disable();
        }

        public void OnPointerUp(PointerEventData data)
        {
            dragging = false;
            Input.Attack.Action.Enable();
        }

        private IEnumerator DragTower()
        {
            var camera = Camera.main;

            var towerPreview = Instantiate(towerPreviewPrefab);
            towerPreview.sprite = iconImage.sprite;

            Vector2 point;
            Vector3Int tilePosition;

            while (dragging)
            {
                point = GetCursorPoint(camera);
                tilePosition = MainGrid.GetNearestTile(point);

                UpdatePreview(towerPreview, IsValidPosition(tilePosition));

                towerPreview.transform.position = MainGrid.RoundToCenter(point);

                yield return null;
            }

            Destroy(towerPreview);

            point = GetCursorPoint(camera);
            tilePosition = MainGrid.GetNearestTile(point);

            if (!IsValidPosition(tilePosition)) yield break;

            var tower = Instantiate(towerAsset.towerPrefab);
            tower.transform.position = MainGrid.RoundToCenter(point);

            var tileFlags = MainGrid.Tilemap.GetTileFlags(tilePosition);
            MainGrid.Tilemap.SetTileFlags(tilePosition, tileFlags | (TileFlags)BasicTile.ExtraFlags.Occupied);
        }

        private Vector2 GetCursorPoint(Camera camera)
        {
            return camera.ScreenToWorldPoint(Input.Point.Value);
        }

        private bool IsValidPosition(Vector3Int position)
        {
            var tile = MainGrid.Tilemap.GetTile(position);
            var tileFlags = MainGrid.Tilemap.GetTileFlags(position);

            if (tile is not BasicTile basicTile)
                return false;
            if (basicTile.colliderType != Tile.ColliderType.None)
                return false;
            if ((tileFlags & (TileFlags)BasicTile.ExtraFlags.Occupied) != 0)
                return false;

            return true;
        }

        private void UpdatePreview(SpriteRenderer preview, bool valid)
        {
            preview.color = valid ? new(0, 1, 0, 0.6f) : new(1, 0, 0, 0.6f);
        }
    }
}
