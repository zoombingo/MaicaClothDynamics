#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

Texture2D skinned_data_1;
Texture2D skinned_data_2;
Texture2D skinned_data_3;
uint skinned_tex_height;
uint skinned_tex_width;

void MyDQSkinningFunction_float(uint vertexId, out float3 vertex, out float3 normal, out float3 tangent)
{
//#if defined(SHADER_API_D3D11) || defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES3) || defined(SHADER_API_METAL) || defined(SHADER_API_VULKAN) || defined(SHADER_API_PS4) || defined(SHADER_API_XBOXONE)
//#ifdef USE_BUFFERS
	uint2 skinned_tex_uv;
	skinned_tex_uv.x = vertexId % skinned_tex_width;
	skinned_tex_uv.y = vertexId / skinned_tex_width;

	float4 data_1 = skinned_data_1[skinned_tex_uv];
	float4 data_2 = skinned_data_2[skinned_tex_uv];

#ifdef _TANGENT_TO_WORLD
	float2 data_3 = skinned_data_3[skinned_tex_uv].xy;
#endif

	vertex = data_1.xyz;
	normal.x = data_1.w;
	normal.yz = data_2.xy;

#ifdef _TANGENT_TO_WORLD
	tangent.xy = data_2.zw;
	tangent.zw = data_3.xy;
#else
	tangent = 0;
#endif
//#endif
//#endif

}

#endif //end MYHLSLINCLUDE_INCLUDED