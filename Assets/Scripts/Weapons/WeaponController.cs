using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    AimPosition aim;

    // Start is called before the first frame update
    void Start()
    {
        aim = Camera.main.gameObject.GetComponent<AimPosition>();
    }

    public void setWeapon(PlayerController player)
    {
        if (!player.haveWeapon && this.name == "DesertEagle_weapon")
        {
            putDesert(player);
        }
    }

    private void putDesert(PlayerController player) {
        Destroy(GetComponent<BoxCollider2D>());
        player.haveWeapon = true;
        this.gameObject.transform.parent = player.handRight;
        this.transform.localPosition = Vector3.zero;
        
        aim.setAim(GameObject.FindGameObjectWithTag("DesertEagle_aim").transform);
        aim.setWeapon(this.transform);
    }
}
