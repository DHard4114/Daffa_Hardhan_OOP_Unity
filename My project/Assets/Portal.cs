using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    private Vector2 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Weapon weapon = player.GetComponentInChildren<Weapon>();
            if (weapon != null && weapon.gameObject.activeSelf)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private void ChangePosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        newPosition = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }
}
