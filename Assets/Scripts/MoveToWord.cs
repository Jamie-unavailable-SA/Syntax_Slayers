using UnityEngine;

public class MoveToWord : MonoBehaviour
{
    public GameObject target;

    private void LateUpdate()
    {
        target = GameObject.FindGameObjectWithTag("word");
    }

    private void Update()
    {
        Vector3 position = new Vector3(target.transform.position.x, target.transform.position.y + -0.05f, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, position, 12 * Time.deltaTime);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("word"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
