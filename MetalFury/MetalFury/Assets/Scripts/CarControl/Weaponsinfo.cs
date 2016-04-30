using UnityEngine;
using System.Collections;

public class Weaponsinfo : MonoBehaviour {

    //public GameObject[] arts;
    public float rateOfFire;
    public float rateOfFireTimer = 0;

    public int shotLimit;
    public int shotLimitMax;

    public float bulletForce = 1000;

    public GameObject bullet;
    public Transform[] bulletSpawnPoint;

    public void initWeapon() {
        /*foreach (GameObject art in arts)
        {
            art.SetActive(value);
        }*/
        rateOfFireTimer = rateOfFire;
        shotLimit = shotLimitMax;
    }



    public bool FireWeapon(WeaponState currentWeaponState, int playerId)
    {

        if (rateOfFireTimer <= 0)
        {
            //gatFlash.Play();
            //audio.PlayOneShot(gatlingGunShot);


            rateOfFireTimer = rateOfFire;
            foreach (Transform i in bulletSpawnPoint)
            {
                GameObject boo = Instantiate(bullet, i.position, i.rotation) as GameObject;//Will change to object pool in future
                boo.GetComponent<Hazard>().damageId = playerId;
                boo.GetComponent<Rigidbody>().AddForce(boo.transform.forward * bulletForce);
            }
            //bullet1.layer = gameObject.layer;
            bool gunAmmoValue = true;

            switch (currentWeaponState)
            {
                case WeaponState.empty:
                    {
                        break;
                    }
                case WeaponState.gun:
                    {
                        gunAmmoValue = loseAmmo();
                        break;
                    }
                case WeaponState.napalm:
                    {
                        break;
                    }
                case WeaponState.saw:
                    {
                        break;
                    }
                case WeaponState.mine:
                    {
                        break;
                    }
            }
            return gunAmmoValue;
        }
        else if (rateOfFireTimer > 0)
        {
            rateOfFireTimer -= Time.deltaTime;
        }
        return true;
    }
    bool loseAmmo() {
        shotLimit--;//If there is a ammo limit
        if (shotLimit <= 0)
        {
            shotLimit = shotLimitMax;
            return false;
        }
        return true;
    }


}
