using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; }

    [System.Serializable]
    private struct RotationVelocity
    {
        public float weight;
        public float angularVelocity;
    }

    [SerializeField]
    private List<BoxComponent> boxes;
    [SerializeField]
    private Transform plane;
    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform centerPoint;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private RotationVelocity maxRotationVelocity;
    [SerializeField]
    private float maxSecondsPerWeightProblem;
    [SerializeField]
    private float maxSecondsPerAngleProblem;
    [SerializeField]
    private float minimumTimePerWeightProblem;
    [SerializeField]
    private float minimumTimePerAngleProblem;
    [SerializeField]
    private float timeWeightProblem;
    [SerializeField]
    private float angleLost;
    [SerializeField]
    private EZCameraShake.CameraShaker cameraShaker;

    private float engineMalfunction;

    private bool weightProblem;

    private float totalWeight;
    private float weightBalance;

    private float timeForWeightProblem;
    private float timeForAngleProblem;

    private float timePassedSinceWeightProblem;
    private float timePassedSinceAngleProblem;

    private float timePassedInWeightProblem;

    private float weightExcess;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        timeForWeightProblem = Random.Range(minimumTimePerWeightProblem, maxSecondsPerWeightProblem);
    }

    public void CalculateWeight()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            totalWeight += (float)boxes[i].myType;
        }

        engineMalfunction = 0;
    }

    private void Update()
    {
        EvaluateWeights();

        if(!weightProblem)
        {
            timePassedSinceWeightProblem += Time.deltaTime;

            if(timePassedSinceWeightProblem >= timeForWeightProblem)
            {
                timePassedSinceWeightProblem = 0;

                StartWeightProblem();

                weightProblem = true;
            }
        }

        timePassedSinceAngleProblem += Time.deltaTime;

        if (timePassedSinceAngleProblem >= timeForAngleProblem)
        {
            timePassedSinceAngleProblem = 0;

            StartAngleProblem();
        }

        RotatePlane();

        if(Vector3.Angle(Vector3.up, plane.transform.up) >= angleLost)
        {
            Debug.Log("Has Perdido");
        }

        if(weightProblem)
        {
            timePassedInWeightProblem += Time.deltaTime;

            if(timePassedInWeightProblem >= timeWeightProblem)
            {
                timePassedInWeightProblem = 0;
                timePassedSinceWeightProblem = 0;
                weightProblem = false;
                Debug.Log("Has Perdido");
            }
        }
    }

    public void EvaluateWeights()
    {
        weightBalance = 0;

        for (int i = 0; i < boxes.Count; i++)
        {
            if(centerPoint.InverseTransformPoint(boxes[i].transform.position).x < centerPoint.localPosition.x)
            {
                float distanceCenterToLeft = Mathf.Abs(centerPoint.InverseTransformPoint(leftPoint.position).x);
                float distanceCenterToBox = Mathf.Abs(centerPoint.InverseTransformPoint(boxes[i].transform.position).x);

                weightBalance += (-(float)boxes[i].myType * (Mathf.Clamp(distanceCenterToBox / distanceCenterToLeft, 0, 1)));
            }

            else if(centerPoint.InverseTransformPoint(boxes[i].transform.position).x > centerPoint.localPosition.x)
            {
                float distanceCenterToRight = Mathf.Abs(centerPoint.InverseTransformPoint(rightPoint.position).x);
                float distanceCenterToBox = Mathf.Abs(centerPoint.InverseTransformPoint(boxes[i].transform.position).x);

                weightBalance += ((float)boxes[i].myType * (Mathf.Clamp(distanceCenterToBox / distanceCenterToRight, 0, 1)));
            }
        }
    }

    private void RotatePlane()
    {
        float balancedWeight = engineMalfunction + weightBalance;

        float angularSpeed = Mathf.Abs(maxRotationVelocity.angularVelocity * balancedWeight / maxRotationVelocity.weight);

        plane.Rotate(plane.forward * angularSpeed * -Mathf.Sign(balancedWeight) * Time.deltaTime);
    }

    public void BoxThrown(BoxComponent box)
    {
        boxes.Remove(box);
        weightExcess -= (float)box.myType;

        if(weightExcess <= 0 && weightProblem)
        {
            weightProblem = false;
            timeForWeightProblem = Random.Range(minimumTimePerWeightProblem, maxSecondsPerWeightProblem);
        }
    }

    private void StartWeightProblem()
    {
        weightExcess = Random.Range(10f, 50f);
        Debug.Log("Start Weight");
    }

    private void StartAngleProblem()
    {
        int wing = Random.Range(0, 2);

        if(wing == 0)
        {
            engineMalfunction += -(Random.Range(5f, 15f));
        }

        else if(wing == 1)
        {
            engineMalfunction += Random.Range(5f, 15f);
        }

        timeForAngleProblem = Random.Range(minimumTimePerAngleProblem, maxSecondsPerAngleProblem);

        cameraShaker.ShakeOnce(6f, 6f, .1f, 2f);
    }
}
