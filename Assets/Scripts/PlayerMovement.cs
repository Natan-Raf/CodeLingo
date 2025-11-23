using System;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ПЕРЕМЕННЫЕ, НАСТРАИВАЕМЫЕ В ИНСПЕКТОРЕ
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    // ПРИВАТНЫЕ ССЫЛКИ НА КОМПОНЕНТЫ
    private Rigidbody2D rb;
    private Animator anim; // <-- Для управления анимациями
    private UnityEngine.Vector2 movement;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // <-- Получаем ссылку на Animator
    }

    void Update()
    {
        // 1. Ввод
        movement.x = Input.GetAxisRaw("Horizontal");
        UnityEngine.Debug.Log("Horizontal Input: " + movement.x); // <-- ИСПРАВЛЕНИЕ
        movement.Normalize();

        // 2. Зеркалирование (поворот спрайта)
        if (movement.x > 0)
        {
            transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new UnityEngine.Vector3(-1f, 1f, 1f);
        }

        // 3. Проверка земли
        CheckIfGrounded();

        // 4. Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 5. Установка параметра анимации (для Idle/Run)
        // Mathf.Abs() гарантирует, что мы используем положительное значение скорости (для логики > 0.01)
        anim.SetFloat("Speed", Mathf.Abs(movement.x));
    }

    void FixedUpdate()
    {
        // Применяем горизонтальную скорость, сохраняя вертикальную (гравитацию/прыжок)
        rb.linearVelocity = new UnityEngine.Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }

    private void CheckIfGrounded()
    {
        // Проверяем землю в точке GroundCheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}