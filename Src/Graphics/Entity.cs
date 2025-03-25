using StbImageSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using U.src.Utils;
using U.Src.Utils;
using System.Text.Json;

namespace U.Src.Graphics;

public class Entity
{
    readonly Shader Shader;
    readonly Texture TextureImage;
    int VBO, VAO;
    float[] Vertices = [];
    readonly Camera _camera;
    public class Shape
    {
        public float[]? Vertices { get; set; }
    }
    public Entity(string vertices, string vertexCode, string fragmentCode, ImageResult textureImage, Camera camera)
    {
        Shader = new(vertexCode, fragmentCode);
        TextureImage = new(textureImage);
        _camera = camera;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Shape? shape = JsonSerializer.Deserialize<Shape>(vertices, options);
        if (shape == null || shape.Vertices == null) return;
        Vertices = [.. shape.Vertices];
        CenterVertices();
    }

    private Vector3 CalculateCentroid()
    {
        float sumX = 0, sumY = 0, sumZ = 0;
        int vertexCount = Vertices.Length / 5;

        for (int i = 0; i < Vertices.Length; i += 5)
        {
            sumX += Vertices[i];
            sumY += Vertices[i + 1];
            sumZ += Vertices[i + 2];
        }

        return new Vector3(sumX / vertexCount, sumY / vertexCount, sumZ / vertexCount);
    }

    public void Load()
    {
        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    private void CenterVertices()
    {
        Vector3 centroid = CalculateCentroid();

        for (int i = 0; i < Vertices.Length; i += 5)
        {
            Vertices[i] -= centroid.X;
            Vertices[i + 1] -= centroid.Y + 0.15f;
            Vertices[i + 2] -= centroid.Z;
        }
    }


    private void Bind()
    {
        Shader.Use();
        GL.Enable(EnableCap.DepthTest);
        GL.BindVertexArray(VAO);
        TextureImage.Use(TextureUnit.Texture0);

        Shader.SetMat4("model", Matrix4.Identity)
              .SetMat4("view", _camera.GetViewMatrix())
              .SetMat4("projection", _camera.GetProjectionMatrix());
    }

    public void Draw(float x, float y, float z, int textureId)
    {
        Bind();
        Shader.SetInt("u_Texture", textureId);
        float[] copiedVertices = [.. Vertices];
        for (int i = 0; i < copiedVertices.Length; i += 5)
        {
            copiedVertices[i] += x;
            copiedVertices[i + 1] += y;
            copiedVertices[i + 2] += z;
        }

        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, copiedVertices.Length * sizeof(float), copiedVertices, BufferUsageHint.StaticDraw);

        GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length / 5);
    }

    public void Draw()
    {
        Bind();
        GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length / 5);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(VBO);
        GL.DeleteVertexArray(VAO);
        // GL.DeleteBuffer(EBO);
        TextureImage.Dispose();
    }
}
