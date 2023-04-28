using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float enginePower = 1000f; // engine power
    public float brakePower = 500f; // brake power
    public float steerAngle = 30f; // max steer angle
    public float maxSpeed = 50f; // max speed
    public float sensitivity = 1f; // input sensitivity

    private float currentSpeed; // current speed
    private float currentSteerAngle; // current steer angle
    private float currentBrakePower; // current brake power

    private FuzzyInferenceSystem fis; // fuzzy inference system

    public WheelCollider[] wheelColliders; // wheel colliders

    // Start is called before the first frame update
    void Start()
    {
        fis = new FuzzyInferenceSystem();

        // Define input linguistic variables and fuzzy sets
        LinguisticVariable speed = new LinguisticVariable("Speed");
        speed.AddFuzzySet(new TriangularFuzzySet("Slow", 0f, 0f, 10f));
        speed.AddFuzzySet(new TriangularFuzzySet("Medium", 5f, 15f, 25f));
        speed.AddFuzzySet(new TriangularFuzzySet("Fast", 20f, 30f, 30f));
        fis.Inputs.Add(speed, 0f);

        LinguisticVariable steering = new LinguisticVariable("Steering");
        steering.AddFuzzySet(new TriangularFuzzySet("Left", -1f, -1f, 0f));
        steering.AddFuzzySet(new TriangularFuzzySet("Center", -0.5f, 0f, 0.5f));
        steering.AddFuzzySet(new TriangularFuzzySet("Right", 0f, 1f, 1f));
        fis.Inputs.Add(steering, 0f);

        // Define output linguistic variable and fuzzy sets
        LinguisticVariable acceleration = new LinguisticVariable("Acceleration");
        acceleration.AddFuzzySet(new TriangularFuzzySet("Brake", -1f, -1f, 0f));
        acceleration.AddFuzzySet(new TriangularFuzzySet("Neutral", -0.5f, 0f, 0.5f));
        acceleration.AddFuzzySet(new TriangularFuzzySet("Accelerate", 0f, 1f, 1f));
        fis.Outputs.Add(acceleration, 0f);

        // Define fuzzy rules
        FuzzyRuleSet ruleSet = new FuzzyRuleSet();
        FuzzyRule rule1 = new FuzzyRule();
        rule1.Antecedents.Add(speed, speed.FuzzySets[0]);
        rule1.Consequents.Add(acceleration, acceleration.FuzzySets[2]);
        ruleSet.AddRule(rule1);
        FuzzyRule rule2 = new FuzzyRule();
        rule2.Antecedents.Add(speed, speed.FuzzySets[1]);
        rule2.Consequents.Add(acceleration, acceleration.FuzzySets[1]);
        ruleSet.AddRule(rule2);
        FuzzyRule rule3 = new FuzzyRule();
        rule3.Antecedents.Add(speed, speed.FuzzySets[2]);
        rule3.Consequents.Add(acceleration, acceleration.FuzzySets[0]);
        ruleSet.AddRule(rule3);
        FuzzyRule rule4 = new FuzzyRule();
        rule4.Antecedents.Add(steering, steering.FuzzySets[0]);
        rule4.Consequents.Add(acceleration, acceleration.FuzzySets[1]);
        ruleSet.AddRule(rule4);
        FuzzyRule rule5 = new FuzzyRule();
        rule5.Antecedents.Add(steering, steering.FuzzySets[1]);
        rule5.Consequents.Add(acceleration, acceleration.FuzzySets[1]);
        ruleSet.AddRule(rule5);
        FuzzyRule rule6 = new FuzzyRule();
        rule6.Antecedents.Add(steering, steering.FuzzySets[2]);
        rule6.Consequents.Add(acceleration, acceleration.FuzzySets[1]);
        ruleSet.AddRule(rule6);
        fis.RuleSet = ruleSet;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get user input
        float inputVertical = Input.GetAxis("Vertical") * sensitivity;
        float inputHorizontal = Input.GetAxis("Horizontal") * sensitivity;

        // Calculate current speed and steer angle
        currentSpeed = wheelColliders[0].rpm * wheelColliders[0].radius * Mathf.PI / 30f;
        currentSteerAngle = inputHorizontal * steerAngle;

        // Calculate fuzzy inputs and infer output
        fis.Inputs[0].Value = currentSpeed;
        fis.Inputs[1].Value = currentSteerAngle;
        fis.Infer();
        float output = fis.Outputs[0].Value;

        // Apply brakes or acceleration
        if (output < 0f)
        {
            currentBrakePower = -output * brakePower;
            wheelColliders[0].brakeTorque = currentBrakePower;
            wheelColliders[1].brakeTorque = currentBrakePower;
        }
        else
        {
            currentBrakePower = 0f;
            wheelColliders[0].brakeTorque = 0f;
            wheelColliders[1].brakeTorque = 0f;
            float currentEnginePower = output * enginePower;
            wheelColliders[0].motorTorque = currentEnginePower;
            wheelColliders[1].motorTorque = currentEnginePower;
        }

        // Apply steering
        wheelColliders[0].steerAngle = currentSteerAngle;
        wheelColliders[1].steerAngle = currentSteerAngle;

        // Limit max speed
        if (currentSpeed > maxSpeed)
        {
            wheelColliders[0].brakeTorque = brakePower;
            wheelColliders[1].brakeTorque = brakePower;
        }
    }
}
