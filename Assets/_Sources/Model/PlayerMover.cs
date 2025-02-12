using System;
using System.Collections;
using UnityEngine;

namespace _Sources.Model
{
    public class PlayerMover : MonoBehaviour
    {
        private Coroutine _coroutine;
        private LayerMask _wallLayer;
        private bool _isInit;

        public void Init(LayerMask wallLayerMask)
        {
            _wallLayer = wallLayerMask;
            _isInit = true;
        }

        public void TryMove(Vector2 direction, float rayLength, float speed)
        {
            if (_isInit == false)
                return;
        
            if (_coroutine != null)
                return;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction * rayLength, rayLength, _wallLayer);

            if(hit.collider == null)
                return;

            _coroutine = StartCoroutine(Move(GetMovePoint(hit.transform, direction), speed));
        }

        private Vector2 GetMovePoint(Transform hitTransform, Vector2 reyDirection)
        {
            float xPosition;
            float yPosition;
            float distance;
            float radiusDelimiter = 2;

            if (reyDirection == Vector2.up || reyDirection == Vector2.down)
            {
                distance = (hitTransform.localScale.y + transform.localScale.y) / radiusDelimiter;
                yPosition = hitTransform.position.y - distance * reyDirection.y;
                xPosition = transform.position.x;
            }
            else if (reyDirection == Vector2.right || reyDirection == Vector2.left)
            {
                distance = (hitTransform.localScale.x + transform.localScale.x) / radiusDelimiter;
                xPosition = hitTransform.position.x - distance * reyDirection.x;
                yPosition = transform.position.y;
            }
            else
            {
                throw new ArgumentException($"Неверный вектор {nameof(reyDirection)}");
            }

            Vector2 point = new Vector2(xPosition, yPosition);

            return point;
        }

        private IEnumerator Move(Vector2 direction, float speed)
        {
            float epsilon = 0.001f;

            while (Vector2.Distance(transform.position, direction) >= epsilon)
            {
                transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);

                yield return null;
            }

            _coroutine = null;

            yield return null;
        }
    }
}
