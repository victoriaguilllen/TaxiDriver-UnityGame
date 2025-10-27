using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControler : MonoBehaviour
{
    private CarController carController;
    public bool reduce;
    public float reductionTime;
    public bool use;
    void Start()
    {
        carController = GetComponent<CarController>();
        reduce = false;
        use = false;
    }

    public void ReduceSpeed(float by)
    {
        carController.SetGasInputReduction(by);
        reduce = true;
    }

    public void Update()
    {
        if (reduce == true)
        {
            reductionTime -= Time.deltaTime;

            if (reductionTime <= 0)
            {
                reduce = false;
                carController.SetGasInputReduction(1);
            }

        }
    }

    public void SetReductionTime(float time)
    {
        reductionTime += time;
    }


}

