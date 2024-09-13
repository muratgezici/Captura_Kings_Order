using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierUnitCombat : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyUnitsNear = new List<GameObject>();
    [SerializeField] private int EnemyUnitNearCount = 0;
    [SerializeField] protected float UnitHealth = 10f;
    [SerializeField] protected float UnitMaxHealth = 10f;
    [SerializeField] protected float UnitAttackPower = 10f;
    [SerializeField] protected float UnitAttackSpeed = 1.2f;
    [SerializeField] protected GameObject AnimationHandler;
    private float AttackTimer = 0;
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
    private void Update()
    {
        
    }
    public GameObject GetCombatRangeEnemy()
    {
        GameObject closest_enemy = null;
        float min_dist = 9999;
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
        if (EnemyUnitNearCount != 0)
        {
            return closest_enemy;
        }
        return null;
    }
    public bool InitiateCombat(GameObject enemy_unit)
    {
        if(enemy_unit != null)
        {

            InstantlyTurn(enemy_unit.transform.position);
        }
        else
        {
            return false;
        }
        
        AttackTimer += Time.deltaTime;
        float attack_speed_random_number = Random.Range(UnitAttackSpeed-0.2f, UnitAttackSpeed + 0.2f);
        if (AttackTimer > attack_speed_random_number)
        {
            AnimationHandler.GetComponent<CSoldierUnitAnimation>().AnimateAttackAnimation();
            //Debug.Log("Attacker: " + gameObject.name + " Attacked: " + enemy_unit.name);
            AttackTimer = 0;
            enemy_unit.gameObject.GetComponent<CSoldierUnitBase>().DecreaseHealth(UnitAttackPower);
            return true;
        }
        return false;
    }
    public void DecreaseHealth(float value)
    {
        UnitHealth -= value;
        if(UnitHealth <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    public int GetEnemyUnitsNear()
    {
        return EnemyUnitNearCount;
    }
    public void SetAttackTimer(float timer)
    {
        AttackTimer = timer;
    }
    public void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!

        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 200000f);
    }
    //Trigger enter düþman listesine düþman ekler
    //Attack coroutine i düþman listesindeki en yakýn düþmana saldýrýr, eðer en yakýn düþman nullsa (exit yapmadan öldüyse) onu listeden çýkarýr ve en yakýndakine saldýrma
    //aramasýna devam eder
    //eðer saldýracak bir düþman yoksa saldýrý modundan çýkar ve olduðu yerde bekler


}
