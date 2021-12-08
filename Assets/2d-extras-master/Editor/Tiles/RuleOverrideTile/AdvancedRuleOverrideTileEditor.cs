using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomEditor(typeof(AdvancedRuleOverrideTile))]
    public class AdvancedRuleOverrideTileEditor : RuleOverrideTileEditor
    {
        private readonly List<KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRuleOutput>> m_Rules =
            new List<KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRuleOutput>>();

        private ReorderableList m_RuleList;

        public new AdvancedRuleOverrideTile overrideTile => target as AdvancedRuleOverrideTile;

        private static float k_DefaultElementHeight => RuleTileEditor.k_DefaultElementHeight;
        private static float k_PaddingBetweenRules => RuleTileEditor.k_PaddingBetweenRules;
        private static float k_SingleLineHeight => RuleTileEditor.k_SingleLineHeight;
        private static float k_LabelWidth => RuleTileEditor.k_LabelWidth;

        public override void OnEnable()
        {
            if (m_RuleList == null)
            {
                m_RuleList = new ReorderableList(m_Rules,
                    typeof(KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRule>), false, true, false, false);
                m_RuleList.drawHeaderCallback = DrawRulesHeader;
                m_RuleList.drawElementCallback = DrawRuleElement;
                m_RuleList.elementHeightCallback = GetRuleElementHeight;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            DrawTileField();

            EditorGUI.BeginChangeCheck();
            overrideTile.m_DefaultSprite =
                EditorGUILayout.ObjectField("Default Sprite", overrideTile.m_DefaultSprite, typeof(Sprite), false) as
                    Sprite;
            overrideTile.m_DefaultGameObject = EditorGUILayout.ObjectField("Default Game Object",
                overrideTile.m_DefaultGameObject, typeof(GameObject), false) as GameObject;
            overrideTile.m_DefaultColliderType =
                (Tile.ColliderType) EditorGUILayout.EnumPopup("Default Collider", overrideTile.m_DefaultColliderType);
            if (EditorGUI.EndChangeCheck())
                SaveTile();

            DrawCustomFields();

            m_Rules.Clear();
            if (overrideTile.m_Tile)
                overrideTile.GetOverrides(m_Rules);

            m_RuleList.DoLayoutList();
        }

        private void DrawRulesHeader(Rect rect)
        {
            GUI.Label(rect, "Tiling Rules", EditorStyles.label);
        }

        private void DrawRuleElement(Rect rect, int index, bool selected, bool focused)
        {
            var originalRule = m_Rules[index].Key;
            var overrideRule = m_Rules[index].Value;
            var isMissing = index >= overrideTile.m_MissingTilingRuleIndex;

            DrawToggleInternal(new Rect(rect.xMin, rect.yMin, 16, rect.height));
            DrawRuleInternal(new Rect(rect.xMin + 16, rect.yMin, rect.width - 16, rect.height));

            void DrawToggleInternal(Rect r)
            {
                EditorGUI.BeginChangeCheck();

                var enabled = EditorGUI.Toggle(new Rect(r.xMin, r.yMin, r.width, k_SingleLineHeight),
                    overrideRule != null);

                if (EditorGUI.EndChangeCheck())
                {
                    if (enabled)
                        overrideTile[originalRule] = originalRule;
                    else
                        overrideTile[originalRule] = null;

                    SaveTile();
                }
            }

            void DrawRuleInternal(Rect r)
            {
                EditorGUI.BeginChangeCheck();

                DrawRule(r, overrideRule ?? originalRule, overrideRule != null, originalRule, isMissing);

                if (EditorGUI.EndChangeCheck())
                    SaveTile();
            }
        }

        private void DrawRule(Rect rect, RuleTile.TilingRuleOutput rule, bool isOverride,
            RuleTile.TilingRule originalRule, bool isMissing)
        {
            if (isMissing)
            {
                EditorGUI.HelpBox(new Rect(rect.xMin, rect.yMin, rect.width, 16), "Original Tiling Rule missing",
                    MessageType.Warning);
                rect.yMin += 16;
            }

            using (new EditorGUI.DisabledScope(!isOverride))
            {
                var yPos = rect.yMin + 2f;
                var height = rect.height - k_PaddingBetweenRules;
                var matrixWidth = k_DefaultElementHeight;

                var ruleBounds = originalRule.GetBounds();
                var ruleGuiBounds = ruleTileEditor.GetRuleGUIBounds(ruleBounds, originalRule);
                var matrixSize = ruleTileEditor.GetMatrixSize(ruleGuiBounds);
                var matrixSizeRate = matrixSize / Mathf.Max(matrixSize.x, matrixSize.y);
                var matrixRectSize = new Vector2(matrixWidth * matrixSizeRate.x,
                    k_DefaultElementHeight * matrixSizeRate.y);
                var matrixRectPosition = new Vector2(rect.xMax - matrixWidth * 2f - 10f, yPos);
                matrixRectPosition.x += (matrixWidth - matrixRectSize.x) * 0.5f;
                matrixRectPosition.y += (k_DefaultElementHeight - matrixRectSize.y) * 0.5f;

                var inspectorRect = new Rect(rect.xMin, yPos, rect.width - matrixWidth * 2f - 20f, height);
                var matrixRect = new Rect(matrixRectPosition, matrixRectSize);
                var spriteRect = new Rect(rect.xMax - matrixWidth - 5f, yPos, matrixWidth, k_DefaultElementHeight);

                ruleTileEditor.RuleInspectorOnGUI(inspectorRect, rule);
                ruleTileEditor.SpriteOnGUI(spriteRect, rule);

                if (!isMissing)
                    using (new EditorGUI.DisabledScope(true))
                    {
                        ruleTileEditor.RuleMatrixOnGUI(overrideTile.m_InstanceTile, matrixRect, ruleGuiBounds,
                            originalRule);
                    }
            }
        }

        private float GetRuleElementHeight(int index)
        {
            var originalRule = m_Rules[index].Key;
            var overrideRule = m_Rules[index].Value;
            var height = overrideRule != null
                ? ruleTileEditor.GetElementHeight(overrideRule)
                : ruleTileEditor.GetElementHeight(originalRule);

            var isMissing = index >= overrideTile.m_MissingTilingRuleIndex;
            if (isMissing)
                height += 16;

            return height;
        }
    }
}