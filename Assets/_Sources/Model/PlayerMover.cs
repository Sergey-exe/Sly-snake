using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private bool _hasWall;
    private Coroutine _coroutine;

    public void TryMove(Vector2 direction, float rayLength, float speed)
    {
        if (_coroutine != null)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction * rayLength);

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
            throw new ArgumentException($"Не верный вектор {nameof(reyDirection)}");
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
