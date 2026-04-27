using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour
{
    private Coroutine shootCoroutine;

    public void StartShooting(float speed, float fireInterval, GameObject projectilePrefab)
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }

        shootCoroutine = StartCoroutine(ShootRoutine(speed, fireInterval, projectilePrefab));
    }

    public void StopShooting()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    private IEnumerator ShootRoutine(float speed, float fireInterval, GameObject projectilePrefab)
    {
        while (true)
        {
            Vector3 shootDir = PlayerController.FocalPoint.transform.forward;

            float[] angles = { -30f, 0f, 30f };

            foreach (float angle in angles)
            {
                Vector3 rotatedDir = Quaternion.AngleAxis(angle, Vector3.up) * shootDir;
                float angleZ = Mathf.Atan2(rotatedDir.x, rotatedDir.z) * Mathf.Rad2Deg;
                Quaternion shootRotation = Quaternion.Euler(90f, 0f, -angleZ);

                // Vector3 offSet = Quaternion.AngleAxis(angle, Vector3.up) * PlayerController.FocalPoint.transform.right;
                Vector3 spawnPos = transform.position + shootDir;

                GameObject proj = Instantiate(projectilePrefab, spawnPos, shootRotation);
                Rigidbody projRb = proj.GetComponent<Rigidbody>();
                projRb.constraints = RigidbodyConstraints.FreezeRotation;
                projRb.AddForce(shootDir * speed, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(fireInterval);
        }
    }
}
