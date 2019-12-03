using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAudioManager
{
    static bool isWalking;
    static bool isJumping;
    static bool isInAir;
    static bool isSucking;
    static bool isHit;

    public static bool GetisWalking()
    {
        return isWalking;
    }

    public static void Setiswalking(bool newWalking)
    {
        isWalking = newWalking;
    }

    public static bool GetisJumping()
    {
        return isJumping;
    }

    public static void SetisJumping(bool newJumping)
    {
        isJumping = newJumping;
    }

    public static bool GetisInAir()
    {
        return isInAir;
    }

    public static void SetisInAir(bool newInAir)
    {
        isInAir = newInAir;
    }

    public static bool GetisSucking()
    {
        return isSucking;
    }

    public static void SetisSucking(bool newSucking)
    {
        isSucking = newSucking;
    }

    public static bool GetisHit()
    {
        return isHit;
    }

    public static void SetisHit(bool newHit)
    {
        isHit = newHit;
    }
}