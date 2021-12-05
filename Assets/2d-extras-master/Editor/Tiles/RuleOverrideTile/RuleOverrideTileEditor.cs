using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomEditor(typeof(RuleOverrideTile))]
    public class RuleOverrideTileEditor : Editor
    {
        private static readonly float k_SpriteElementHeight = 48;
        private static readonly float k_GameObjectElementHeight = 16;
        private static readonly float k_PaddingBetweenRules = 4;
        private ReorderableList m_GameObjectList;

        private readonly List<KeyValuePair<GameObject, GameObject>> m_GameObjects =
            new List<KeyValuePair<GameObject, GameObject>>();

        private RuleTileEditor m_RuleTileEditor;
        private RuleTile m_RuleTileEditorTarget;
        private ReorderableList m_SpriteList;

        private readonly List<KeyValuePair<Sprite, Sprite>> m_Sprites = new List<KeyValuePair<Sprite, Sprite>>();

        public RuleOverrideTile overrideTile => target as RuleOverrideTile;

        public RuleTileEditor ruleTileEditor
        {
            get
            {
                if (m_RuleTileEditorTarget != overrideTile.m_Tile)
                {
                    DestroyImmediate(m_RuleTileEditor);
                    m_RuleTileEditor = CreateEditor(overrideTile.m_InstanceTile) as RuleTileEditor;
                    m_RuleTileEditorTarget = overrideTile.m_Tile;
                }

                return m_RuleTileEditor;
            }
        }

        public virtual void OnEnable()
        {
            if (m_SpriteList == null)
            {
                overrideTile.GetOverrides(m_Sprites);

                m_SpriteList = new ReorderableList(m_Sprites, typeof(KeyValuePair<Sprite, Sprite>), false, true, false,
                    false);
                m_SpriteList.drawHeaderCallback = DrawSpriteListHeader;
                m_SpriteList.drawElementCallback = DrawSpriteElement;
                m_SpriteList.elementHeightCallback = GetSpriteElementHeight;
            }

            if (m_GameObjectList == null)
            {
                overrideTile.GetOverrides(m_GameObjects);

                m_GameObjectList = new ReorderableList(m_GameObjects, typeof(KeyValuePair<Sprite, Sprite>), false, true,
                    false, false);
                m_GameObjectList.drawHeaderCallback = DrawGameObjectListHeader;
                m_GameObjectList.drawElementCallback = DrawGameObjectElement;
                m_GameObjectList.elementHeightCallback = GetGameObjectElementHeight;
            }
        }

        public virtual void OnDisable()
        {
            DestroyImmediate(ruleTileEditor);
            m_RuleTileEditorTarget = null;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            DrawTileField();
            DrawCustomFields();

            // Sprite List
            EditorGUI.BeginChangeCheck();
            overrideTile.GetOverrides(m_Sprites);

            m_SpriteList.list = m_Sprites;
            m_SpriteList.DoLayoutList();
            if (EditorGUI.EndChangeCheck())
                for (var i = 0; i < targets.Length; i++)
                {
                    var tile = targets[i] as RuleOverrideTile;
                    tile.ApplyOverrides(m_Sprites);
                    SaveTile();
                }

            // GameObject List
            EditorGUI.BeginChangeCheck();
            overrideTile.GetOverrides(m_GameObjects);

            m_GameObjectList.list = m_GameObjects;
            m_GameObjectList.DoLayoutList();
            if (EditorGUI.EndChangeCheck())
                for (var i = 0; i < targets.Length; i++)
                {
                    var tile = targets[i] as RuleOverrideTile;
                    tile.ApplyOverrides(m_GameObjects);
                    SaveTile();
                }
        }

        private void DrawSpriteListHeader(Rect rect)
        {
            var xMax = rect.xMax;
            rect.xMax = rect.xMax / 2.0f;
            GUI.Label(rect, "Original Sprite", EditorStyles.label);
            rect.xMin = rect.xMax;
            rect.xMax = xMax;
            GUI.Label(rect, "Override Sprite", EditorStyles.label);
        }

        private void DrawGameObjectListHeader(Rect rect)
        {
            var xMax = rect.xMax;
            rect.xMax = rect.xMax / 2.0f;
            GUI.Label(rect, "Original GameObject", EditorStyles.label);
            rect.xMin = rect.xMax;
            rect.xMax = xMax;
            GUI.Label(rect, "Override GameObject", EditorStyles.label);
        }

        private float GetSpriteElementHeight(int index)
        {
            var height = k_SpriteElementHeight + k_PaddingBetweenRules;

            var isMissing = index >= overrideTile.m_MissingSpriteIndex;
            if (isMissing)
                height += 16;

            return height;
        }

        private float GetGameObjectElementHeight(int index)
        {
            var height = k_GameObjectElementHeight + k_PaddingBetweenRules;

            var isMissing = index >= overrideTile.m_MissingGameObjectIndex;
            if (isMissing)
                height += 16;

            return height;
        }

        private void DrawSpriteElement(Rect rect, int index, bool selected, bool focused)
        {
            var isMissing = index >= overrideTile.m_MissingSpriteIndex;
            if (isMissing)
            {
                EditorGUI.HelpBox(new Rect(rect.xMin, rect.yMin, rect.width, 16), "Original Sprite missing",
                    MessageType.Warning);
                rect.yMin += 16;
            }

            var originalSprite = m_Sprites[index].Key;
            var overrideSprite = m_Sprites[index].Value;
            var fullRect = rect;

            rect.y += 2;
            rect.height -= k_PaddingBetweenRules;

            rect.xMax = rect.xMax / 2.0f;
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUI.ObjectField(new Rect(rect.xMin, rect.yMin, rect.height, rect.height), originalSprite,
                    typeof(Sprite), false);
            }

            rect.xMin = rect.xMax;
            rect.xMax *= 2.0f;

            EditorGUI.BeginChangeCheck();
            overrideSprite = EditorGUI.ObjectField(new Rect(rect.xMin, rect.yMin, rect.height, rect.height),
                overrideSprite, typeof(Sprite), false) as Sprite;
            if (EditorGUI.EndChangeCheck())
                m_Sprites[index] = new KeyValuePair<Sprite, Sprite>(originalSprite, overrideSprite);
        }

        private void DrawGameObjectElement(Rect rect, int index, bool selected, bool focused)
        {
            var isMissing = index >= overrideTile.m_MissingSpriteIndex;
            if (isMissing)
            {
                EditorGUI.HelpBox(new Rect(rect.xMin, rect.yMin, rect.width, 16), "Original Game Object missing",
                    MessageType.Warning);
                rect.yMin += 16;
            }

            var originalGameObject = m_GameObjects[index].Key;
            var overrideGameObject = m_GameObjects[index].Value;
            var fullRect = rect;

            rect.y += 2;
            rect.height -= k_PaddingBetweenRules;

            rect.xMax = rect.xMax / 2.0f;
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUI.ObjectField(new Rect(rect.xMin, rect.yMin, rect.width, rect.height), originalGameObject,
                    typeof(GameObject), false);
            }

            rect.xMin = rect.xMax;
            rect.xMax *= 2.0f;

            EditorGUI.BeginChangeCheck();
            overrideGameObject = EditorGUI.ObjectField(new Rect(rect.xMin, rect.yMin, rect.width, rect.height),
                overrideGameObject, typeof(GameObject), false) as GameObject;
            if (EditorGUI.EndChangeCheck())
                m_GameObjects[index] = new KeyValuePair<GameObject, GameObject>(originalGameObject, overrideGameObject);
        }

        public void DrawTileField()
        {
            EditorGUI.BeginChangeCheck();
            var tile = EditorGUILayout.ObjectField("Tile", overrideTile.m_Tile, typeof(RuleTile), false) as RuleTile;
            if (EditorGUI.EndChangeCheck())
            {
                if (!LoopCheck(tile))
                {
                    overrideTile.m_Tile = tile;
                    SaveTile();
                }
                else
                {
                    Debug.LogWarning("Circular tile reference");
                }
            }

            bool LoopCheck(RuleTile checkTile)
            {
                if (!overrideTile.m_InstanceTile)
                    return false;

                var renferenceTils = new HashSet<RuleTile>();
                Add(overrideTile.m_InstanceTile);

                return renferenceTils.Contains(checkTile);

                void Add(RuleTile ruleTile)
                {
                    if (renferenceTils.Contains(ruleTile))
                        return;

                    renferenceTils.Add(ruleTile);

                    var overrideTiles = RuleTileEditor.FindAffectedOverrideTiles(ruleTile);

                    foreach (var overrideTile in overrideTiles)
                        Add(overrideTile.m_InstanceTile);
                }
            }
        }

        public void DrawCustomFields()
        {
            if (ruleTileEditor)
            {
                ruleTileEditor.target.hideFlags = HideFlags.None;
                ruleTileEditor.DrawCustomFields();
                ruleTileEditor.target.hideFlags = HideFlags.NotEditable;
            }
        }

        public void SaveInstanceTileAsset()
        {
            var assetChanged = false;

            if (overrideTile.m_InstanceTile)
                if (!overrideTile.m_Tile || overrideTile.m_InstanceTile.GetType() != overrideTile.m_Tile.GetType())
                {
                    DestroyImmediate(overrideTile.m_InstanceTile, true);
                    overrideTile.m_InstanceTile = null;
                    assetChanged = true;
                }

            if (!overrideTile.m_InstanceTile)
                if (overrideTile.m_Tile)
                {
                    var t = overrideTile.m_Tile.GetType();
                    var instanceTile = CreateInstance(t) as RuleTile;
                    instanceTile.hideFlags = HideFlags.NotEditable;
                    AssetDatabase.AddObjectToAsset(instanceTile, overrideTile);
                    overrideTile.m_InstanceTile = instanceTile;
                    assetChanged = true;
                }

            if (overrideTile.m_InstanceTile)
            {
                var instanceTileName = overrideTile.m_Tile.name + " (Override)";
                if (overrideTile.m_InstanceTile.name != instanceTileName)
                {
                    overrideTile.m_InstanceTile.name = instanceTileName;
                    assetChanged = true;
                }
            }

            if (assetChanged)
                AssetDatabase.SaveAssets();
        }

        public void SaveTile()
        {
            EditorUtility.SetDirty(target);
            SceneView.RepaintAll();

            SaveInstanceTileAsset();

            if (overrideTile.m_InstanceTile)
            {
                overrideTile.Override();
                RuleTileEditor.UpdateAffectedOverrideTiles(overrideTile.m_InstanceTile);
            }

            if (ruleTileEditor && ruleTileEditor.m_PreviewTilemaps != null)
                foreach (var tilemap in ruleTileEditor.m_PreviewTilemaps)
                    tilemap.RefreshAllTiles();
        }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            if (ruleTileEditor)
                return ruleTileEditor.RenderStaticPreview(assetPath, subAssets, width, height);

            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }

        public override bool HasPreviewGUI()
        {
            if (ruleTileEditor)
                return ruleTileEditor.HasPreviewGUI();

            return false;
        }

        public override void OnPreviewSettings()
        {
            if (ruleTileEditor)
                ruleTileEditor.OnPreviewSettings();
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            if (ruleTileEditor)
                ruleTileEditor.OnPreviewGUI(r, background);
        }
    }
}