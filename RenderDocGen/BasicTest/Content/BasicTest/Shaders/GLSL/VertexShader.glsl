#version 330

layout (location = 0) in vec4 Position0;
layout (location = 1) in vec4 Color0;

out PS_IN
{
	vec4 color;
} vsOutput;

void main()
{    
    gl_Position = Position0;
	vsOutput.color = Color0;
}