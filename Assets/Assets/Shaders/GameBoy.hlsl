#define H 255

void GameBoy_float(in float3 Col, out float3 Out)
{
    if (Col.r > 0.80)
    {
        Col = half3(202./H, 220./H, 159./H);
    }
    else if (Col.r <= 0.80 && Col.r > 0.60)
    {
        Col = half3(48./H, 98.H, 48./H);
    }
    else if (Col.r <= 0.80 && Col.r > 0.40)
    {
        Col = half3(48./H, 98.H, 48./H);
    }
    else if (Col.r <= 0.40 && Col.r > 0.20)
    {
        Col = half3(48./H, 98.H, 48./H);
    }
    else 
    {
        Col = half3(15./H, 56./H, 15./H);
    }

    Out = Col;
}