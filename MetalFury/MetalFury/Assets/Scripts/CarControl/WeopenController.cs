using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(BaseCarMove))]

public enum WeaponState { gun, napalm, saw, mine, lance, empty }

public class WeopenController : MonoBehaviour {

    public WeaponState currentWeaponState;
    BaseCarMove playerCar;
    public Weaponsinfo[] weopens;

    void Awake() {
        playerCar = GetComponent<BaseCarMove>();
    }

	// Use this for initialization
	void Start () {
        currentWeaponState = WeaponState.empty;
	}

    public void ChangeState(WeaponState newState) {

        Debug.Log("Current WeaponState: " + currentWeaponState);
        if (currentWeaponState != WeaponState.empty)
        {
            weopens[(int)currentWeaponState].gameObject.SetActive(false);
        }
            currentWeaponState = newState;
            if (currentWeaponState != WeaponState.empty)
            {
                weopens[(int)currentWeaponState].gameObject.SetActive(true);
                weopens[(int)currentWeaponState].initWeapon();
            }
    }

	// Update is called once per frame
	void Update () {

        if (currentWeaponState != WeaponState.empty)
        {
            if (playerCar.player.input.GetButton("Fire"))
            {
                if (!weopens[(int)currentWeaponState].FireWeapon(currentWeaponState, playerCar.player.playerId))
                {
                    ChangeState(WeaponState.empty);
                }
            }
        }
	}
}
