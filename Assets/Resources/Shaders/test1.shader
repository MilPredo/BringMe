Shader "Custom/test1" {
    Properties {
        _Color ("Kulay", Color) = (1,1,1,1)
    }
    SubShader {

        CGPROGRAM
        #pragma surface surf Lambert

        /*
        Surface Output of Lambert
        struct SurfaceOutput {
            fixed3 Albedo; diffuse 
            fixed3 Normal; tangent space normal
            fixed3 Emission; 
            half Specular; specular power 0.0-1.0 range
            fixed Gloss; specular intensity
            fixed Alpha; alpha
        }
        */
        
        struct Input{
            float2 uvMainTex;
        };

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o){
            o.Emission = _Color.rgb;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
