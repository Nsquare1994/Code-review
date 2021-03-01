private void Accelerate()
{
    if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
    {
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);
        int CFS_Accelerator_Pedal_Position = rec.lZ;
        int CFS_Brake_Pedal_Position = rec.lY;

        float normalize_CFS_Accelerator_Pedal_Position = (-1 * CFS_Accelerator_Pedal_Position + 32767.0f) / 65535.0f;
        float normalize_CFS_Brake_Pedal_Position = (-1 * CFS_Brake_Pedal_Position + 32767.0f) / 65535.0f;
        this.VDS_Veh_Speed = Math.Abs(this.transform.InverseTransformVector(this.gameObject.GetComponent<Rigidbody>().velocity).z);
        this.VDS_Veh_Eng_Torque = 0;


        this.engineTorque.Update(normalize_CFS_Accelerator_Pedal_Position, this.VDS_Veh_Speed);

        //D
        if (this.gearStatus == 3)
            this.VDS_Veh_Eng_Torque = this.engineTorque.WheelTorque;
        //R
        else if (this.gearStatus == 2)
            this.VDS_Veh_Eng_Torque = -1 * this.engineTorque.WheelTorque;
        else
            this.VDS_Veh_Eng_Torque = 0;

        this.VDS_Brake_Torque = brakeStrengthCoefficient * (float)normalize_CFS_Brake_Pedal_Position;
        float rpm = this.engineTorque.rpm;

        // change of model causing fps drop
        if (FPS_30 < 0)
        {

            Quaternion next_pos = Quaternion.Euler(new Vector3(this.speedometerNeedle.transform.eulerAngles.x,
                                                               this.speedometerNeedle.transform.eulerAngles.y,
                                                               Math.Abs(VDS_Veh_Speed * 2.23693629f) * 287 / 200 - 52.93f));

            this.speedometerNeedle.transform.rotation = Quaternion.Lerp(this.speedometerNeedle.transform.rotation, next_pos, Time.deltaTime * 60);

            next_pos = Quaternion.Euler(new Vector3(this.TachometerNeedle.transform.eulerAngles.x,
                                                               this.TachometerNeedle.transform.eulerAngles.y,
                                                               (rpm / 1000) * 287 / 10 - 52.93f));

            this.TachometerNeedle.transform.rotation = Quaternion.Lerp(this.TachometerNeedle.transform.rotation, next_pos, Time.deltaTime * 60);
        }

        this.RLWheelC.motorTorque = this.VDS_Veh_Eng_Torque;
        this.RRWheelC.motorTorque = this.VDS_Veh_Eng_Torque;
        this.RLWheelC.brakeTorque = this.VDS_Brake_Torque;
        this.RRWheelC.brakeTorque = this.VDS_Brake_Torque;
        this.FLWheelC.brakeTorque = this.VDS_Brake_Torque;
        this.FRWheelC.brakeTorque = this.VDS_Brake_Torque;
    }
}