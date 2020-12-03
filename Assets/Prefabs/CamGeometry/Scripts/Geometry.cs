using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour
{
    public float depth = 0.400f;
    public float width = 0.492f;
    public float height = 0.370f;

    public float z_factor = 2;

    public Material mat;
    
    GameObject CreateSphere(Vector3 pos, float radius)
    {
        var sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph.transform.SetParent(transform);

        pos.z -= depth;
        pos.z *= z_factor;
        sph.transform.localPosition = pos*100;
        sph.transform.localScale = new Vector3(radius, radius, radius);
        sph.GetComponent<Renderer>().material = mat;

        Debug.Log("Created Sphere");
        Debug.Log(sph.transform.position);

        return sph;
    }

    GameObject CreateSphere(Vector2 pos, float radius)
    {
        var poss = new Vector3(pos.x*width/2,pos.y*height/2,depth);
        return CreateSphere(poss, radius);
    }

    public Vector3 zaxis
    {
        get
        {
            return transform.position - transform.parent.position;
        }
    }

    public Vector3 origin
    {
        get { return transform.parent.position; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("pressed y");
            var xx = Random.value - 0.5f;
            var yy = Random.value - 0.5f;
            Debug.Log($"{xx}, {yy}");
            CreateSphere(new Vector2(xx,yy), 1f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("pressed u");
            var xx = Random.value - 0.5f;
            var yy = Random.value - 0.5f;
            var zz = Random.value - 0.4f;
            Debug.Log($"{xx}, {yy}, {zz}");
            CreateSphere(new Vector3(xx,yy,zz), 1f);
        }
    }

    public void PlaceObject(GameObject obj, Vector2 pos)
    {
        var sph = CreateSphere(pos, 0.01f);
        obj.transform.position = sph.transform.position;
        Destroy(sph);
    }

    public void PlaceObject(GameObject obj, Vector3 pos)
    {
        var sph = CreateSphere(pos, 0.01f);
        obj.transform.position = sph.transform.position;
        Destroy(sph);
    }

    public Vector3[] GetRealPoints(Vector2[] imagePoints, float[] dists)
    {
        Debug.Log("GettingRealPoints");
        float[] x = { 0, 0, 0, 0 };
        float[] y = { 0, 0, 0, 0 };
        float[] a = { 0, 0, 0 };
        float[] b = { 0, 0, 0 };
        float[] c = { 0, 0, 0 };
        float[] L = { 0, 0, 0 };

        for (int i = 0; i < 3; i++)
        {
            L[i] = dists[i] * dists[i];
        }

        for (int i = 0; i < 4; i++)
        {
            x[i] = imagePoints[i % 3].x * width / depth;
            y[i] = imagePoints[i % 3].y * height / depth;
        }

        a[0] = 1 + x[0] * x[0] + y[0] * y[0];
        b[0] = -2 * (x[0] * x[1] + y[0] * y[1] + 1);
        c[0] = 1 + x[1] * x[0] + y[1] * y[1];

        a[1] = a[0];
        b[1] = -2 * (x[0] * x[2] + y[0] * y[2] + 1);
        c[1] = 1 + x[2] * x[2] + y[2] * y[2];

        a[2] = c[0];
        b[2] = -2 * (x[1] * x[2] + y[1] * y[2] + 1);
        c[2] = c[1];

        List<Vector3[]> ans = new List<Vector3[]>();

        int[] iii = { -1, 1 };

        foreach(var i1 in iii)
        {
            foreach(var i2 in iii)
            {
                var coefs = GetFinalEq(a, b, c, L, i1, i2);
                var sols = SolveBinSearch(coefs);
                Debug.Log($"Sols length: {sols.Count}");
                foreach(var sol in sols)
                {
                    Debug.Log($"sol: {sol}");
                    var p1 = new Vector3(
                        sol * imagePoints[0].x * width / depth,
                        sol * imagePoints[0].y * height / depth,
                        sol
                        );

                    float sol1 = GetSolQuadratic(sol, a[0], b[0], c[0], L[0], i1);
                    float sol2 = GetSolQuadratic(sol, a[1], b[1], c[1], L[1], i2);

                    var p2 = new Vector3(
                        sol1 * imagePoints[1].x * width / depth,
                        sol1 * imagePoints[1].y * height / depth,
                        sol1
                        );

                    var p3 = new Vector3(
                        sol2 * imagePoints[2].x * width / depth,
                        sol2 * imagePoints[2].y * height / depth,
                        sol2
                        );

                    float dist_val = Vector3.Distance(p2, p3);
                    if(Mathf.Abs(dist_val - dists[2]) < 0.1f)
                    {
                        Debug.Log($"good sol: {sol}");
                        Vector3[] anss = { p1, p2, p3 };
                        ans.Add(anss);
                    }
                }
            }
        }

        ans.Sort((v1, v2) => Vector3.Distance(v1[1], v1[2]) > Vector3.Distance(v2[1], v2[2]) ? 1 : -1);
        if (ans.Count > 0)
        {
            var anss = ans[0];
            ans.Clear();
            return anss;
        }

        return null;
    }

    float GetSolQuadratic(float x, float a, float b, float c, float L, int i)
    {
        float y1 = -b / c / 2;
        float y2 = y1 * y1 - a / c;
        float y3 = L / c / y2;
        y2 = Mathf.Sqrt(Mathf.Abs(y2));

        return y1 * x + i * y2 * Mathf.Sqrt(Mathf.Abs(x * x + y3));
    }

    float[] GetQuadraticCoef(float a, float b, float c, float L)
    {
        float y1 = -b / c / 2;
        float y2 = y1 * y1 - a / c;
        float y3 = L / c / y2;
        y2 = Mathf.Sqrt(Mathf.Abs(y2));

        float[] coefs = { y1, y2, y3 };
        return coefs;
    }

    float[] GetFinalEq(float[] a, float[] b, float[] c, float[] L, int i1, int i2)
    {
        float a1 = a[0], a2 = a[1], a3 = a[2];
        float b1 = b[0], b2 = b[1], b3 = b[2];
        float c1 = c[0], c2 = c[1], c3 = c[2];
        float L1 = L[0], L2 = L[1], L3 = L[2];

        var y = GetQuadraticCoef(a1, b1, c1, L1);
        float y1 = y[0], y2 = y[1], y3 = y[2];

        var z = GetQuadraticCoef(a2, b2, c2, L2);
        float z1 = z[0], z2 = z[1], z3 = z[2];

        y2 *= i1;
        z2 *= i2;

        a3 = a3 / c1;
        c3 = c3 / c2;

        float alpha = -a3 * a1 - c3 * a2 - a3 * b1 * y1 - c3 * b2 * z1 + b3 * y1 * z1;

        float by = -a3 * b1 * y2 + b3 * y2 * z1;
        float bz = -c3 * b2 * z2 + b3 * y1 * z2;

        float gamma = b3 * y2 * z2;

        float delta = a3 * L1 + c3 * L2 - L3;

        float[] coefs = { alpha, by, y3, bz, z3, gamma, delta };
        return coefs;
    }

    float EvalFunc(float x, float[] coefs)
    {
        float x2 = x * x;

        float a = coefs[0];


        float by = coefs[1];
        float y = coefs[2];
        float sy = Mathf.Sqrt(Mathf.Abs(x2 + y));


        float bz = coefs[3];
        float z = coefs[4];
        float sz = Mathf.Sqrt(Mathf.Abs(x2 + z));

        float c = coefs[5];
        float d = coefs[6];
        return a * x2 + by * x * sy + bz * x * sz + c * sy * sz + d;
    }

    List<float> SolveBinSearch(float[] coefs, float start = 0, float fin = 1.5f, float eps = 1e-5f, int init_split = 101, float prec = 1e-7f)
    {
        List<float> sols = new List<float>();

        float[] init_points = new float[init_split];
        float init_step = (fin - start) / (init_split - 1);
        for (int i = 0; i < init_split; i++)
        {
            init_points[i] = start + i * init_step;
        }

        float[] first_iter = new float[init_split];
        for (int i = 0; i < init_split; i++)
        {
            first_iter[i] = EvalFunc(init_points[i],coefs);
        }

        List<Vector2> pairs = new List<Vector2>();

        for (int i = 0; i < init_split - 1; i++)
        {
            if(first_iter[i]*first_iter[i+1] <= 0)
            {
                pairs.Add(new Vector2(init_points[i], init_points[i + 1]));
            }
        }

        foreach(var p in pairs)
        {
            float x = p.x;
            float y = p.y;
            float m = (x + y) / 2;

            while (y - x > eps)
            {
                m = (x + y) / 2;
                float fx = EvalFunc(x, coefs);
                //float fy = EvalFunc(y, coefs);
                float fm = EvalFunc(m, coefs);

                if (Mathf.Abs(fm) < prec)
                {
                    break;
                }
                if (fm * fx < 0)
                {
                    y = m;
                }
                else
                {
                    x = m;
                }
            }

            sols.Add(m);
        }

        return sols;
    }

    /// <summary>
    /// Returns the rotations to be made in order around axes: 
    /// 1. y axis
    /// 2. proper z axis of object
    /// 3. the new v1 axis
    /// To be performed in this order, for obtaining 
    /// the angle of vectors v1 and v2 
    /// From the angle of x and y axes 
    /// </summary>
    Vector3 GetAngleRotation(Vector3 v1, Vector3 v2)
    {
        float A = Vector3.Angle(v1, v2);

        float a1 = Vector3.Angle(new Vector3(1, 0, 0), new Vector3(v1.x, 0, v1.z));
        if (v1.z > 0) a1 *= -1;
        float a2 = - Vector3.Angle(new Vector3(v1.x, 0, v1.z), v1);

        Vector3 n = Vector3.Cross(v1, new Vector3(v1.x, 0, v1.z));
        Vector3 pr1 = Vector3.ProjectOnPlane(v2, n);
        Vector3 pr2 = Vector3.Project(v2, v1);

        float a3 = Vector3.Angle(v2 - pr1, v2 - pr2);

        return new Vector3(a1,a2,a3);
    }

    public void RotateObjectToMatch(GameObject obj, Vector3 v1, Vector3 v2)
    {
        //var rot = GetAngleRotation(v1, v2);
        //Debug.Log($"rot {rot.x} {rot.y} {rot.z} ");

        //var someObj = new GameObject();
        //someObj.transform.SetParent(transform);

        //var secondAxis = Vector3.Cross(new Vector3(0, 1, 0), new Vector3(v1.x, 0, v1.z));

        //someObj.transform.Rotate(new Vector3(0, 1, 0), rot.x);
        //someObj.transform.Rotate(secondAxis, rot.y);
        ////someObj.transform.Rotate(v1, rot.z);

        //obj.transform.localRotation = someObj.transform.rotation;

        //Destroy(someObj);

        Transform t = obj.transform;
        Transform t1 = obj.transform.GetChild(3);
        Transform t2 = obj.transform.GetChild(4);

        Vector3 h1 = t1.position - t.position;

        Vector3 z0 = new Vector3(1, 0, 0);

        float a1 = Vector3.Angle(z0, v1);
        float a2 = Vector3.Angle(h1, v1);
        if (Mathf.Abs(a2) > 1e-1)
        {
            Vector3 n1 = Vector3.Cross(z0, v1);
            Quaternion q = Quaternion.AngleAxis(a1,n1);
            //Debug.Log($"qq {q.w} {q.x} {q.y} {q.z}");
            t.rotation = q;
            //Debug.Log($"h1 {h1.x} {h1.y} {h1.z}");
            //Debug.Log($"v1 {v1.x} {v1.y} {v1.z}");
            //Debug.Log($"n1 {n1.x} {n1.y} {n1.z}");
            //Debug.Log($"a1 {a1}");
            //Debug.Log($"a2 {a2}");
            
            //h1 = t1.position - t.position;
            //Debug.Log($"h2 {h1.x} {h1.y} {h1.z}");
            //a1 = Vector3.Angle(h1, v1);
        }

        t1 = obj.transform.GetChild(3);
        t2 = obj.transform.GetChild(4);
        h1 = t1.position - t.position;

        Vector3 h2 = t2.position - t.position;
        Vector3 n2 = Vector3.Project(h2, h1);
        float n2_norm = Vector3.Magnitude(n2);
        h2 /= n2_norm;
        n2 /= n2_norm;

        Vector3 n3 = Vector3.Project(v2, h1);
        float n3_norm = Vector3.Magnitude(n3);
        v2 /= n3_norm;
        n3 /= n3_norm;

        a2 = Vector3.Angle(h2 - n2, v2 - n2);
        var ax = Vector3.Cross(h2 - n2, v2 - n2);

        //float a1 = Vector3.Angle(h1, v1);
        //if (Mathf.Abs(a1) > 1e-1)
        //{
        //    Vector3 n1 = Vector3.Cross(h1, v1);
        //    Quaternion q = Quaternion.AngleAxis(a1, n1);
        //    Debug.Log($"qq {q.w} {q.x} {q.y} {q.z}");
        //    t.rotation *= q;
        //    Debug.Log($"h1 {h1.x} {h1.y} {h1.z}");
        //    Debug.Log($"v1 {v1.x} {v1.y} {v1.z}");
        //    Debug.Log($"n1 {n1.x} {n1.y} {n1.z}");
        //    Debug.Log(a1);
        //    h1 = t1.position - t.position;
        //    Debug.Log($"h2 {h1.x} {h1.y} {h1.z}");
        //    //a1 = Vector3.Angle(h1, v1);
        //}

        if (Mathf.Abs(a2) > 1e-3)
        {
            var q2 = Quaternion.AngleAxis(a2, ax);
            t.rotation = q2 * t.rotation;
        }
        
    }
}
