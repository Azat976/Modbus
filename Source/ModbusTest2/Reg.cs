using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ModbusTest2
{

    public class Reg
    {
        public static byte[] func1 = { 0, 1, 0, 0, 0 };
    }

    public class Master
    {
        public string inIp1 = "127.0.0.1";
        public int port1 = 507;

        List<byte> Header;

             //public Master(string inIp, int port, string slaveid)
        //{
        //    inIp1 = inIp;
        //    port1 = port;
        //}
        private static TcpClient TCPMBus= new TcpClient();
        //private static byte[] TCPMBusBuffer = new byte[2048];

        #region
        public static void Connect(string server, int port)
        {
            //TcpClient TCPMBus = new TcpClient();
            TCPMBus.ReceiveBufferSize = 48;
            TCPMBus.Connect(server, port);
            NetworkStream tcpStream = TCPMBus.GetStream();


        }
        #endregion

    


        public static void WriteData(/*string server, int port,*/byte[] data)
        {
            //TcpClient TCPMBus = new TcpClient();
            //TCPMBus.ReceiveBufferSize = 48;
            //TCPMBus.Connect(server, port);
            NetworkStream tcpStream = TCPMBus.GetStream();
            tcpStream.Write(data, 0, data.Length);
            
            byte[] bytes = new byte[TCPMBus.ReceiveBufferSize];
            int bytesRead = tcpStream.Read(bytes, 0, TCPMBus.ReceiveBufferSize);
            byte[] bytes1 = new byte[TCPMBus.ReceiveBufferSize];
            int bytesRead1 = tcpStream.Read(bytes1, 0, TCPMBus.ReceiveBufferSize);
            TCPMBus.Close();
            // Строка, содержащая ответ от сервера
            string returnData = BitConverter.ToString(bytes);
            Console.WriteLine("Ответ: " + returnData);
            byte[] values = new byte[2];
            int str = Convert.ToInt32(data[7]);
            for (int j = 0; j < 8; j++)
            {
                values[1] = (byte)(bytes[9 + (j * 2)]);
                values[0] = (byte)(bytes[10 + (j * 2)]);
                if (str == 3)
                    Console.WriteLine("распознанные данные, регистр " + (j + 1) + ": " + BitConverter.ToUInt16(values, 0));
            }
            Console.ReadLine();
            Console.ReadLine();
        }

        public static byte[] FullPocket(byte[] data)
        {
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            WriteData(mess1);
            return mess1;
        }


        public static void ReadCoilStatus(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
            byte[] data = new byte[7];
            data[0] = len[0];
            data[1] = unit;
            data[2] = func[0];
            data[3] = startAddress[0];
            data[4] = startAddress[1];
                //Количество регистров, далее сделано всё так же
            data[5] = 0;
            data[6] = 2;
            //byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(data);
            Console.WriteLine("Запрос Read Coil Status:  00-01-00-00-00-" + msd1);
            FullPocket(data);
        


            //byte[] data = new byte[7];
            //data[0] = len[0];
            //data[1] = unit;
            //data[2] = func[0];
            //data[3] = startAddress[0];
            //data[4] = startAddress[1];
            ////Количество регистров, далее сделано всё так же
            //data[5] = 0;
            //data[6] = 2;
            ////byte[] mess1 = Reg.func1.Concat(data).ToArray();
            //string msd1 = BitConverter.ToString(data);
            //Console.WriteLine("Запрос Read Coil Status:  00-01-00-00-00-" + msd1);
            //FullPocket(data);
            //return data;
            ////return data;

        }
        public static void ReadInputStatus(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
            byte[] data = new byte[7];
            data[0] = len[0];
            data[1] = unit;
            data[2] = func[1];
            data[3] = startAddress[0];
            data[4] = startAddress[1];
            data[5] = 0;
            data[6] = 2;
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(mess1);
            Console.WriteLine("Запрос Read Input Status: " + msd1);
            FullPocket(data);
        }
        public static void ReadHoldingReg(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
            byte[] data = new byte[7];
            data[0] = len[0];
            data[1] = unit;
            data[2] = func[2];
            data[3] = startAddress[0];
            data[4] = startAddress[1];
            data[5] = 0;
            data[6] = 2;
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(mess1);
            Console.WriteLine("Запрос Read Holding Registers: " + msd1);
            FullPocket(data);
        }
        public static void ReadInputReg(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
            byte[] data = new byte[7];
            data[0] = len[0];
            data[1] = unit;
            data[2] = func[3];
            data[3] = startAddress[0];
            data[4] = startAddress[1];
            data[5] = 0;
            data[6] = 1;
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(mess1);
            Console.WriteLine("Запрос Read Input Registers: " + msd1);
            FullPocket(data);
        }
        public static void ForceSingleCoil(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
            byte[] data = new byte[7];
            data[0] = len[0];
            data[1] = unit;
            data[2] = func[4];
            data[3] = startAddress[0];
            data[4] = startAddress[1];
            data[5] = 0;
            data[6] = 1;
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(mess1);
            Console.WriteLine("Запрос Force Single Coil: " + msd1);
            FullPocket(data);
        }
        public static void PresetSingleReg(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
            byte[] data = new byte[7];
            data[0] = len[0];
            data[1] = unit;
            data[2] = func[5];
            data[3] = startAddress[0];
            data[4] = startAddress[1];
            data[5] = 1;
            data[6] = 2;
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(mess1);
            Console.WriteLine("Запрос Preset Single Register: " + msd1);
            FullPocket(data);
        }
        public static void ForceMultipleCoil(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
                byte[] data = new byte[9];
                data[0] = len[2];
                data[1] = unit;
                data[2] = func[6];
                data[3] = startAddress[0];
                data[4] = startAddress[1];
                data[5] = 0;
                data[6] = 5;
                data[7] = 1;
                data[8] = 19;
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(mess1);
            Console.WriteLine("Запрос Force Multiple Coils: " + msd1);
            FullPocket(data);
        }
        public static void PresetMultipleReg(byte[] len, byte unit, byte[] func, byte[] startAddress)
        {
                byte[] data = new byte[12];
                data[0] = len[1];
                data[1] = unit;
                data[2] = func[7];
                data[3] = startAddress[0];
                data[4] = startAddress[1];
                data[5] = 0;            // количество регистров
                data[6] = 2;           // для чтения
                data[7] = 4;            // количество байт далее
                data[8] = 0;           // значение ст
                data[9] = 10;           // значение мл
                data[10] = 1;           // значение ст
                data[11] = 2;           // значение мл
            byte[] mess1 = Reg.func1.Concat(data).ToArray();
            string msd1 = BitConverter.ToString(mess1);
            Console.WriteLine("Запрос Preset Multiple Registers: " + msd1);
            FullPocket(data);
        }
    }
}
