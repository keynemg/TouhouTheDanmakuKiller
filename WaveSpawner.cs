using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject Enemy_Prefab;
    public int waves_Count;
    public float waves_Timer;
    public int enemy_Count;
    public float enemy_Speed;
    public float angle;
    public float radius;
    public float rotation;
    public float spawnRotationStep;
    public float rotationOffset;
    public float projectileRotationStep;

    public GameObject Enemy_Prefab_B;
    public int waves_Count_B;
    public float waves_Timer_B;
    public int enemy_Count_B;
    public float enemy_Speed_B;
    public float angle_B;
    public float radius_B;
    public float rotation_B;
    public float spawnRotationStep_B;
    public float rotationOffset_B;
    public float spawnRotationAfterShot_B;

    public GameObject Enemy_Prefab_C;
    public int waves_Count_C;
    public float waves_Timer_C;
    public int enemy_Count_C;
    public float enemy_Speed_C;
    public float angle_C;
    public float radius_C;
    public float rotation_C;
    public float spawnRotationStep_C;
    public float rotationOffset_C;
    public float spawnRotationAfterShot_C;

    void Start()
    {
        StopAllCoroutines();
        StartCoroutine(Waves());
    }

    IEnumerator Waves()
    {
        for (int k = 0; k < 3; k++)
        {
            for (int i = 0; i < waves_Count; i++)
            {
                yield return new WaitForSecondsRealtime(waves_Timer);
                NewRadial.RadialV2(transform.position, Enemy_Prefab, enemy_Count, angle, radius, rotation, rotationOffset, transform, enemy_Speed);
                rotationOffset += projectileRotationStep;
                rotation += spawnRotationStep;
            }

            yield return new WaitForSecondsRealtime(1f);

            for (int i = 0; i < waves_Count; i++)
            {
                yield return new WaitForSecondsRealtime(waves_Timer_B);
                NewRadial.RadialV2(transform.position, Enemy_Prefab_B, enemy_Count_B, angle_B, radius_B, rotation_B, rotationOffset_B, transform, enemy_Speed_B);
                rotationOffset_B += spawnRotationAfterShot_B;
                rotation_B += spawnRotationStep_B;
            }

            yield return new WaitForSecondsRealtime(0.5f);

            for (int i = 0; i < waves_Count; i++)
            {
                yield return new WaitForSecondsRealtime(waves_Timer_C);
                NewRadial.RadialV2(transform.position, Enemy_Prefab_C, enemy_Count_C, angle_C, radius_C, rotation_C, rotationOffset_C, transform, enemy_Speed_C);
                rotationOffset_C += spawnRotationAfterShot_C;
                rotation_C += spawnRotationStep_C;
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
