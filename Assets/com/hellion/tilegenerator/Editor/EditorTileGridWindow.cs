using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using com.hellion.tilesystem.utilities;
using com.hellion.packman;

namespace com.hellion.tilesystem.editor
{
    public class EditorTileGridWindow : EditorWindow
    {
        private SOGridData source;
        private string grid_asset = "Assets/com/hellion/tilegenerator/2d/prefabs/Tile Generator.prefab";
        private string grid_data_asset = "Assets/com/hellion/tilegenerator/ScriptableObjects/TileGridData.asset";
        private GameObject gridObject = null;
        private string Heading = "Select Or Generate Grid Data";
        Dictionary<ETileType, string> tileTextures = new Dictionary<ETileType, string>() { { ETileType.Wall_Blank, "Assets/com/hellion/packman/Sprites/Wall_00.png" },
        { ETileType.Wall_Top_Right, "Assets/com/hellion/packman/Sprites/Wall_01.png" }, { ETileType.Wall_Top_Left, "Assets/com/hellion/packman/Sprites/Wall_02.png" },
        { ETileType.Wall_Bottom_Right, "Assets/com/hellion/packman/Sprites/Wall_03.png" }, { ETileType.Wall_Bottom_Left, "Assets/com/hellion/packman/Sprites/Wall_04.png" },
        { ETileType.Wall_Right, "Assets/com/hellion/packman/Sprites/Wall_05.png" }, { ETileType.Wall_Left, "Assets/com/hellion/packman/Sprites/Wall_06.png" },
        { ETileType.Wall_Top, "Assets/com/hellion/packman/Sprites/Wall_07.png" }, { ETileType.Wall_Bottom, "Assets/com/hellion/packman/Sprites/Wall_08.png" },
        { ETileType.Wall_Bottom_Right_Line_Right, "Assets/com/hellion/packman/Sprites/Wall_09.png" }, { ETileType.Wall_Top_Right_Line_Right, "Assets/com/hellion/packman/Sprites/Wall_10.png" },
        { ETileType.Wall_Bottom_Left_Line_Left, "Assets/com/hellion/packman/Sprites/Wall_11.png" }, { ETileType.Wall_Top_Left_Line_Left, "Assets/com/hellion/packman/Sprites/Wall_12.png" },
        { ETileType.Wall_Top_Left_Line_Top, "Assets/com/hellion/packman/Sprites/Wall_13.png" }, { ETileType.Wall_Top_Right_Line_Top, "Assets/com/hellion/packman/Sprites/Wall_14.png" },
        { ETileType.Wall_Bottom_Left_Line_Bottom, "Assets/com/hellion/packman/Sprites/Wall_15.png" }, { ETileType.Wall_Bottom_Right_Line_Bottom, "Assets/com/hellion/packman/Sprites/Wall_16.png" },
        { ETileType.Wall_Bottom_Right_Line_Single, "Assets/com/hellion/packman/Sprites/Wall_17.png" }, { ETileType.Wall_Bottm_Left_Line_Single, "Assets/com/hellion/packman/Sprites/Wall_18.png" },
        { ETileType.Wall_Top_Right_Single_Line, "Assets/com/hellion/packman/Sprites/Wall_19.png" }, { ETileType.Wall_Top_Left_Line_Single, "Assets/com/hellion/packman/Sprites/Wall_20.png" },
        { ETileType.Wall_Bottom_Line_single, "Assets/com/hellion/packman/Sprites/Wall_21.png" },
        { ETileType.Wall_Top_Line_Single, "Assets/com/hellion/packman/Sprites/Wall_22.png" }, { ETileType.Wall_Left_Line_Single, "Assets/com/hellion/packman/Sprites/Wall_23.png" },
        { ETileType.Wall_Right_Line_Single, "Assets/com/hellion/packman/Sprites/Wall_24.png" }, { ETileType.Wall_Top_Right_Small, "Assets/com/hellion/packman/Sprites/Wall_25.png" },
        { ETileType.Wall_Top_Left_Small, "Assets/com/hellion/packman/Sprites/Wall_26.png" }, { ETileType.Wall_Bottom_Right_Small, "Assets/com/hellion/packman/Sprites/Wall_27.png" },
        { ETileType.Wall_Bottom_Left_Small, "Assets/com/hellion/packman/Sprites/Wall_28.png" }, { ETileType.Wall_Bootom_Right_Small_Middle, "Assets/com/hellion/packman/Sprites/Wall_29.png" },
        { ETileType.Wall_Top_Right_Small_Middle, "Assets/com/hellion/packman/Sprites/Wall_30.png" }, { ETileType.Wall_Bottom_Left_Small_Middle, "Assets/com/hellion/packman/Sprites/Wall_31.png" },
        { ETileType.Wall_Top_Left_Small_Middle, "Assets/com/hellion/packman/Sprites/Wall_32.png" }, { ETileType.Wall_Top_Right_Sharp, "Assets/com/hellion/packman/Sprites/Wall_33.png" },
        { ETileType.Wall_Top_Left_Sharp, "Assets/com/hellion/packman/Sprites/Wall_34.png" },{ ETileType.Wall_Bottom_Right_Sharp, "Assets/com/hellion/packman/Sprites/Wall_35.png" },
        { ETileType.Wall_Bottom_Left_Sharp, "Assets/com/hellion/packman/Sprites/Wall_36.png" },
        { ETileType.Wall_Bottom_Single_White, "Assets/com/hellion/packman/Sprites/Wall_37.png" }};

