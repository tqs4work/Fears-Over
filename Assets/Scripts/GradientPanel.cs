using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class UIGradient : BaseMeshEffect
{
    public Color leftColor = Color.white;
    public Color rightColor = Color.black;

    [Range(0f, 1f)]
    public float gradientMidpoint = 0.5f; // 0 = full leftColor, 1 = full rightColor

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;

        UIVertex vertex = new UIVertex();
        float leftX = float.MaxValue;
        float rightX = float.MinValue;

        // Find bounds (min and max X)
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);
            float x = vertex.position.x;
            if (x > rightX) rightX = x;
            if (x < leftX) leftX = x;
        }

        float width = rightX - leftX;
        if (width == 0) width = 1f; // Prevent divide-by-zero

        // Apply gradient with midpoint shift
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);
            float t = (vertex.position.x - leftX) / width;

            // Shift t based on gradientMidpoint
            float adjustedT = Mathf.Clamp01(t / gradientMidpoint);
            if (t > gradientMidpoint)
                adjustedT = Mathf.Clamp01((t - gradientMidpoint) / (1f - gradientMidpoint));

            vertex.color = Color.Lerp(leftColor, rightColor, adjustedT);
            vh.SetUIVertex(vertex, i);
        }
    }
}
