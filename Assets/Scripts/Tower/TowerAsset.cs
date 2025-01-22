using UnityEngine;

namespace Scarcity
{
    [CreateAssetMenu(fileName = "Tower Asset", menuName = "Tower Asset")]
    public class TowerAsset : ScriptableObject
    {
        public Tower towerPrefab;

        public float GetDPS()
        {
            var towerGun = towerPrefab.GetComponent<TowerGun>();
            var gun = towerGun ? towerGun.gun : null;

            return gun ? Mathf.Round(gun.projectilePrefab.damage / gun.firingCooldown * 10) / 10 : 0;
        }

        public float GetRange()
        {
            var towerGun = towerPrefab.GetComponent<TowerGun>();

            return towerGun ? towerGun.range : 0;
        }

        public float GetCost()
        {
            return towerPrefab.cost;
        }
    }
}