using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class FlagController : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private UnityEvent HitEventBlue;
    [SerializeField] private UnityEvent HitEventRed;
    [SerializeField] private Vector3 flagPositionInPlayer;
    [SerializeField] private Quaternion flagRotationsInPlayer;
    [SerializeField] private GameObject flag;
    private bool onTake = true;
    private void Start()
    {
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
    }
    public void ResetFlag()
    {
        this.gameObject.transform.SetParent(null);
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.Euler(0, -90, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CharacterRed")
        {
            if (onTake)
            {
                FlagTakeRed(other);
                onTake = false;
                StartCoroutine(TakeFlagPause(other));
            }
        }
        else if (other.tag == "CharacterBlue")
        {
            if (onTake)
            {
                FlagTakeBlue(other);
                onTake = false;
                EnemyAI.TargetUpdate();
                StartCoroutine(TakeFlagPause(other));
            }
        }
    }
    IEnumerator TakeFlagPause(Collider other)
    {
        yield return new WaitForSeconds(1);
        onTake = true;
    }
    private void FlagTakeRed(Collider other)
    {
        this.transform.SetParent(other.transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localPosition = flagPositionInPlayer;
        this.transform.localRotation = flagRotationsInPlayer;
        HitEventRed.Invoke();
        Debug.Log("Red" + this.gameObject.tag);
    }
    private void FlagTakeBlue(Collider other)
    {
        this.transform.SetParent(other.transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localPosition = flagPositionInPlayer;
        this.transform.localRotation = flagRotationsInPlayer;
        HitEventBlue.Invoke();
        Debug.Log("Blue" + this.gameObject.tag);
    }

}
