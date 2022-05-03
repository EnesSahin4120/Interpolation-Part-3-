using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class De_Casteljaus : MonoBehaviour
{
    [SerializeField] private int frameCount;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform firstControlPoint;
    [SerializeField] private Transform secondControlPoint;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void DrawCurve()
    {
        for (int i = 0; i < frameCount; i++)
        {
            float _t = i / (float)frameCount;
            Vector3 curvePos = PositionOnCurve(_t);
            lineRenderer.SetPosition(i, curvePos);
        }
        lineRenderer.positionCount = frameCount;
    }

    private void Update()
    {
        DrawCurve();
    }

    private Vector3 PositionOnCurve(float t)
    {
        Vector3 A = (1 - t) * startPoint.position + t * firstControlPoint.position;
        Vector3 B = (1 - t) * firstControlPoint.position + t * secondControlPoint.position;
        Vector3 C = (1 - t) * secondControlPoint.position + t * endPoint.position;

        Vector3 D = (1 - t) * A + t * B;
        Vector3 E = (1 - t) * B + t * C;

        Vector3 F = (1 - t) * D + t * E;
        return F;
    }
}