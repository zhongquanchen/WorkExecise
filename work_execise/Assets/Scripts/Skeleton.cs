using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;
using System;

public class Skeleton : MonoBehaviour
{
    public Animator avatar;
    public BVHFile bvh;
    private int current_frame = 0;

    [Title("file path")]
    public string path;

    private void Start()
    {
        bvh = new BVHFile(File.ReadAllText(path));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) RefreshFrame();
    }

    public void RefreshFrame()
    {
        foreach (var bone in bvh.boneList) UpdateBone(bone);
    }

    public void UpdateBone(BVHFile.BVHBone bone)
    {
        // return if bone name does not match any
        if (!bone.corresBone.enabled) return;
        bool isRoot = bone.corresBone.bone == HumanBodyBones.Hips;
        bool isFirstFrame = current_frame == 0;

        foreach(var channelId in bone.channelOrder)
        {
            float values = bone.channels[channelId].values[current_frame++];
            //if (isRoot && isFirstFrame) normalizeFirstFrameRootPosition(channelId, values);

            // channelId < 3 perform rotation;
            if (channelId < 3) MoveBone(bone.corresBone.bone, values, channelId);
            else RotateBone(bone.corresBone.bone, values, channelId);
        }
    }

    #region Translate bones
    public void MoveBone(HumanBodyBones bone, float value, int axis)
    {
        Debug.Log($"moving bone {bone}");
        var avatarBone = avatar.GetBoneTransform(bone);
        var direction = Constants.MatchDirection(axis);
        switch(direction)
        {
            case "x":
                avatarBone.localPosition += new Vector3(value, 0, 0);
                Debug.Log("adding x axis");
                return;
            case "y":
                avatarBone.localPosition += new Vector3(0, value, 0);
                Debug.Log("adding y axis");
                return;
            case "z":
                avatarBone.localPosition += new Vector3(0, 0, value);
                Debug.Log("adding z axis");
                return;
            default:
                return;
        }
    }
    #endregion


    #region Rotate bones
    // fixme: - for now we use euler angle to rotate each bone,
    // fixme: - it will change to quantion later
    public void RotateBone(HumanBodyBones bone, float value, int axis)
    {
        var avatarBone = avatar.GetBoneTransform(bone);
        var direction = Constants.MatchDirection(axis);
        switch (direction)
        {
            case "x":
                avatarBone.localEulerAngles += new Vector3(value, 0, 0);
                return;
            case "y":
                avatarBone.localEulerAngles += new Vector3(0, value, 0);
                return;
            case "z":
                avatarBone.localEulerAngles += new Vector3(0, 0, value);
                return;
            default:
                return;
        }
    }
    #endregion


    #region utilities
    private float[] normalized = new float[6] { 0, 0, 0, 0, 0, 0 };
    private void normalizeFirstFrameRootPosition(int channelId, float values) => normalized[channelId] = values;
    #endregion
}
