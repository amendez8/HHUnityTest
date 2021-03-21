using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Projectile : MonoBehaviour
{
    protected Light lightSource;

    public abstract void Attack();
    public  void Delete()
    {
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider col)
    {
        Enemy enemyNPC = col.gameObject.GetComponent<Enemy>();

        // Checking if the projectile is "loot" and hitting an enemy.
        if (enemyNPC && !this.transform.root.CompareTag("Loot"))
        {
            Delete();
        }
    }

    public void RemoveLight()
    {
        lightSource.enabled = false;
    }

}
