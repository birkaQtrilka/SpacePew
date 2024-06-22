using System.Collections;
using UnityEngine;

public class DirectionChanger : MonoBehaviour
{
    [SerializeField] float _turnTime;
    [SerializeField] float _debugDistance;
    bool activated;

    void OnDrawGizmos()
    {
        float edgesDistance = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y) / 2f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + transform.right * edgesDistance, transform.forward * _debugDistance);    
        Gizmos.DrawRay(transform.position - transform.right * edgesDistance, transform.forward * _debugDistance);
    }

    void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        var controls = other.transform.GetComponentInParent<PlayerControls>();
        if ( controls == null) return;

        activated = true;
        Barrier barrier = other.transform.parent.GetComponentInChildren<Barrier>();
        barrier.gameObject.SetActive(true);

        Barrier newBarrier = Instantiate(barrier, barrier.transform.position, barrier.transform.rotation);
        controls.StartCoroutine(TurnShip(controls, transform.rotation,transform.forward, barrier));
    }

    IEnumerator TurnShip(PlayerControls controls, Quaternion endRot, Vector3 forward, Barrier barrier)
    {
        EnemySpawner.CanSpawn = false;
        controls.CanMoveForward = false;
        float currTurnTime = 0;
        Vector3 startForward = controls.transform.forward;
        Vector3 startPos = controls.transform.position;
        Debug.Log("start");
        //put player at 0 0 0
        while(currTurnTime <= _turnTime)
        {
            yield return null;
            controls.transform.position = Vector3.Slerp(startPos, transform.position, currTurnTime / _turnTime);
            controls.transform.forward = Vector3.Slerp(startForward, forward, currTurnTime / _turnTime);
            currTurnTime += Time.deltaTime;
        }
        EnemySpawner.CanSpawn = true;
        controls.CanMoveForward = true;
        //activated = false;

        controls.transform.SetPositionAndRotation(transform.position, endRot);
        barrier.gameObject.SetActive(true);
    }
}
