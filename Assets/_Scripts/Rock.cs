using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rock : Projectile
{
    private Rock newRock;
    public int attackForce;
    public int damage;

    private void Start()
    {
        lightSource = GetComponent<Light>();
    }
    private void Update()
    {
        // if rock attached to the Player, remove the light with RemoveLight from parent class
        if (transform.root.CompareTag("MainCamera"))
            RemoveLight();
    }

    public override void Attack()
    {
        // create new rock to be thrown instead
        newRock = Instantiate(this, this.transform.position, Quaternion.identity);
        newRock.GetComponent<Rigidbody>().isKinematic = false;
        newRock.GetComponent<Rigidbody>().useGravity = true;
        newRock.GetComponent<Rigidbody>().AddForce(this.transform.parent.forward * attackForce, ForceMode.Impulse);
        UiManager.instance.DecreaseRockAmmo();
    }
}
