#version 330 core
// GLSL
layout (location = 0) in vec3 aPosition;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    mat4 MVP = model * view * projection;
    gl_Position = vec4(aPosition, 1.0) * MVP;
}
