#pragma once
#include <math.h>

//----------------------------------------------------------------------------------
// Defines and Macros
//----------------------------------------------------------------------------------
#ifndef PI
#define PI 3.14159265358979323846f
#endif

#ifndef DEG2RAD
#define DEG2RAD (PI/180.0f)
#endif

#ifndef RAD2DEG
#define RAD2DEG (180.0f/PI)
#endif


struct Vector3 {
    float x;
    float y;
    float z;

    
#pragma region COMPARISON

    /// <summary>
    /// Compara para ver si los vectores son iguales
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    bool operator==(Vector3 other)
    {
        return (x == other.x && y == other.y && z == other.z);
    }

    /// <summary>
    /// Compara para saber si los vectores son diferentes
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    bool operator!=(Vector3 other)
    {
        return (x != other.x || y != other.y || z != other.z);
    }

#pragma endregion


#pragma region ADDITIONS

    /// <summary>
    /// Suma 2 vectores
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    Vector3 operator+(Vector3 other)
    {
        return Vector3{ x + other.x, y + other.y , z + other.z };
    }
    
    /// <summary>
    /// Suma un vector a otro
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    Vector3& operator+=(Vector3 other)
    {
        x += other.x;
        y += other.y;
        z += other.z;
        return *this;
    }

    /// <summary>
    /// Suma el vector con un flotante
    /// </summary>
    /// <param name="add"></param>
    /// <returns></returns>
    Vector3 operator+(float add)
    {
        return Vector3{ x + add, y + add , z + add };
    }

    /// <summary>
    /// Suma a los componentes del vector un flotante
    /// </summary>
    /// <param name="add"></param>
    /// <returns></returns>
    Vector3& operator+=(float add)
    {
        x += add;
        y += add;
        z += add;
        return *this;
    }

#pragma endregion

#pragma region SUBSTRACTIONS

    /// <summary>
    /// Resta de vectores
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    Vector3 operator-(Vector3 other)
    {
        return Vector3{ x - other.x, y - other.y , z - other.z };
    }

    /// <summary>
    /// Resta un vector a otro
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    Vector3& operator-=(Vector3 other)
    {
        x -= other.x;
        y -= other.y;
        z -= other.z;
        return *this;
    }

    /// <summary>
    /// Resta un flotante a un vector
    /// </summary>
    /// <param name="sub"></param>
    /// <returns></returns>
    Vector3 operator-(float sub)
    {
        return Vector3{ x - sub, y - sub , z - sub };
    }

    /// <summary>
    /// Resta a los componentes del vector un flotante
    /// </summary>
    /// <param name="sub"></param>
    /// <returns></returns>
    Vector3& operator-=(float sub)
    {
        x -= sub;
        y -= sub;
        z -= sub;
        return *this;
    }

#pragma endregion

#pragma region SCALE (MULTIPLY)

    /// <summary>
    /// Multiplica un vector con un flotante
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    Vector3 operator*(float scale)
    {
        return Vector3{ x * scale,y * scale,z * scale };
    }

    /// <summary>
    /// Modifica la escala de un vector con un flotante
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    Vector3& operator*=(float scale)
    {
        x *= scale;
        y *= scale;
        z *= scale;
        return *this;
    }

    /// <summary>
    /// Multiplica 2 vectores
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    Vector3 operator*(Vector3 other)
    {
        return Vector3{ x * other.x,y * other.y ,z * other.z };
    }

    /// <summary>
    /// Multiplica el vector con otro
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    Vector3& operator*=(Vector3 other)
    {
        x *= other.x;
        y *= other.y;
        z *= other.z;
        return *this;
    }

#pragma endregion

    
    /// <summary>
    /// Devuelve la magnitud del vector
    /// </summary>
    /// <returns></returns>
    float Magnitude()
    {
        return sqrtf(x * x + y * y + z * z);
    }

    /// <summary>
    /// Lo mismo que Magnitude() pero mas rapido
    /// </summary>
    /// <returns></returns>
    float SqrMagnitude()
    {
        return x * x + y * y + z * z;
    }
};

/// <summary>
/// Crea un Vector3 con valores en 0.0f
/// </summary>
/// <returns></returns>
Vector3 Vector3Zero()
{
    return Vector3{ 0.0f, 0.0f, 0.0f };
}

/// <summary>
/// Crea un Vector3 con sus parametros en 1.0f
/// </summary>
/// <returns></returns>
Vector3 Vector3One()
{
    return Vector3{ 1.0f,1.0f,1.0f };
}

/// <summary>
/// Devuelve el producto punto entre 2 Vector3
/// </summary>
/// <param name="v1"></param>
/// <param name="v2"></param>
/// <returns></returns>
float Vector3DotProduct(Vector3 v1, Vector3 v2)
{
    return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z);
}

/// <summary>
/// Calcula la distancia entre 2 Vectores3
/// </summary>
/// <param name="v1"></param>
/// <param name="v2"></param>
/// <returns></returns>
float Vector3Distance(Vector3 v1, Vector3 v2)
{
    float dx = v2.x - v1.x;
    float dy = v2.y - v1.y;
    float dz = v2.z - v1.z;
    return sqrtf(dx * dx + dy * dy + dz * dz);
}

/// <summary>
/// Devuelve un Vector3 con los valores invertidos
/// </summary>
/// <param name="v"></param>
/// <returns></returns>
Vector3 Vector3Negate(Vector3 v)
{
    return { -v.x, -v.y, -v.z };
}

