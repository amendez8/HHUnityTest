using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private Arrow newArrow;
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
        // create new arrow to be shot instead
        newArrow = Instantiate(this, this.transform.position, this.transform.rotation);
        newArrow.GetComponent<Rigidbody>().isKinematic = false;
        newArrow.GetComponent<Rigidbody>().useGravity = true;
        newArrow.GetComponent<Rigidbody>().AddForce( this.transform.parent.forward * attackForce, ForceMode.Impulse);
        UiManager.instance.DecreaseBowAmmo();
    }
}
