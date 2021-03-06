﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace GGJ.Gameplay
{
    public class Ship : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float rotationSpeed = 1.2f;

        private List<Transform> InfuencingLightBeams = new List<Transform>();

        private State state = State.Spawn;

        public bool CanBeControlledByLighthouse => state != State.Sailing;

        public int CollectedChests { get; private set; }

        [Range(0f, 1f)]
        [SerializeField]
        private float chestSpeedReduction;

        [SerializeField]
        Animator animator;

        [SerializeField]
        private AnimationCurve spawnAnimation;

        private UnityEvent<Ship> onCrash = new UnityEvent<Ship>();
        public UnityEvent<Ship> OnCrash
        {
            get { return onCrash; }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                Chest chest = other.GetComponentInParent<Chest>();
                Debug.Assert(chest != null, $@"GameObject with Tag ""Chest"" has no component of type ""{nameof(Chest)}""");
                chest.Collected();
                ++CollectedChests;
            }
            else if (other.CompareTag("Obstacle") || other.CompareTag("Ship"))
            {
                onCrash.Invoke(this);
                GetComponent<SphereCollider>().enabled = false;
                speed = 0;
                if(animator)
                    animator.SetTrigger("Sink");
            }
            else if (other.CompareTag("Lightbeam"))
            {
                if (!InfuencingLightBeams.Contains(other.transform.parent))
                {
                    InfuencingLightBeams.Add(other.transform.parent);
                }
            }
        }

        public void FinishCrash()
        {
            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Lightbeam"))
            {
                InfuencingLightBeams.Remove(other.transform.parent);
            }
        }

        public void ReachedHarbor()
        {
            state = State.InPort;
            // We could do some animation here, or we can just remove the ship. At this point, I'll just do the latter
            gameObject.SetActive(false);
        }

        Vector3 CalculateDirection()
        {
            Vector3 center = Vector3.zero;
            float count = 0;
            for (int i = 0; i < InfuencingLightBeams.Count; i++)
            {
                center += InfuencingLightBeams[i].position;
                count++;
            }
            center /= count;

            return new Vector3(center.x, transform.position.y, center.z);
        }

        public void DoSpawnAnimation()
        {
            StartCoroutine(SpawnAnimationRoutine());
        }

        public IEnumerator SpawnAnimationRoutine()
        {
            Vector3 startPosition = transform.position - transform.forward * 2 - transform.up * 3;
            Vector3 endPosition = transform.position;
            const float totalTime = 2f;
            float time = totalTime;

            while (time > 0)
            {
                time -= Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, spawnAnimation.Evaluate(1 - time / totalTime));
                yield return null;
            }
            transform.position = endPosition;
            state = State.Sailing;
        }

        void Update()
        {
            if (state == State.Spawn) { return; }

            WindManager windManager = GameManager.Instance.WindManager;
            Vector3 direction = Vector3.forward;
            if (windManager != null)
            {
                Vector2 wind = windManager.GetWindSpeed();
                Vector3 windDirection = Vector3.forward + new Vector3(wind.x, 0f, wind.y);
                direction = (Vector3.forward + windDirection) / 2f;
            }
            if (CollectedChests > 0) { direction *= (1f - chestSpeedReduction); }

            transform.Translate(direction * Time.deltaTime * speed);

            if (InfuencingLightBeams.Count > 0)
            {
                Vector3 targetDirection = CalculateDirection() - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * rotationSpeed, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }

        private enum State
        {
            Spawn,
            Sailing,
            InPort
        }
    }
}