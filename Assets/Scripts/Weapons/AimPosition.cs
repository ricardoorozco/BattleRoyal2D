using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPosition : MonoBehaviour
{
    [SerializeField]
    Transform aim;
    [SerializeField]
    Transform weapon;
    [SerializeField]
    Transform rightHandLimb;
    [SerializeField]
    Transform leftHandLimb;

    public void setWeapon(Transform weapon)
    {
        this.weapon = weapon;
    }
    public void setAim(Transform aim)
    {
        this.aim = aim;
    }

    void LateUpdate()
    {
        if (this.weapon)
        {
            this.playerAim();
            this.aim.rotation = new Quaternion(0f, 0f, 0f, 1);
            this.aim.position = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z
                ));


            //weapon look the aim
            
        }

    }

    private void playerAim()
    {
        if (this.weapon && this.aim)
        {
            this.rightHandLimb.position = this.aim.position;
        }
    }
}
