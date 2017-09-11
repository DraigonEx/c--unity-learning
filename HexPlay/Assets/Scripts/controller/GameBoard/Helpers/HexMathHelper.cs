using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using thelab.mvc;

public class HexMathHelper : Controller<TileApp> {

    public static readonly Tile[] HEX_DIRECTIONS = {
        new Tile(1, 0, -1), new Tile(1, -1, 0), new Tile(0, -1, 1),
        new Tile(-1, 0, 1), new Tile(-1, 1, 0), new Tile(0, 1, -1) };

    // Math functions for Hexes

    public static float Distance(Tile hexA, Tile hexB)
    {
        return Mathf.Max(Mathf.Abs(hexA.X - hexB.X), Mathf.Abs(hexA.Y - hexB.Y), Mathf.Abs(hexA.Z - hexB.Z));
    }

    public static Tile HexAdd(Tile hexA, Tile hexB)
    {
        return new Tile(hexA.X + hexB.X, hexA.Y + hexB.Y);
    }

    public static Tile HexSubtract(Tile hexA, Tile hexB)
    {
        return new Tile(hexA.X - hexB.X, hexA.Y - hexB.Y);
    }

    public static Tile HexMultiply(Tile hexA, int multiplier)
    {
        return new Tile(hexA.X * multiplier, hexA.Y * multiplier);
    }

    public static int HexLength(Tile hex)
    {
        return ((Mathf.Abs(hex.X) + Mathf.Abs(hex.Y) + Mathf.Abs(hex.Z)) / 2);
    }

    public static int HexDistance(Tile hexA, Tile hexB)
    {
        return HexLength(HexSubtract(hexA, hexB));
    }

    public static Tile HexDirection(int direction /* 0 to 5 */)
    {
        if (direction >= 0 && direction < 6)
            return HEX_DIRECTIONS[direction];

        Debug.LogError("There are only 6 valid Hex directions: 0-5. I recieved " + direction + ".");
        return null;
    }

    public static Tile HexNeighbor(Tile hex, int direction)
    {

        return HexAdd(hex, HexDirection(direction));
    }

    public static Vector3 HexRound(Vector3 hex)
    {
        int q = (int)(Mathf.Round(hex.x));
        int r = (int)(Mathf.Round(hex.y));
        int s = (int)(Mathf.Round(hex.z));
        double q_diff = Mathf.Abs(q - hex.x);
        double r_diff = Mathf.Abs(r - hex.y);
        double s_diff = Mathf.Abs(s - hex.z);
        if (q_diff > r_diff && q_diff > s_diff)
        {
            q = -r - s;
        }
        else if (r_diff > s_diff)
        {
            r = -q - s;
        }
        else
        {
            s = -q - r;
        }
        return new Vector3(q, r, s);
    }

    public static float Lerp(float a, float b, float t)
    {
        return a * (1 - t) + b * t;
        /* better for floating point precision than
           a + (b - a) * t, which is what I usually write */
    }

    public static Vector3 HexLerp(Tile a, Tile b, float t)
    {
        Vector3 fh = new Vector3(
            Lerp(a.X, b.X, t),
            Lerp(a.Y, b.Y, t),
            Lerp(a.Z, b.Z, t));
        return fh;
    }
}
