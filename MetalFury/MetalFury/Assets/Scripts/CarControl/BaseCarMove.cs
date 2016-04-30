using UnityEngine;
using System.Collections;

public enum carState{normal,frozen,death}

public class BaseCarMove : MonoBehaviour {

    public Playercontroller player;

    public Rigidbody rigidbody;
    public WeopenController weaponController;

    [Header("Health")]
    internal carState currentCarState = carState.normal;
    public int currentHealth = 100;
    public int maxHealth = 100;
    int lastDamagePlayerId = -1;

    [Header("Base Control")]
    public float speed = 500;//current speed
    public float currentMaxSpeed = 200;//current top speed
    public float maxSpeed = 2200;//normal top speed
    public float backwardMod = 0.6f;//muiltpled by speed when moving backward to slow player


    [Header("Boost Control")]
    public float boostForce = 2;
    float boostValue = 100;
    public float boostValueIncrease = 2;
    public float boostValueDecrease = 2;

    [Header("Drift Control")]
    public float turningMod = 1;//handling value
    public float driftForce = 175;//force added while drifting

    public WheelCollider[] drivingWheels;
    public WheelCollider[] steeringWheels;



    void Awake()
    {
    }
	// Use this for initialization
	void Start () {
        weaponController = GetComponent<WeopenController>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = Vector3.down;
        currentHealth = maxHealth;
	}

    public void Reset() {
        currentCarState = carState.normal;
        currentHealth = maxHealth;
    }

	// Update is called once per frame
	void Update () {
	    if(currentHealth <= 0){
            changeCarState(carState.death);
        }

	}
    public void changeCarState(carState newState){
        switch (newState)
        {
            case carState.normal: {
                break;
            }
            case carState.frozen: {
                    break;
            }
            case carState.death: {
                for (int i = 0; i < drivingWheels.Length; i++)
                {
                    drivingWheels[i].motorTorque = 0;
                }
                for (int i = 0; i < drivingWheels.Length; i++)
                {
                    steeringWheels[i].steerAngle = 0;
                }
                player.Respawn(lastDamagePlayerId);
                    break;
            }
        }
        currentCarState = newState;
    }
    void FixedUpdate()
    {
        if(currentCarState != carState.death){
            carMovment();
        }
      /*  RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
           // transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //gravityDir = (hit.point - transform.position).normalized;

        }
        Debug.DrawRay(transform.position, -transform.up, Color.red);

       // rigidbody.AddForce(graityScale * rigidbody.mass * gravityDir);*/
    }
    void carMovment() {

        if (player.canInput)
        {
            float steer = player.input.GetAxis("Steer");
            float gas = player.input.GetAxis("Gas");

            //rigidbody.AddRelativeForce(new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, Mathf.Clamp((gas * speed) + rigidbody.velocity.z, -currentMaxSpeed, currentMaxSpeed)));

            if (player.input.GetButton("Boost") && boostValue > 0)
            {
                rigidbody.velocity = transform.forward * boostForce;  //+ new Vector3(rigidbody.velocity.x,rigidbody.velocity.y, Mathf.Clamp((boostForce * 1f),0f,1000f));
                boostValue -= Mathf.Clamp(Time.deltaTime * boostValueDecrease, 0, 100);
            }
            else if (boostValue <= 100)
            {
                boostValue += Mathf.Clamp(Time.deltaTime * boostValueIncrease, 0, 100);
            }

            if (player.input.GetButton("Drift"))
            {
                for (int i = 0; i < drivingWheels.Length; i++)
                {

                    //drivingWheels[i].motorTorque = 20;
                    drivingWheels[i].brakeTorque = 10;
                }
                if (steer > 0)
                {
                    //rigidbody.AddRelativeForce(transform.forward * 1000);
                    // rigidbody.AddRelativeForce((transform.right) * 950);
                }
                else if (steer < 0)
                {
                    //rigidbody.AddRelativeForce(transform.forward * 1000);
                    // rigidbody.AddRelativeForce((-transform.right) * 950);
                }

                for (int i = 0; i < steeringWheels.Length; i++)
                {

                    float steeringAngle = Mathf.Lerp(steeringWheels[i].steerAngle, steer * 30, Time.time / 2);
                    steeringWheels[i].steerAngle = steeringAngle;
                }
            }
            else
            {
                for (int i = 0; i < drivingWheels.Length; i++)
                {
                    drivingWheels[i].brakeTorque = 0;
                    drivingWheels[i].motorTorque = gas * speed;
                }

                for (int i = 0; i < steeringWheels.Length; i++)
                {

                    float steeringAngle = Mathf.Lerp(steeringWheels[i].steerAngle, steer * 15, Time.time / 2);
                    steeringWheels[i].steerAngle = steeringAngle;
                }
            }
        }
    }

    public void AllDamage(int damageValue, int damageId) {
        currentHealth -= damageValue;
        lastDamagePlayerId = damageId;
        //DamageZone.LosePart();
    }
}
