Shader "Custom/Water2D"
{
      Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveHeight ("Wave Height", Float) = 0.1
        _ScrollSpeed ("Scroll Speed", Float) = 0.5
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _WaveSpeed;
            float _WaveHeight;
            float _ScrollSpeed;

            v2f vert (appdata v)
            {
                v2f o;
               
                float wave = sin(_Time.y * _WaveSpeed + v.vertex.x * 10) * _WaveHeight;
                v.vertex.y += wave;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color;
               
                o.uv.x += _Time.y * _ScrollSpeed;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float ripple = sin(i.uv.x * 30 - _Time.y * 3) * 0.02;
                i.uv.y += ripple;
                
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                return col;
            }
            ENDCG
        }
    }
}
