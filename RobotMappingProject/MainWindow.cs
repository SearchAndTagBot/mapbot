using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotMappingProject {
    public partial class MainWindow : Form {
        const double LEFT_IR_ANGLE = -90 * Math.PI / 180;
        const double RIGHT_IR_ANGLE = 90 * Math.PI / 180;
        const double IMU_TO_PIXELS = 20;
        const double IR_TO_PIXELS = 2;
        const int INITIAL_X = 250;
        const int INITIAL_Z = 200;
        const double INITIAL_ANGLE = 0;

        int DATA_LENGTH = 7;
        double[] imuX, imuZ;
        double[] compassY;
        int[] frontIR, leftIR, rightIR;
        double[] deltaT;

        static double[] currVelX, currVelZ, currPosX, currPosZ;

        int picX, picZ, middleIR_X, middleIR_Z, leftIR_X, leftIR_Z, rightIR_X, rightIR_Z;

        Pen redPen = new Pen(Color.Red, 2);
        Pen bluePen = new Pen(Color.Blue, 2);
        Pen purplePen = new Pen(Color.Purple, 2);
        Pen pinkPen = new Pen(Color.Pink, 2);
        public MainWindow() {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
            imuX = new double[3];
            imuZ = new double[3];
            compassY = new double[3];
            frontIR = new int[3];
            leftIR = new int[3];
            rightIR = new int[3];
            deltaT = new double[3];

            currVelX = new double[3];
            currVelZ = new double[3];
            currPosX = new double[3];
            currPosZ = new double[3];
            DataReading();
        }
        async void DataReading() {
            Graphics g = mapImage.CreateGraphics();
            await Task.Delay(10);
            while (true) {
                while (!mbedData.IsOpen) {
                    await Task.Delay(2);
                }
                string input = mbedData.ReadLine();
                await RunUpdateData(input);
                try {
                    g.DrawRectangle(bluePen, new Rectangle(picX, picZ, 1, 1));
                    await Task.Delay(1);
                    if (frontIR[0] < 55) {
                        g.DrawRectangle(redPen, new Rectangle(middleIR_X, middleIR_Z, 1, 1));
                        await Task.Delay(1);
                    }
                    if (leftIR[0] < 55) {
                        g.DrawRectangle(pinkPen, new Rectangle(leftIR_X, leftIR_Z, 1, 1));
                        await Task.Delay(1);
                    }
                    if (rightIR[0] < 55) {
                        g.DrawRectangle(purplePen, new Rectangle(rightIR_X, rightIR_Z, 1, 1));
                        await Task.Delay(1);
                    }
                } catch (System.OverflowException exc) {
                    Console.WriteLine("picX:  " + picX + " picZ:  " + picZ);
                }
            await Task.Delay(5);
            }
        }
        async Task<bool> RunUpdateData(string input) {
            await Task.Delay(5);
            string[] data = input.Split(',');
            double currAngle = compassY[0] + INITIAL_ANGLE;
            double prevAngle = compassY[1] + INITIAL_ANGLE;
            if (data.Length == DATA_LENGTH) {
                Console.WriteLine("Received:  " + input);
                shiftData();
                currPosX[0] = double.Parse(data[0], System.Globalization.CultureInfo.InvariantCulture);
                currPosZ[0] = double.Parse(data[1], System.Globalization.CultureInfo.InvariantCulture);
                compassY[0] = double.Parse(data[2]) * Math.PI / 180;
                frontIR[0] = Int32.Parse(data[3]);
                leftIR[0] = Int32.Parse(data[4]);
                rightIR[0] = Int32.Parse(data[5]);
                //deltaT[0] = double.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);

                picX = (int)(INITIAL_X + currPosX[0] * IMU_TO_PIXELS);
                picZ = (int)(INITIAL_Z + currPosZ[0] * IMU_TO_PIXELS);
                Console.WriteLine("Center X:  " + picX);
                Console.WriteLine("Center Z:  " + picZ);
                middleIR_X = picX + (int)(Math.Cos(currAngle) * frontIR[0] * IR_TO_PIXELS);
                middleIR_Z = picZ + (int)(Math.Sin(currAngle) * frontIR[0] * IR_TO_PIXELS);
                leftIR_X = picX + (int)(Math.Cos(currAngle + LEFT_IR_ANGLE) * leftIR[0] * IR_TO_PIXELS);
                leftIR_Z = picZ + (int)(Math.Sin(currAngle + LEFT_IR_ANGLE) * leftIR[0] * IR_TO_PIXELS);
                rightIR_X = picX + (int)(Math.Cos(currAngle + RIGHT_IR_ANGLE) * rightIR[0] * IR_TO_PIXELS);
                rightIR_Z = picZ + (int)(Math.Sin(currAngle + RIGHT_IR_ANGLE) * rightIR[0] * IR_TO_PIXELS);
                return true;
            }
            Console.WriteLine("Invalid format");
            return false;
        }

        void shiftData() {
            for (int i = 2; i > 0; i--) {
                imuX[i] = imuX[i - 1];
                imuZ[i] = imuZ[i - 1];
                compassY[i] = compassY[i - 1];
                frontIR[i] = frontIR[i - 1];
                leftIR[i] = leftIR[i - 1];
                rightIR[i] = rightIR[i - 1];
                deltaT[i] = deltaT[i - 1];
                currVelX[i] = currVelX[i - 1];
                currVelZ[i] = currVelZ[i - 1];
                currPosX[i] = currPosX[i - 1];
                currPosZ[i] = currPosZ[i - 1];
            }
        }

        private void portButton_Click(object sender, EventArgs e) {
            if (mbedData.IsOpen) {
                mbedData.Close();
            }
            if (portBox.SelectedItem != null) {
                mbedData.PortName = (string)portBox.SelectedItem;
                try {
                    mbedData.Open();
                } catch (Exception exc) {
                    Console.WriteLine("Error:  Connecting to " + mbedData.PortName);
                }
            }
        }

        private void portBox_MouseEnter(object sender, EventArgs e) {
            portBox.Items.Clear();
            foreach (string str in System.IO.Ports.SerialPort.GetPortNames()) {
                portBox.Items.Add(str);
            }
        }

        private void testData_MouseClick(object sender, MouseEventArgs e) {
            if (!mbedData.IsOpen) {
                portButton_Click(null, null);
            }
            try {
                mbedData.WriteLine(errorBox.Text);
                System.Threading.Thread.Sleep(50);
                Console.WriteLine("Received:\n" + mbedData.ReadLine());
            } catch (Exception exc) {
                Console.WriteLine("Error:  Communicating with " + mbedData.PortName);
            }
        }
    }
}
