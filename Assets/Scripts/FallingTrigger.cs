using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    public float destroy = 5f;
    public float Poseidonhp = 100f;



    public bool isFalling;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Falltrigger(Collision2D collission)
    {
        //usiker på hva jeg skall lege her men, du må legge til HP trigger med Poseidon som at de starter å falle etter poseidons HP er 0 eller 25 eller 75 du bestemmer.
        if (Poseidonhp <= 0)
        {

            //StartCoroutine(Fall());
        }
     
    /*void IEnumiratorFall()
    {
        Poseidonhp = 0f;
        yield return new WaitForSeconds(fallWait);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroy);
    }
    // adde spawn funksjonen vist du vil, eller kopier platformen flere ganger op til deg
    */
    }
    










}
