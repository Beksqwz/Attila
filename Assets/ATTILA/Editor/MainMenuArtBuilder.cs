using ATTILA.UI;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace ATTILA.Editor
{
    public static class MainMenuArtBuilder
    {
        private const string BackgroundPath = "Assets/ATTILA/Art/Menu/Background/TemporarySteppeSunset.png";
        private const string LogoPath = "Assets/ATTILA/Art/Menu/Logo/TemporaryAttilaPixelLogo.png";
        private const string CharacterPath = "Assets/ATTILA/Art/Menu/Character/TemporaryBatyr.png";
        private const string KhanPath = "Assets/ATTILA/Art/Characters/Menu/TemporaryKhan.png";
        private const string DefinitionPath = "Assets/ATTILA/Resources/UI/MainMenuVisuals.asset";
        private const string MaterialPath = "Assets/ATTILA/Art/Menu/UI/TemporaryBatyrBlackKey.mat";

        [MenuItem("ATTILA/Build Main Menu Visual Assets")]
        public static void Build()
        {
            GenerateLogo();
            ConfigureSprite(BackgroundPath, 1);
            ConfigureSprite(LogoPath, 1);
            ConfigureSprite(CharacterPath, 1);
            ConfigureSprite(KhanPath, 1);
            AssetDatabase.ImportAsset(BackgroundPath, ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.ImportAsset(LogoPath, ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.ImportAsset(CharacterPath, ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.ImportAsset(KhanPath, ImportAssetOptions.ForceSynchronousImport);

            var definition = AssetDatabase.LoadAssetAtPath<MenuVisualDefinition>(DefinitionPath);
            if (definition == null)
            {
                definition = ScriptableObject.CreateInstance<MenuVisualDefinition>();
                AssetDatabase.CreateAsset(definition, DefinitionPath);
            }

            var material = AssetDatabase.LoadAssetAtPath<Material>(MaterialPath);
            if (material == null)
            {
                material = new Material(Shader.Find("ATTILA/Menu Black Key"));
                AssetDatabase.CreateAsset(material, MaterialPath);
            }

            definition.background = AssetDatabase.LoadAssetAtPath<Sprite>(BackgroundPath);
            definition.logo = AssetDatabase.LoadAssetAtPath<Sprite>(LogoPath);
            definition.character = AssetDatabase.LoadAssetAtPath<Sprite>(CharacterPath);
            definition.khan = AssetDatabase.LoadAssetAtPath<Sprite>(KhanPath);
            definition.characterMaterial = material;
            EditorUtility.SetDirty(definition);
            AssetDatabase.SaveAssets();
            Debug.Log("ATTILA Main Menu visual assets are ready.");
        }

        private static void ConfigureSprite(string path, float pixelsPerUnit)
        {
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null) return;
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.spritePixelsPerUnit = pixelsPerUnit;
            importer.spritePivot = new Vector2(.5f, .5f);
            importer.filterMode = FilterMode.Point;
            importer.mipmapEnabled = false;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.alphaIsTransparency = true;
            importer.SaveAndReimport();
        }

        private static void GenerateLogo()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(LogoPath));
            const int unit = 12, glyphWidth = 5, glyphHeight = 7, gap = 1;
            var letters = new[] { "01110|10001|10001|11111|10001|10001|10001", "11111|00100|00100|00100|00100|00100|00100", "11111|00100|00100|00100|00100|00100|00100", "10000|10000|10000|10000|10000|10000|11111", "11111|00100|00100|00100|00100|00100|11111", "01110|10001|10001|10001|10001|10001|10001" };
            var width = (letters.Length * glyphWidth + (letters.Length - 1) * gap) * unit + 24;
            var height = glyphHeight * unit + 28;
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            for (var x = 0; x < width; x++) for (var y = 0; y < height; y++) texture.SetPixel(x, y, Color.clear);
            for (var index = 0; index < letters.Length; index++)
            {
                var rows = letters[index].Split('|');
                for (var y = 0; y < glyphHeight; y++) for (var x = 0; x < glyphWidth; x++) if (rows[y][x] == '1')
                {
                    var px = 12 + (index * (glyphWidth + gap) + x) * unit;
                    var py = 12 + (glyphHeight - 1 - y) * unit;
                    Paint(texture, px + 4, py - 4, unit, unit, new Color(.208f, .09f, .075f, 1));
                    Paint(texture, px - 2, py - 2, unit + 4, unit + 4, new Color(.427f, .114f, .09f, 1));
                    Paint(texture, px, py, unit, unit, new Color(.851f, .604f, .333f, 1));
                    Paint(texture, px, py + unit - 3, unit, 3, new Color(.969f, .945f, .89f, 1));
                }
            }
            File.WriteAllBytes(LogoPath, texture.EncodeToPNG());
            Object.DestroyImmediate(texture);
        }

        private static void Paint(Texture2D texture, int left, int bottom, int width, int height, Color color)
        { for (var x = Mathf.Max(0,left); x < Mathf.Min(texture.width,left+width); x++) for (var y = Mathf.Max(0,bottom); y < Mathf.Min(texture.height,bottom+height); y++) texture.SetPixel(x,y,color); }
    }
}
