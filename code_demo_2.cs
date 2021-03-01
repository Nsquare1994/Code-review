private void Steer()
{
    if (LogitechGSDK.LogiIsConnected(0))
    {
        LogitechGSDK.DIJOYSTATE2ENGINES rec;

        rec = LogitechGSDK.LogiGetStateUnity(0);
        // for wheel object rotate
        float degree = -1.0f * (float)(rec.lX / 32767.0f) * 450.0f;
        if (this.SteerWheel)
        {
            this.SteerWheel.transform.eulerAngles = new Vector3(this.SteerWheel.transform.eulerAngles.x, this.SteerWheel.transform.eulerAngles.y, degree);
        }
        // for steer object rotate
        this.FLWheelC.steerAngle = this.maxSteerAngle * rec.lX / 32767;
        this.FRWheelC.steerAngle = this.maxSteerAngle * rec.lX / 32767;
        LogitechGSDK.LogiPlayDamperForce(0, 50);
        LogitechGSDK.LogiPlaySpringForce(this.index, this.offsetPercentage, this.saturationPercentage, this.coefficientPercentage);
    }

}