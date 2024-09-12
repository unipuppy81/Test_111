using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Move Horizontal")]
    [SerializeField] private Camera mainCamera;
    private Vector3 initialMousePos;
    private float detectionDistance = 0.5f; 
    private float minX = -2f;
    private float maxX = 2f;
    private float speed = 3f;
    private bool isDrag = false;

    [Header("Animation")]
    [SerializeField] private Animator anim;

    [Header("NormalAttack")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform pos;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float curTime;
    public float coolTime = .5f;
    public int attackCount = 0;
    public int swordLevel = 0;
    public bool doubleAttack = false;


    [Header("Stats")]
    public float damage = 0f;
    public float curHp;
    public float maxHp = 300;
    public int gold;

    private void Start()
    {
        anim.SetBool("isGameDone", false);
    }

    private void Update()
    {
        HandleMouseInput();
        MoveHorizontal();
        NormalAttack();
    }

    #region 체력 & 이동

    public void SetHealth(float amount)
    {
        curHp += amount;

        if (curHp >= maxHp)
            curHp = maxHp;
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePos = Input.mousePosition;
            isDrag = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }
    }

    private void MoveHorizontal()
    {
        if (isDrag)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - initialMousePos;

            float moveX = mouseDelta.x * speed * Time.deltaTime;

            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, detectionDistance, enemyLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, detectionDistance, enemyLayer);

            Vector3 newPosition = transform.position;
            if (moveX < 0 && hitLeft.collider == null)
                newPosition.x += moveX;
            else if (moveX > 0 && hitRight.collider == null)
                newPosition.x += moveX;


            transform.position = new Vector3(Mathf.Clamp(newPosition.x, minX, maxX), transform.position.y, transform.position.z);
            initialMousePos = currentMousePosition;
        }
    }
    #endregion


    #region 기본 공격
    private void NormalAttack()
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2D)
        {
            if (curTime <= 0)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Enemy e = collider.GetComponent<Enemy>();
                    e.OnHit(damage);

                    GameManager.Instance.isCrush = true;
                    anim.SetTrigger("isAtk");
                    attackCount++;
                    curTime = coolTime;


                    if (swordLevel >= 7 && attackCount == 3)
                    {
                        doubleAttack = true;
                    }

                    if (swordLevel >= 5 && attackCount >= 3)
                    {
                        curHp += 5;
                        attackCount = 0;
                    }


                    if (doubleAttack)
                        StartCoroutine(PerformDoubleAttack());
                }
                else
                {
                    GameManager.Instance.isCrush = false;
                }
            }

        }

        curTime -= Time.deltaTime;

    }

    private IEnumerator PerformDoubleAttack()
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2D)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy e = collider.GetComponent<Enemy>();
                e.OnHit(damage);
                anim.SetTrigger("isAtk");
                yield return new WaitForSeconds(0.1f);
            }
        }
        doubleAttack = false;
    }
    #endregion


    #region 디버깅
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    #endregion
}