        [MenuItem("packman/TileGridWindow")]
        private static void ShowWindow()
        {
            var window = GetWindow<EditorTileGridWindow>();
            window.titleContent = new GUIContent("TileGridWindow");
            window.Show();
        }
        private void Awake()
        {
            Selection.selectionChanged = null;
            Selection.selectionChanged += OnSelectionChanged;
        }
        private List<TileObject> selectedTiles = new List<TileObject>();
        private void OnSelectionChanged()
        {
            ClearAllSelectedTiles();
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                TileObject tile = Selection.gameObjects[i].GetComponent<TileObject>();
                if (tile)
                {
                    selectedTiles.Add(tile);
                    tile.SetSelected(true);
                }
            }
        }
        void ClearAllSelectedTiles()
        {
            foreach (TileObject item in selectedTiles)
            {
                item.SetSelected(false);
            }
            selectedTiles.Clear();
        }

        private ETileType selectedTileType = ETileType.Wall_Blank;
        private EPelletType selectedPelletType = EPelletType.None;
        private void OnGUI()
        {
            GUILayout.BeginHorizontal(Heading, "window");
            if (source == null)
            {
                source = EditorGUILayout.ObjectField("Grid Data", source, typeof(SOGridData), true) as SOGridData;
                if (source != null)
                {
                    if (!AssetDatabase.OpenAsset(source))
                    {
                        source = null;
                    }
                    else
                    {
                        EditorUtility.SetDirty(source);
                    }
                    Heading = "Update Grid Data";
                }
                if (GUILayout.Button("Create Grid Data"))
                {
                    source = ScriptableObject.CreateInstance<SOGridData>();
                    source.tile_data = new List<SOGridDataSerializableList>();
                    for (int i = 0; i < source._height; i++)
                    {
                        source.tile_data.Add(new SOGridDataSerializableList());
                        for (int j = 0; j < source._width; j++)
                        {
                            source.tile_data[i].data.Add(new SOGridDataSerializable(ETileType.Wall_Blank, LoadAsset(tileTextures[ETileType.Wall_Blank])));
                        }
                    }
                    AssetDatabase.CreateAsset(source, grid_data_asset);
                    EditorUtility.SetDirty(source);
                    AssetDatabase.SaveAssets();
                    Heading = "Update Grid Data";
                }
            }
            else if (source != null)
            {
                GUILayout.BeginVertical();
                EditorGUI.BeginDisabledGroup(true);
                source = EditorGUILayout.ObjectField("Grid Data", source, typeof(SOGridData), true) as SOGridData;
                EditorGUI.EndDisabledGroup();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Back"))
                {
                    source = null;
                }
                if (GUILayout.Button("Save"))
                {
                    EditorUtility.SetDirty(source);
                    AssetDatabase.SaveAssets();
                }
                if (GUILayout.Button("Debug"))
                {
                    if (gridObject != null)
                    {
                        DestroyGrid();
                    }
                    else
                    {
                        CreateGrid();
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                selectedTileType = (ETileType)EditorGUILayout.EnumPopup("Tile Type", selectedTileType);
                if (GUILayout.Button("Change Tile"))
                {
                    if (selectedTiles.Count > 0)
                    {

                        EditorUtility.SetDirty(source);
                        foreach (TileObject item in selectedTiles)
                        {
                            item.UpdateTileObject(selectedTileType, LoadAsset(tileTextures[selectedTileType]));
                            source.tile_data[item.GetIndex().y].data[item.GetIndex().x].SetTileType(selectedTileType, LoadAsset(tileTextures[selectedTileType]));
                        }
                        AssetDatabase.Refresh();
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                selectedPelletType = (EPelletType)EditorGUILayout.EnumPopup("Pellet Type", selectedPelletType);
                if (GUILayout.Button("Change Pellet Type"))
                {
                    if (selectedTiles.Count > 0)
                    {
                        EditorUtility.SetDirty(source);
                        foreach (TileObject item in selectedTiles)
                        {
                            item.UpdatePellet(selectedPelletType, GetPelletAsset(selectedPelletType));
                            source.tile_data[item.GetIndex().y].data[item.GetIndex().x].SetPellet(selectedPelletType, GetPelletAsset(selectedPelletType));
                        }
                        AssetDatabase.Refresh();
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }

        private Sprite GetPelletAsset(EPelletType type)
        {
            switch (type)
            {
                case EPelletType.None:
                    return null;
                case EPelletType.Pellet:
                    return LoadAsset("Assets/com/hellion/packman/Sprites/Pellet_Small.png");
                case EPelletType.Energizer:
                    return LoadAsset("Assets/com/hellion/packman/Sprites/Pellet_Large.png");
                default:
                    return null;
            }
        }

        private Sprite LoadAsset(string path)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        private void CreateGrid()
        {
            gridObject = Instantiate(PrefabUtility.LoadPrefabContents(grid_asset) as GameObject);
            gridObject.GetComponent<TileGenerator>().GenerateGrid(source);
        }

        private void OnDestroy()
        {
            if (source != null)
            {
                source = null;
            }
            if (gridObject != null)
            {
                DestroyGrid();
            }
        }

        private void DestroyGrid()
        {
            ClearAllSelectedTiles();
            Heading = "Select Or Generate Grid Data";
            if (gridObject != null)
            {
                DestroyImmediate(gridObject);
                gridObject = null;
            }
        }
    }
}
