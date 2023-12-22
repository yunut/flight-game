using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;


    // Update is called once per frame
    void Update()
    {
        // 키보드 이동
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // //        float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);

        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // 마우스 이동
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 포지션을 개발자가 보는 position으로 바꿔줌
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (!GameManager.instance.isGameOver)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 현재 게임에서 흐른 시간
        if (Time.time - lastShotTime > shootInterval)
        {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "boss")
        {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade()
    {
        if (weaponIndex < weapons.Length)
        {
            weaponIndex++;
        }
    }
}
