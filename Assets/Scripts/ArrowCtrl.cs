using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCtrl : MonoBehaviour
{
    public Transform realArrow;

    public Transform startPoint;    // assign an obj near the tail as the startPoint if you test with FollowMouse().
    public Transform endPoint;

    public Sprite blackArrow;
    public Sprite redArrow;

    private void Start()
    {
        //UpdatePointing();
    }

    // adjust the arrow between two objects (point at the border of the object instead of the center)
    public void UpdatePointing()
    {
        UpdateArrow(startPoint.position, endPoint.position);
    }

    // adjust the arrow according to s->t
    private void UpdateArrow(Vector3 s, Vector3 t)
    {
        // direction
        s.z = 0;
        transform.position = s + (t-s).normalized* startPoint.GetComponent<SpriteRenderer>().bounds.size.x/2;
        t.z = 0;
        transform.right = t - s;
        // length
        float dis = Vector3.Distance(s, t) - endPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2 - startPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2; // minus realsize/2
        dis /= realArrow.localScale.x;
        Vector2 originalSize = realArrow.GetComponent<SpriteRenderer>().size;
        originalSize.x = dis;
        realArrow.GetComponent<SpriteRenderer>().size = originalSize;
    }

    public void SetToRed(bool isRed)
    {
        if (isRed)
            realArrow.GetComponent<SpriteRenderer>().sprite = redArrow;
        else
            realArrow.GetComponent<SpriteRenderer>().sprite = blackArrow;
    }

    /*
    // follow the mouse
    public void FollowMouse()
    {
        UpdateArrow(startPoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    */

    /*
    // set the start point
    public void SetStartPoint(Transform trans)
    {
        startPoint = trans;
    }

    public void SetEndPoint(Transform trans)
    {
        if (startPoint.Equals(trans))
        {
            Destroy(gameObject);
        }
        else
        {
            endPoint = trans;
            UpdatePointing();
        }
    }
    */

    /*
    // calculate the crosspoint
    private Vector2 GetIntersection(Vector2 lineAStart, Vector2 lineAEnd, Vector2 lineBStart, Vector2 lineBEnd)
    {
        float x1 = lineAStart.x, y1 = lineAStart.y;
        float x2 = lineAEnd.x, y2 = lineAEnd.y;

        float x3 = lineBStart.x, y3 = lineBStart.y;
        float x4 = lineBEnd.x, y4 = lineBEnd.y;

        if (x1 == x2 && x3 == x4 && x1 == x3)
        {
            return Vector2.zero;
        }

        if (y1 == y2 && y3 == y4 && y1 == y3)
        {
            return Vector2.zero;
        }

        if (x1 == x2 && x3 == x4)
        {
            return Vector2.zero;
        }

        if (y1 == y2 && y3 == y4)
        {
            return Vector2.zero;
        }
        float x, y;

        if (x1 == x2)
        {
            float m2 = (y4 - y3) / (x4 - x3);
            float c2 = -m2 * x3 + y3;

            x = x1;
            y = c2 + m2 * x1;
        }
        else if (x3 == x4)
        {
            float m1 = (y2 - y1) / (x2 - x1);
            float c1 = -m1 * x1 + y1;

            x = x3;
            y = c1 + m1 * x3;
        }
        else
        {
            float m1 = (y2 - y1) / (x2 - x1);
            float c1 = -m1 * x1 + y1;
            float m2 = (y4 - y3) / (x4 - x3);
            float c2 = -m2 * x3 + y3;
            x = (c1 - c2) / (m2 - m1);
            y = c2 + m2 * x;
        }

        if (IsInsideLine(lineAStart, lineAEnd, x, y) &&
             IsInsideLine(lineBStart, lineBEnd, x, y))
        {
            return new Vector2(x, y);
        }
        return Vector2.zero;
    }

    private bool IsInsideLine(Vector2 start, Vector2 end, float x, float y)
    {
        return ((x >= start.x && x <= end.x)
            || (x >= end.x && x <= start.x))
            && ((y >= start.y && y <= end.y)
                || (y >= end.y && y <= start.y));
    }
    */

}
