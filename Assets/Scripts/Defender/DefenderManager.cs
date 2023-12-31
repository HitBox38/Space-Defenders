using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderManager : MonoBehaviour
{

    public int DefenderHealth { get; private set; } = 100;

    public int GetMaxHealth => maxHealth;
    [Header("Defender Specs")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float rotationSpeed = 450;
    [SerializeField] private float reAimTime = 2;
    [SerializeField] private Transform canon;
    [Header("Defender Fire")]
    [SerializeField] private int fireRate = 1;
    [SerializeField] private float timeBetweenShots = 1;
    [SerializeField] private float timeBetweenFireRateShots = .1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 2;
    [SerializeField] private float projectileLifespan = 2;
    [Header("Player Finding")]
    [SerializeField] private LayerMask playerShipsLayers;
    [SerializeField] private float playerShipDetectorRadius = 10;

    private Vector2 projectileDirection = Vector2.down;
    private float defenderAngle = 0;
    private bool playerSeen = false;

    // Start is called before the first frame update
    void Start()
    {
        DefenderHealth = maxHealth;
        StartCoroutine(DefenderShooting());
        StartCoroutine(ChooseWhereToAim());
    }

    private void Update()
    {
        canon.rotation = Quaternion.Lerp(canon.rotation, Quaternion.Euler(0, 0, defenderAngle - 180), rotationSpeed * Time.deltaTime);

        if (DefenderHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ChooseWhereToAim()
    {
        Collider2D[] playerShips = Physics2D.OverlapCircleAll(transform.position,
            playerShipDetectorRadius, playerShipsLayers);
        if (playerShips.Length > 0)
        {
            playerSeen = true;
            Transform shipToFollow = FindClosestShip(playerShips).transform;
            projectileDirection = (shipToFollow.position - transform.position).normalized;
        }
        else
        {
            playerSeen = false;
            projectileDirection = Vector2.down;
        }
        defenderAngle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg - 90;
        yield return new WaitForSeconds(reAimTime);
        StartCoroutine(ChooseWhereToAim());
    }

    private Collider2D FindClosestShip(Collider2D[] ships)
    {
        Collider2D closestShip = null;
        foreach (Collider2D ship in ships)
        {
            if (closestShip == null)
            {
                closestShip = ship;
            }
            else
            {
                if (Vector2.Distance(transform.position, closestShip.transform.position) >
                    Vector2.Distance(transform.position, ship.transform.position))
                {
                    closestShip = ship;
                }
            }
        }
        return closestShip;
    }

    private IEnumerator DefenderShooting()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        if (playerSeen)
        {
            for (int i = 0; i < fireRate && playerSeen; i++)
            {
                yield return new WaitForSeconds(timeBetweenFireRateShots);
                GameObject projectile = Instantiate(projectilePrefab, (Vector2)transform.position + projectileDirection, Quaternion.identity);
                projectile.transform.eulerAngles = new Vector3(0, 0, defenderAngle);
                projectile.GetComponent<Rigidbody2D>().AddForce(projectileDirection * projectileSpeed);
                Destroy(projectile, projectileLifespan);
            }
        }
        StartCoroutine(DefenderShooting());
    }

    public void DamageDefender(int damage)
    {
        DefenderHealth -= damage;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "UnitProjectile")
        {
            int str = coll.gameObject.GetComponent<ProjectileManager>().GetStrength();
            Debug.Log("Shot: " + str);
            DamageDefender(str);
        }
        else if (coll.tag == "Unit")
        {
            int str = (int)coll.gameObject.GetComponent<UnitStatManager>().GetKamikaze();
            Debug.Log("Kamakazie: " + str);
            DamageDefender(str);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, playerShipDetectorRadius);
    }
#endif
}
