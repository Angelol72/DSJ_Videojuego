using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public enum UnitState { Idle, Moving, Attacking, Dead }
    public UnitState state = UnitState.Idle;
    public string unitName; // Name of the unit
    public int attackPower; // Attack power of the unit
    public abstract void Die();
    public abstract void TakeDamage(int damage);
    public virtual bool isDead()
    {
        return state == UnitState.Dead;
    }
}