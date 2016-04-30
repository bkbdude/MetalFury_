using UnityEngine;
using System.Collections;

public enum DamageType { AllDamage, PointDamage, Frost, Fire, Lighting, Gravity}

public class Hazard : MonoBehaviour {
    internal int damageId = -1;

    public DamageType damageType;
    public int damageValue = 10;

    public void DamageCar(BaseCarMove carDamaged){
        switch (damageType)
        {
            case DamageType.AllDamage:{
                carDamaged.AllDamage(damageValue, damageId);
                break;
            }
            case DamageType.PointDamage:{
                break;
                }
            case DamageType.Frost:{
                break;
                }
            case DamageType.Fire:{
                break;
                }
            case DamageType.Gravity:{
                break;
                }
            case DamageType.Lighting:{
                break;
                }
        }
    }

}
