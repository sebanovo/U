#version 330 core

in vec2 TexCoord;
uniform sampler2D texture1;

void main() {
    // FragColor = vec4(ourColor, 0.1);
    // FragColor = texture(ourTexture, TexCoord);
    // FragColor = texture(texture1, TexCoord) * vec4(color, 1.0); 
    // FragColor =mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2f) * vec4(ourColor, 1.0f);
    gl_FragColor =  texture(texture1, TexCoord);
}