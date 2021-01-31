﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Gameplay
{
    public class Whale : MonoBehaviour
    {
        [SerializeField] private float speed;

        private State state = State.Spawn;

        [SerializeField]
        private AnimationCurve spawnAnimation;
        [SerializeField]
        private AnimationCurve despawnAnimation;

        public void DoSpawnAnimation()
        {
            StartCoroutine(SpawnAnimationRoutine(State.Sailing, spawnAnimation));
        }

        public void DoDespawnAnimation()
        {
            StartCoroutine(SpawnAnimationRoutine(State.Despawn, despawnAnimation));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ship"))
            {
                DoDespawnAnimation();
            }
        }

        IEnumerator SpawnAnimationRoutine(State newState, AnimationCurve animationCurve)
        {
            Vector3 startPosition = transform.position - transform.forward * 2 - transform.up * 3;
            Vector3 endPosition = transform.position;
            const float totalTime = 2f;
            float time = totalTime;

            while (time > 0)
            {
                time -= Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, animationCurve.Evaluate(1 - time / totalTime));
                yield return null;
            }
            transform.position = endPosition;
            state = newState;
            if (newState == State.Despawn)
            {
                gameObject.SetActive(false);
            }
        }

        void Start()
        {
            DoSpawnAnimation();
        }

        void Update()
        {
            if (state != State.Sailing) { return; }

            Vector3 direction = transform.right;
            transform.Translate(direction * Time.deltaTime * speed);
        }

        private enum State
        {
            Spawn,
            Sailing,
            Despawn
        }
    }

}