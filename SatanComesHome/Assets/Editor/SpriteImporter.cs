using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteImporter : AssetPostprocessor
{
    void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
    {
        TextureImporter importer = assetImporter as TextureImporter;

        // only change sprite import settings on first import, so we can change those settings for individual files

        string name = importer.assetPath.ToLower();
        if (File.Exists(AssetDatabase.GetTextMetaFilePathFromAssetPath(name)))
        {
            return;
        }

        // adjust values for pixel art

        importer.spritePixelsPerUnit = 1;
        importer.mipmapEnabled = false;
        importer.filterMode = FilterMode.Point;
        importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
        importer.compressionQuality = 0;
        importer.textureCompression = TextureImporterCompression.Uncompressed;

        // access the TextureImporterSettings to change the spriteAlignment value

        TextureImporterSettings textureSettings = new TextureImporterSettings();
        importer.ReadTextureSettings(textureSettings);

        textureSettings.spritePivot = new Vector2(0.5f, 0f);
        textureSettings.spriteAlignment = (int)SpriteAlignment.BottomCenter;

        importer.SetTextureSettings(textureSettings);

        importer.SaveAndReimport();

        Debug.Log("Sprites: " + sprites[0].name);
    }
}
