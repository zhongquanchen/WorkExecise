using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Constants
{
    private static Dictionary<string, HumanBodyBones> boneNames = new Dictionary<string, HumanBodyBones>()
    {
        // spine
        {"Hips", HumanBodyBones.Hips },
        {"LowerBack", HumanBodyBones.Spine },
        {"Spine", HumanBodyBones.Chest },
        {"Spine1", HumanBodyBones.UpperChest },

        // left arm 
        {"LeftShoulder", HumanBodyBones.LeftShoulder },
        {"LeftArm", HumanBodyBones.LeftUpperArm },
        {"LeftForeArm", HumanBodyBones.LeftLowerArm },
        {"LeftHand", HumanBodyBones.LeftHand },

        // right arm
        {"RightShoulder", HumanBodyBones.RightShoulder },
        {"RightArm", HumanBodyBones.RightUpperArm },
        {"RightForeArm", HumanBodyBones.RightLowerArm },
        {"RightHand", HumanBodyBones.RightHand },

        // left leg
        {"LeftUpLeg", HumanBodyBones.LeftUpperLeg },
        {"LeftLeg", HumanBodyBones.LeftLowerLeg },
        {"LeftFoot", HumanBodyBones.LeftFoot },
        {"LeftToeBase", HumanBodyBones.LeftToes },

        // right leg
        {"RightUpLeg", HumanBodyBones.RightUpperLeg },
        {"RightLeg", HumanBodyBones.RightLowerLeg },
        {"RightFoot", HumanBodyBones.RightFoot },
        {"RightToeBase", HumanBodyBones.RightToes }
    };
    public static void MatchBoneNames(string name, ref BVHFile.BVHBone.CorrespondingBone bone)
    {
        if (boneNames.ContainsKey(name))
        {
            bone.bone = boneNames[name];
            bone.enabled = true;
            return;
        }
        Debug.Log($"bone not exist {name}");
    }

    private static Dictionary<int, string> direction = new Dictionary<int, string>()
    {
        // 0 = Xpos, 1 = Ypos, 2 = Zpos, 3 = Xrot, 4 = Yrot, 5 = Zrot
        {0, "x" },
        {1, "y" },
        {2, "z" },
        {3, "x" },
        {4, "y" },
        {5, "z" }
    };
    public static string MatchDirection(int dir)
    {
        return direction[dir];
    }
}
 