using System.Collections;
using UnityEngine;
using PlaceHolder;

namespace PlayerNameSpace
{
    [RequireComponent(typeof(Player))]
    public class PlayerCollition : MonoBehaviour
    {
        private Player player;
        private PlayerMovement playerMovement;

        private void Awake()
        {
            player = GetComponent<Player>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Collectables>())
            {
                collision.gameObject.GetComponent<Collectables>().OnCollect();
            }
            else if (collision.gameObject.GetComponent<DethBoundries>())
            {
                GameManager.Instance.GameOver();
            }

            if (playerMovement != null)
            {
                if (playerMovement.isOnKite == true)
                {
                    if (playerMovement.IsGrounded)
                    {
                        playerMovement.GetOffKite();
                        int childCount = player.transform.childCount;
                        Transform kite = player.transform.GetChild(childCount - 1);
                        GameObject.Destroy(kite.gameObject);
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (playerMovement != null)
            {
                if (collision.gameObject.GetComponent<Kite>())
                {
                    playerMovement.isOnKite = true;
                    collision.transform.parent = player.transform;
                    collision.transform.localPosition = new Vector3(0, 5, 0);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (playerMovement != null)
            {
                if (collision.gameObject.GetComponent<Ladder>() != null)
                {
                    playerMovement.ClimeLatter();
                }
            }

            if (player.InputActions.AllTerain.Interact.inProgress)
            {
                if (collision.gameObject.GetComponent<Door>() != null)
                {
                    collision.gameObject.GetComponent<Door>().OpenDoors();
                }
                else if (collision.gameObject.GetComponent<Chest>() != null)
                {
                    collision.gameObject.GetComponent<Chest>().OnInteract();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (playerMovement != null)
            {
                if (collision.gameObject.GetComponent<Ladder>() != null)
                {
                    playerMovement.FinishedClimingLatter();
                    StartCoroutine(TurnColliderForTime(collision, 1f));
                }
            }
        }

        private IEnumerator TurnColliderForTime(Collider2D collision, float turnOffTimeAmount)
        {
            collision.enabled = false;

            yield return new WaitForSeconds(turnOffTimeAmount);

            collision.enabled = true;
        }

    }
}
