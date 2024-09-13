using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierUnitDetection : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyUnitsNear = new List<GameObject>();
    [SerializeField] private int EnemyUnitNearCount = 0;
    [SerializeField] private bool IsInsideEnemyDetectionState = false;

    private void OnTriggerEnter(Collider other)
    {
        if (EnemyUnitNearCount > 5 || !other.CompareTag("SoldierUnit"))
        {
            return;
        }
        CSoldierUnitBase this_unit_base = gameObject.transform.parent.GetComponent<CSoldierUnitBase>();
        CSoldierUnitBase unit_base = other.gameObject.GetComponent<CSoldierUnitBase>();
        if (unit_base.GetOwnedByColor() != this_unit_base.GetOwnedByColor())
        {
            EnemyUnitsNear.Add(other.gameObject);
            EnemyUnitNearCount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("SoldierUnit"))
        {
            return;
        }
        CSoldierUnitBase this_unit_base = gameObject.transform.parent.GetComponent<CSoldierUnitBase>();
        CSoldierUnitBase unit_base = other.gameObject.GetComponent<CSoldierUnitBase>();
        if (unit_base.GetOwnedByColor() != this_unit_base.GetOwnedByColor())
        {
            EnemyUnitsNear.Remove(other.gameObject);
            EnemyUnitNearCount--;
        }
    }
    public GameObject GetNearestEnemy()
    {
        GameObject closest_enemy = null;
        float min_dist = 99999;
        foreach (GameObject enemy_unit in EnemyUnitsNear)
        {
            if (enemy_unit == null)
            {
                EnemyUnitNearCount--;
            }
            else
            {
                float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(enemy_unit.transform.position.x, enemy_unit.transform.position.y));
                if (dist < min_dist)
                {
                    min_dist = dist;
                    closest_enemy = enemy_unit;
                }
            }
        }
        for (int i = EnemyUnitsNear.Count - 1; i >= 0; i--)
        {
            if (EnemyUnitsNear[i] == null)
            {
                EnemyUnitsNear.Remove(EnemyUnitsNear[i]);
            }
        }
        EnemyUnitNearCount = EnemyUnitsNear.Count;
        return closest_enemy;
    }
  
}
