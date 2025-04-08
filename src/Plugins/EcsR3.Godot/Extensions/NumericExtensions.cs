using GV2 = Godot.Vector2;
using GV3 = Godot.Vector3;
using GQ = Godot.Quaternion;
using MV2 = System.Numerics.Vector2;
using MV3 = System.Numerics.Vector3;
using MQ = System.Numerics.Quaternion;

namespace EcsR3.Godot.Plugins.EcsR3.Godot.Extensions;

public static class NumericExtensions
{
    public static MV2 ToNumeric(this GV2 vector) => new MV2(vector.X, vector.Y);
    public static MV3 ToNumeric(this GV3 vector) => new MV3(vector.X, vector.Y, vector.Z);
    public static MQ ToNumeric(this GQ quaternion) => new MQ(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    
    public static GV2 ToGodot(this MV2 vector) => new GV2(vector.X, vector.Y);
    public static GV3 ToGodot(this MV3 vector) => new GV3(vector.X, vector.Y, vector.Z);
    public static GQ ToGodot(this MQ quaternion) => new GQ(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
}