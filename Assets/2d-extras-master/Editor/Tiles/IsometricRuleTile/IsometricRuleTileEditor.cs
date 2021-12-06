using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomEditor(typeof(IsometricRuleTile), true)]
    [CanEditMultipleObjects]
    public class IsometricRuleTileEditor : RuleTileEditor
    {
        private static readonly int[] s_Arrows = {3, 0, 1, 6, -1, 2, 7, 8, 5};

        public override int GetArrowIndex(Vector3Int position)
        {
            return s_Arrows[base.GetArrowIndex(position)];
        }

        public override Vector2 GetMatrixSize(BoundsInt bounds)
        {
            var p = Mathf.Pow(2, 0.5f);
            var w = (bounds.size.x / p + bounds.size.y / p) * k_SingleLineHeight;
            return new Vector2(w, w);
        }

        public override void RuleMatrixOnGUI(RuleTile ruleTile, Rect rect, BoundsInt bounds,
            RuleTile.TilingRule tilingRule)
        {
            Handles.color = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.2f) : new Color(0f, 0f, 0f, 0.2f);
            var w = rect.width / bounds.size.x;
            var h = rect.height / bounds.size.y;

            // Grid
            var d = rect.width / (bounds.size.x + bounds.size.y);
            for (var y = 0; y <= bounds.size.y; y++)
            {
                var left = rect.xMin + d * y;
                var top = rect.yMin + d * y;
                var right = rect.xMax - d * (bounds.size.y - y);
                var bottom = rect.yMax - d * (bounds.size.y - y);
                Handles.DrawLine(new Vector3(left, bottom), new Vector3(right, top));
            }

            for (var x = 0; x <= bounds.size.x; x++)
            {
                var left = rect.xMin + d * x;
                var top = rect.yMax - d * x;
                var right = rect.xMax - d * (bounds.size.x - x);
                var bottom = rect.yMin + d * (bounds.size.x - x);
                Handles.DrawLine(new Vector3(left, bottom), new Vector3(right, top));
            }

            Handles.color = Color.white;

            var neighbors = tilingRule.GetNeighbors();

            // Icons
            var iconSize = (rect.width - d) / (bounds.size.x + bounds.size.y - 1);
            var iconScale = Mathf.Pow(2, 0.5f);

            for (var y = bounds.yMin; y < bounds.yMax; y++)
            for (var x = bounds.xMin; x < bounds.xMax; x++)
            {
                var pos = new Vector3Int(x, y, 0);
                var offset = new Vector3Int(pos.x - bounds.xMin, pos.y - bounds.yMin, 0);
                var r = new Rect(
                    rect.xMin + rect.size.x - iconSize * (offset.y - offset.x + 0.5f + bounds.size.x),
                    rect.yMin + rect.size.y - iconSize * (offset.y + offset.x + 1.5f),
                    iconSize, iconSize
                );
                var center = r.center;
                r.size *= iconScale;
                r.center = center;

                RuleMatrixIconOnGUI(tilingRule, neighbors, pos, r);
            }
        }

        public override bool ContainsMousePosition(Rect rect)
        {
            var center = rect.center;
            var halfWidth = rect.width / 2;
            var halfHeight = rect.height / 2;
            var mouseFromCenter = Event.current.mousePosition - center;
            var xAbs = Mathf.Abs(Vector2.Dot(mouseFromCenter, Vector2.right));
            var yAbs = Mathf.Abs(Vector2.Dot(mouseFromCenter, Vector2.up));
            return xAbs / halfWidth + yAbs / halfHeight <= 1;
        }

        public override void OnPreviewSettings()
        {
            base.OnPreviewSettings();

            if (m_PreviewGrid)
            {
                var height = EditorGUILayout.FloatField("Cell Height", m_PreviewGrid.cellSize.y);
                m_PreviewGrid.cellSize = new Vector3(1f, Mathf.Max(height, 0), 1f);
            }
        }

        public override void CreatePreview()
        {
            base.CreatePreview();

            m_PreviewGrid.cellSize = new Vector3(1f, 0.5f, 1f);
            m_PreviewGrid.cellLayout = GridLayout.CellLayout.Isometric;

            foreach (var tilemapRenderer in m_PreviewTilemapRenderers)
                tilemapRenderer.sortOrder = TilemapRenderer.SortOrder.TopRight;

            m_PreviewTilemapRenderers[0].sortingOrder = 0;
            m_PreviewTilemapRenderers[1].sortingOrder = -1;
            m_PreviewTilemapRenderers[2].sortingOrder = 1;
            m_PreviewTilemapRenderers[3].sortingOrder = 0;
        }
    }
}