/// <summary>
/// Devuelve la division entre 2 vectores
/// </summary>
/// <param name="v1"></param>
/// <param name="v2"></param>
/// <returns></returns>
Vector3 Vector3Divide(Vector3 v1, Vector3 v2)
{
    return Vector3{ v1.x / v2.x, v1.y / v2.y, v1.z / v2.z };
}

/// <summary>
/// Devuelve un Vector3 Normalizado
/// </summary>
/// <param name="v"></param>
/// <returns></returns>
Vector3 Vector3Normalize(Vector3 v)
{
    Vector3 result = v;

    float length, inverseLength;
    length = sqrtf(v.x * v.x + v.y * v.y + v.z * v.z);
    if (length == 0.0f) length = 1.0f;
    inverseLength = 1.0f / length;

    result.x *= inverseLength;
    result.y *= inverseLength;
    result.z *= inverseLength;

    return result;
}


/// <summary>
/// Devuelve el producto cruz entre 2 Vector3
/// </summary>
/// <param name="v1"></param>
/// <param name="v2"></param>
/// <returns></returns>
Vector3 Vector3CrossProduct(Vector3 v1, Vector3 v2)
{
    return Vector3{ v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x };
}

/// <summary>
/// Devuelve un vector perpendicular en base al que se le paso
/// </summary>
/// <param name="v"></param>
/// <returns></returns>
Vector3 Vector3Perpendicular(Vector3 v)
{
    Vector3 result = { 0 };

    float min = (float)fabs(v.x);
    Vector3 cardinalAxis = { 1.0f, 0.0f, 0.0f };

    if (fabs(v.y) < min)
    {
        min = (float)fabs(v.y);
        Vector3 tmp = { 0.0f, 1.0f, 0.0f };
        cardinalAxis = tmp;
    }

    if (fabs(v.z) < min)
    {
        Vector3 tmp = { 0.0f, 0.0f, 1.0f };
        cardinalAxis = tmp;
    }

    result = Vector3CrossProduct(v, cardinalAxis);

    return result;
}

// Orthonormalize provided vectors
// Makes vectors normalized and orthogonal to each other
void Vector3OrthoNormalize(Vector3* v1, Vector3* v2)
{
    *v1 = Vector3Normalize(*v1);
    Vector3 vn = Vector3CrossProduct(*v1, *v2);
    vn = Vector3Normalize(vn);
    *v2 = Vector3CrossProduct(vn, *v1);
}

/// <summary>
/// Calcula la interpolacion lineal entre 2 Vector3 en un espacio de tiempo
/// </summary>
/// <param name="v1"></param>
/// <param name="v2"></param>
/// <param name="amount"></param>
/// <returns></returns>
Vector3 Lerp(Vector3 v1, Vector3 v2, float amount)
{
    Vector3 result = { 0 };

    result.x = v1.x + amount * (v2.x - v1.x);
    result.y = v1.y + amount * (v2.y - v1.y);
    result.z = v1.z + amount * (v2.z - v1.z);

    return result;
}

/// <summary>
/// Calcula el reflejo del Vector3 en base a una Normal (Otro Vector3)
/// </summary>
/// <param name="v"></param>
/// <param name="normal"></param>
/// <returns></returns>
Vector3 Reflect(Vector3 v, Vector3 normal)
{
    // I is the original vector
    // N is the normal of the incident plane
    // R = I - (2*N*( DotProduct[ I,N] ))

    Vector3 result = { 0 };

    float dotProduct = Vector3DotProduct(v, normal);

    result.x = v.x - (2.0f * normal.x) * dotProduct;
    result.y = v.y - (2.0f * normal.y) * dotProduct;
    result.z = v.z - (2.0f * normal.z) * dotProduct;

    return result;
}

/// <summary>
/// Vevuelve un Vector3 con los componentes minimos entre 2 Vector3
/// </summary>
/// <param name="v1"></param>
/// <param name="v2"></param>
/// <returns></returns>
Vector3 Min(Vector3 v1, Vector3 v2)
{
    Vector3 result = { 0 };

    result.x = fminf(v1.x, v2.x);
    result.y = fminf(v1.y, v2.y);
    result.z = fminf(v1.z, v2.z);

    return result;
}

/// <summary>
/// Vevuelve un Vector3 con los componentes maximos entre 2 Vector3
/// </summary>
/// <param name="v1"></param>
/// <param name="v2"></param>
/// <returns></returns>
Vector3 Max(Vector3 v1, Vector3 v2)
{
    Vector3 result = { 0 };

    result.x = fmaxf(v1.x, v2.x);
    result.y = fmaxf(v1.y, v2.y);
    result.z = fmaxf(v1.z, v2.z);

    return result;
}

// Compute barycenter coordinates (u, v, w) for point p with respect to triangle (a, b, c)
// NOTE: Assumes P is on the plane of the triangle
Vector3 Barycenter(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
{
    //Vector v0 = b - a, v1 = c - a, v2 = p - a;

    Vector3 v0 = b - a;
    Vector3 v1 = c - a;
    Vector3 v2 = p - a;
    float d00 = Vector3DotProduct(v0, v0);
    float d01 = Vector3DotProduct(v0, v1);
    float d11 = Vector3DotProduct(v1, v1);
    float d20 = Vector3DotProduct(v2, v0);
    float d21 = Vector3DotProduct(v2, v1);

    float denom = d00 * d11 - d01 * d01;

    Vector3 result = { 0 };

    result.y = (d11 * d20 - d01 * d21) / denom;
    result.z = (d00 * d21 - d01 * d20) / denom;
    result.x = 1.0f - (result.z + result.y);

    return result;
}