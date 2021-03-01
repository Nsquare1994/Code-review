private void LogitechSteeringWheelBtnPress()
{
    LogitechGSDK.DIJOYSTATE2ENGINES rec;
    rec = LogitechGSDK.LogiGetStateUnity(0);

    if (rec.rglSlider[0] != 32767)
    {
        //Debug.Log(this.transform.position);
        // right
        // change gear)

        if (LogitechGSDK.LogiButtonTriggered(0, 4))
        {
            if (this.gearStatus >= 3)
                this.gearStatus = 0;
            else
                this.gearStatus++;
        }
        // left
        // change gear
        else if (LogitechGSDK.LogiButtonTriggered(0, 5))
        {
            if (this.gearStatus <= 0)
                this.gearStatus = 3;
            else
                this.gearStatus--;
        }
    }

    // 0 = P, 1 = N, 2 = R, 3 = D
    if (gearStatus == 0)
        shiftGear.text = "P";
    else if (gearStatus == 1)
        shiftGear.text = "N";
    else if (gearStatus == 2)
        shiftGear.text = "R";
    else if (gearStatus == 3)
        shiftGear.text = "D";
    if (rec.rgbButtons[10] == 128)
    {
        // right
        // right turn light
    }
    if (rec.rgbButtons[11] == 128)
    {
        // left
        // left turn light
    }
    if (rec.rgbButtons[23] == 128)
    {
        // horn
    }
}