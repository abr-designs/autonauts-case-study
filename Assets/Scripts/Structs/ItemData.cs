using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemData : IEquatable<ItemData>
{
    public string Name;
    public int Space;

    public bool Equals(ItemData other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object obj)
    {
        return obj is ItemData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (Name != null ? Name.GetHashCode() : 0);
    }
}
