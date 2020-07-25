#version 330

in PS_IN
{
	vec4 color;
} psInput; 

out vec4 fragColor;

void main()
{    	 
	 fragColor = psInput.color;
}