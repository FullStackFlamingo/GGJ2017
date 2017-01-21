using UnityEngine;

public class MeshTools {

    public static bool Intersect(Vector3 p1, Vector3 p2, Vector3 p3, Ray _ray)
    {
        // Vectors from p1 to p2/p3 (edges)
        Vector3 e1, e2;

        Vector3 p, q, t;
        float det, invDet, u, v;


        //Find vectors for two edges sharing vertex/point p1
        e1 = p2 - p1;
        e2 = p3 - p1;

        // calculating determinant 
        p = Vector3.Cross(_ray.origin, e2);

        //Calculate determinat
        det = Vector3.Dot(e1, p);

        //if determinant is near zero, ray lies in plane of triangle otherwise not
        if (det > -Mathf.Epsilon && det < Mathf.Epsilon) { return false; }
        invDet = 1.0f / det;

        //calculate distance from p1 to ray origin
        t = _ray.origin - p1;

        //Calculate u parameter
        u = Vector3.Dot(t, p) * invDet;

        //Check for ray hit
        if (u < 0 || u > 1) { return false; }

        //Prepare to test v parameter
        q = Vector3.Cross(t, e1);

        //Calculate v parameter
        v = Vector3.Dot(_ray.direction, q) * invDet;

        //Check for ray hit
        if (v < 0 || u + v > 1) { return false; }

        if ((Vector3.Dot(e2, q) * invDet) > Mathf.Epsilon)
        {
            //ray does intersect
            return true;
        }

        // No hit at all
        return false;
    }


    private static Vector3 edge1 = new Vector3();
    private static Vector3 edge2 = new Vector3();
    private static Vector3 tvec = new Vector3();
    private static Vector3 pvec = new Vector3();
    private static Vector3 qvec = new Vector3();
    private static Vector3 tuv = new Vector3();

    public static Vector3 intersectTriangle(Ray ray,
                                           Vector3 vert0,
                                           Vector3 vert1,
                                           Vector3 vert2)
    {
        // Find vectors for two edges sharing vert0
        edge1.Set(vert1.x - vert0.x, vert1.y - vert0.y, vert1.z - vert0.z);
        edge2.Set(vert2.x - vert0.x, vert2.y - vert0.y, vert2.z - vert0.z);

        // Begin calculating determinant -- also used to calculate U parameter
        pvec.Set(ray.direction.y * edge2.z - ray.direction.z * edge2.y, ray.direction.z * edge2.x - ray.direction.x * edge2.z, ray.direction.x * edge2.y - ray.direction.y * edge2.x);

        // If determinant is near zero, ray lies in plane of triangle
        float det = Vector3.Dot(edge1,pvec);

        if (det > -Mathf.Epsilon && det < Mathf.Epsilon)
        {
            tuv.Set(-99999, -99999, -99999);
            return tuv;
        }

        float invDet = 1.0f / det;

        // Calculate distance from vert0 to ray origin
        tvec.Set(ray.origin.x - vert0.x, ray.origin.y - vert0.y, ray.origin.z - vert0.z);

        // Calculate U parameter and test bounds
        float u = Vector3.Dot(tvec,pvec) * invDet;
        if (u < 0.0f || u > 1.0f)
        {
            tuv.Set(-99999, -99999, -99999);
            return tuv;
        }

        // Prepare to test V parameter
        qvec.Set(tvec.y * edge1.z - tvec.z * edge1.y, tvec.z * edge1.x - tvec.x * edge1.z, tvec.x * edge1.y - tvec.y * edge1.x);

        // Calculate V parameter and test bounds
        float v = Vector3.Dot(ray.direction,qvec) * invDet;
        if (v < 0.0f || (u + v) > 1.0f)
        {
            tuv.Set(-99999, -99999, -99999);
            return tuv;
        }

        // Calculate t, ray intersects triangle
        float t = Vector3.Dot(edge2, qvec) * invDet;

        
        tuv.Set(t, u, v);
        return tuv;
        
    }


}
