using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public int length;
    public LineRenderer lineRen;
    Vector3[] segmentPoses;

    public Transform targetDir;
    public float targetDist;
    Vector3[] segmentV;
    public float smoothSpeed;

    private void Start()
    {
        lineRen.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    private void Update()
    {
        segmentPoses[0] = targetDir.position;

        for(int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed);
        }

        lineRen.GetPositions(segmentPoses);
    }
}
