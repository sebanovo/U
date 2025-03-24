using System;
using System.Reflection;
using StbImageSharp;

namespace U.Properties;

public class Resources
{
    public class Image
    {
        public static ImageResult awesomeface
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Images.awesomeface.png");
            }
        }

        public static ImageResult container
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Images.container.jpg");
            }
        }

        public static ImageResult texture
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Images.texture.png");
            }
        }

        private static ImageResult LoadEmbeddedShader(string textureResourceName)
        {
            Stream stream = Assembly.GetExecutingAssembly()?.GetManifestResourceStream(textureResourceName)
                            ?? throw new Exception($"Texture resource {textureResourceName} not found.");
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            return image;
        }
    }

    public static class Shader
    {
        public static string uShapeVert
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.uShape.vert");
            }
        }

        public static string uShapeFrag
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.uShape.frag");
            }
        }

        public static string axisVert
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.axis.vert");
            }
        }

        public static string axisFrag
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.axis.frag");
            }
        }

        public static string crossHairVert
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.crossHair.vert");
            }
        }

        public static string crossHairFrag
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.crossHair.frag");
            }
        }

        private static string LoadEmbeddedShader(string resourceName)
        {

            Stream stream = (Assembly.GetExecutingAssembly()?.GetManifestResourceStream(resourceName)) ??
                             throw new Exception($"Shader resource {resourceName} not found.");
            StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
