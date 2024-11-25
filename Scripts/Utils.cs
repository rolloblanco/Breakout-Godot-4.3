using Godot;
using System;
using Godot.Collections;

public class Utils
{
    public static string ArrayToString<[MustBeVariant] T>(Array<T> array)
    {
        string result = "[";
        for (int i = 0; i < array.Count; i++)
        {
            result += array[i]?.ToString();
            if (i < array.Count - 1)
                result += ", "; // Add a comma and space between items
        }
        result += "]";
        return result;
    }

    public static Array<Dictionary<string, float>> ViewportSplicer(float viewportWidth, float viewportHeight, float blockWidth, float blockHeight)
    {
        Array<Dictionary<string, float>> viewportBlocks = new Array<Dictionary<string, float>>();
        float widthRatio = viewportWidth / blockWidth;
        float heightRatio = viewportHeight / blockHeight;


        int blockY = 0;
        int blockX = 0;
        // for each location
        for (int blockLocation = 0; blockLocation < heightRatio * widthRatio; blockLocation++)
        {
            viewportBlocks.Add(new Dictionary<string, float>());
            // % widthRatio to wrap around, starting from 0 and ending at widthRatio
            viewportBlocks[blockLocation].Add("X", (blockX % widthRatio) * blockWidth + blockWidth/2); // add width/2 to prevent blocks appearing offscreen
            viewportBlocks[blockLocation].Add("Y", blockY * blockHeight + blockHeight/2); // add height/2 to prevent blocks appearing offscreen
            
            //GD.Print($"I have created a block at ({viewportBlocks[blockLocation]["X"]},{viewportBlocks[blockLocation]["Y"]})");

            blockX++;
            if (blockX % widthRatio == 0)
                blockY++;
        }
        
        return viewportBlocks;
    }
}
