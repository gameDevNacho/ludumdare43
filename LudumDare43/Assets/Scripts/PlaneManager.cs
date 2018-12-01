using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance { get; private set; }

    [System.Serializable]
    private struct Box
    {
        public Transform box;
        public float weight;
    }

    [System.Serializable]
    private struct RotationVelocity
    {
        public float weight;
        public float angularVelocity;
    }

    [SerializeField]
    private List<Box> boxes;
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

    private float engineMalfunction;

    private float totalWeight;
    private float weightBalance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void CalculateWeight()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            totalWeight += boxes[i].weight;
        }

        engineMalfunction = 0;
    }

    private void Update()
    {
        EvaluateWeights();

        RotatePlane();
    }

    public void EvaluateWeights()
    {
        weightBalance = 0;

        for (int i = 0; i < boxes.Count; i++)
        {
            if(centerPoint.InverseTransformPoint(boxes[i].box.position).x < centerPoint.localPosition.x)
            {
                float distanceCenterToLeft = Mathf.Abs(centerPoint.InverseTransformPoint(leftPoint.position).x);
                float distanceCenterToBox = Mathf.Abs(centerPoint.InverseTransformPoint(boxes[i].box.position).x);

                weightBalance += (-boxes[i].weight * (distanceCenterToBox / distanceCenterToLeft));
            }

            else if(centerPoint.InverseTransformPoint(boxes[i].box.position).x > centerPoint.localPosition.x)
            {
                float distanceCenterToRight = Mathf.Abs(centerPoint.InverseTransformPoint(rightPoint.position).x);
                float distanceCenterToBox = Mathf.Abs(centerPoint.InverseTransformPoint(boxes[i].box.position).x);

                weightBalance += (boxes[i].weight * (distanceCenterToBox / distanceCenterToRight));
            }
        }
    }

    private void RotatePlane()
    {
        float balancedWeight = engineMalfunction + weightBalance;

        float angularSpeed = Mathf.Abs(maxRotationVelocity.angularVelocity * balancedWeight / maxRotationVelocity.weight);

        plane.Rotate(plane.forward * angularSpeed * -Mathf.Sign(balancedWeight) * Time.deltaTime);
    }
}
