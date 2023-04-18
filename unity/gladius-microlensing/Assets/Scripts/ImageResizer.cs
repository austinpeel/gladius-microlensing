using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEditor;

public static class ImageResizer
{
    // // Resize the input texture and return the resized texture
    // public static Texture2D Resize(Texture2D inputTexture, int maxDimension = 128, bool useCompression = true)
    // {
    //     // Get the input texture dimensions
    //     int inputWidth = inputTexture.width;
    //     int inputHeight = inputTexture.height;

    //     // If the input texture is already the correct size, return it as-is
    //     if (inputWidth <= maxDimension && inputHeight <= maxDimension)
    //     {
    //         return inputTexture;
    //     }

    //     // Calculate the new texture dimensions based on the maximum dimension
    //     int newWidth, newHeight;
    //     if (inputWidth > inputHeight)
    //     {
    //         newWidth = maxDimension;
    //         newHeight = Mathf.RoundToInt((float)inputHeight / inputWidth * maxDimension);
    //     }
    //     else
    //     {
    //         newWidth = Mathf.RoundToInt((float)inputWidth / inputHeight * maxDimension);
    //         newHeight = maxDimension;
    //     }

    //     // Reduce the input texture size using TextureImporter
    //     if (inputWidth > maxDimension || inputHeight > maxDimension)
    //     {
    //         string tempPath = Path.Combine(Application.temporaryCachePath, inputTexture.name);
    //         File.WriteAllBytes(tempPath, inputTexture.EncodeToPNG());

    //         AssetDatabase.ImportAsset(tempPath);
    //         TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(tempPath);
    //         textureImporter.maxTextureSize = Mathf.Max(inputWidth, inputHeight);
    //         textureImporter.textureCompression = useCompression ? TextureImporterCompression.Compressed : TextureImporterCompression.Uncompressed;
    //         textureImporter.SaveAndReimport();

    //         inputTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(tempPath);
    //     }

    //     // Resize the texture
    //     Texture2D outputTexture = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);
    //     Color[] outputPixels = outputTexture.GetPixels();

    //     int threadCount = Mathf.Max(System.Environment.ProcessorCount / 2, 1);
    //     int rowsPerThread = newHeight / threadCount;
    //     List<Thread> threads = new List<Thread>();

    //     for (int i = 0; i < threadCount; i++)
    //     {
    //         int startY = i * rowsPerThread;
    //         int endY = i == threadCount - 1 ? newHeight : startY + rowsPerThread;

    //         Thread thread = new Thread(() =>
    //         {
    //             for (int y = startY; y < endY; y++)
    //             {
    //                 for (int x = 0; x < newWidth; x++)
    //                 {
    //                     float u = (float)x / (newWidth - 1);
    //                     float v = (float)y / (newHeight - 1);

    //                     outputPixels[y * newWidth + x] = inputTexture.GetPixelBilinear(u, v);
    //                 }
    //             }
    //         });

    //         thread.Start();
    //         threads.Add(thread);
    //     }

    //     foreach (Thread thread in threads)
    //     {
    //         thread.Join();
    //     }

    //     outputTexture.SetPixels(outputPixels);
    //     outputTexture.Apply();

    //     return outputTexture;
    // }
}
