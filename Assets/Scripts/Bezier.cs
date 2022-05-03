using UnityEngine;
[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class Bezier : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private int segmentFrameCount;

    private Vector3 _p0;
    private Vector3 _p1;
    private Vector3 _p2;
    private Vector3 _p3;
    private int segmentCount;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        GetInfo();
    }

    private void GetInfo()
    {
        lineRenderer = GetComponent<LineRenderer>();
        segmentCount = (int)points.Length / 3;
    }

    void Update()
    {
        DrawCurve();
    }

    void DrawCurve()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            for (int j = 1; j < segmentFrameCount - 1; j++)
            {
                float _t = j / (float)segmentFrameCount;
                int index = i * 3;

                _p0 = points[index].position;
                _p1 = points[index + 1].position;
                _p2 = points[index + 2].position;
                _p3 = points[index + 3].position;

                Vector3 curvePos = PositionOnCurve(_p0, _p1, _p2, _p3, _t);
                lineRenderer.positionCount = i * segmentFrameCount + j;
                lineRenderer.SetPosition((i * segmentFrameCount) + (j - 1), curvePos);
            }
        }
    }

    Vector3 PositionOnCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 position = (1-t) * (1-t) * (1-t) * p0
              + 3 * (1-t) * (1-t) * t * p1
              + 3 * (1-t) * t * t * p2
              + t * t * t * p3;
        return position;
    }
}