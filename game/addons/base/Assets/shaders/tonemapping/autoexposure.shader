//
//  Purpose: Auto Exposure that's applied separately - not in the tonemap
//

HEADER
{
    Description = "Auto Exposure";
    DevShader = true;
}  

MODES
{
	Default();
    Forward();
}
 
COMMON
{
    #include "postprocess/shared.hlsl"
}

struct VertexInput
{
    float3 vPositionOs : POSITION < Semantic( PosXyz ); >;
    float2 vTexCoord : TEXCOORD0 < Semantic( LowPrecisionUv ); >;
};

struct PixelInput
{ 
    float2 vTexCoord : TEXCOORD0;

    // VS only
    #if ( PROGRAM == VFX_PROGRAM_VS )
        float4 vPositionPs		: SV_Position;
    #endif
};
 
VS
{    
    PixelInput MainVs( VertexInput i )
    {
        PixelInput o;
        o.vPositionPs = float4(i.vPositionOs.xyz, 1.0f);
        o.vPositionPs.z = 1;
        o.vTexCoord = i.vTexCoord;
        return o;
    } 
} 

PS 
{
    #include "postprocess/common.hlsl"

    Texture2D g_tColorBuffer < Attribute( "ColorBuffer" ); SrgbRead( true ); >;

    float4 MainPs( PixelInput i ) : SV_Target0
    {   
        float4 f4ColorBuffer = g_tColorBuffer.Sample( g_sBilinearMirror, i.vTexCoord );
        return float4( f4ColorBuffer.xyz * g_flToneMapScalarLinear, f4ColorBuffer.w ); 
    }
}
