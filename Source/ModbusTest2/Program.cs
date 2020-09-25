using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ModbusTest2;

namespace TCPModbus
{

    #region   
    class Header
    {

        ////Для заголовка: id-идентефикатор(в дальнейшем он конвертируется в байты(можно было и его через байты), IdProt-Идентификатор протокола, lenghtMod-длинна сообщения протокола модбас
        //static ushort id = 1;
        //static byte[] IdProt = { 0, 0 };
        //static byte lenghtMod = 0;

        ////Для формирования функции: len- длинна сообщения, func-функция
        //static byte[] len = { 6, 11, 8 };
        //static byte unit = 1;
        //static byte[] func = { 1, 2, 3, 4, 5, 6, 15, 16 };

        ////Для стартого адреса, везде стоит 0.1, в некоторых длинна сообщения, и кол во байт выставлены сразу как константы!(для примера)
        //static byte[] startAddress = { 0, 1 };



        public byte[] func1 = {0, 1, 0, 0, 0};
        public byte[] Header1(ushort id, byte[] IdProt, byte lenghtMod)
        {
            byte[] data = new byte[5];
            byte[] _id = BitConverter.GetBytes((short)id);
            data[0] = _id[1];
            data[1] = _id[0];
            data[2] = IdProt[0];
            data[3] = IdProt[1];
            data[4] = lenghtMod;
            return data;
        }
    }

    class Podkl
    {
        public const int port = 506;
        public const string server = "127.0.0.1";
    }
    #endregion
    public class Program
    {

        //Для заголовка: id-идентефикатор(в дальнейшем он конвертируется в байты(можно было и его через байты), IdProt-Идентификатор протокола, lenghtMod-длинна сообщения протокола модбас
        static ushort id = 1;
        static byte[] IdProt = { 0, 0 };
        static byte lenghtMod = 0;

        //Для формирования функции: len- длинна сообщения, func-функция
        static byte[] len = { 6, 11, 8 };
        static byte unit = 1;
        static byte[] func = { 1, 2, 3, 4, 5, 6, 15, 16 };

        //Для стартого адреса, везде стоит 0.1, в некоторых длинна сообщения, и кол во байт выставлены сразу как константы!(для примера)
        static byte[] startAddress = { 0, 1 };



        #region 
        //Можно было и так...
        //byte[] _id = BitConverter.GetBytes((short)id);
        //static byte[] PocketByte1 = { _id[1], _id[0], 0, 0, 0, 6, 1, 1, 0, 1, 3, 4 };
        //static byte[] PocketByte2 = { 0, 1, 0, 0, 0, 6, 1, 2, 0, 1, 0, 2 };
        //static byte[] PocketByte3 = { 0, 1, 0, 0, 0, 6, 1, 3, 0, 1, 0, 6 };
        //static byte[] PocketByte4 = { 0, 1, 0, 0, 0, 6, 1, 4, 0, 1, 0, 6 };
        //static byte[] PocketByte5 = { 0, 1, 0, 0, 0, 6, 1, 5, 0, 1, 0, 1 };
        //static byte[] PocketByte6 = { 0, 1, 0, 0, 0, 6, 1, 6, 0, 1, 1, 2 };
        //static byte[] PocketByte7 = { 0, 1, 0, 0, 0, 8, 1, 15, 0, 3, 0, 5, 1, 19 };
        //static byte[] PocketByte8 = { 0, 1, 0, 0, 0, 11, 1, 16, 0, 3, 0, 2, 4, 0, 10, 1, 2 };
        #endregion

//        public ModbusTest2.Master MB;
//        static ModbusTest2.Podkl P;

        public const int port = 507;
        public const string server = "127.0.0.1";
        static void Main(string[] args)
        {
            ModbusTest2.Master.Connect(server, port);
            Console.Write(" Выберите пункт меню: ");
            String strr = Console.ReadLine();


            switch (strr)
            {
                //
                case "1":
                    ModbusTest2.Master.ReadCoilStatus(len, unit, func, startAddress);

                    break;
                //
                case "2":
                    ModbusTest2.Master.ReadInputStatus(len, unit, func, startAddress);
                    break;

                case "3":
                    ModbusTest2.Master.ReadHoldingReg(len, unit, func, startAddress);
                    break;

                case "4":
                    ModbusTest2.Master.ReadInputReg(len, unit, func, startAddress);
                    break;
                //    //
                case "5":
                    ModbusTest2.Master.ForceSingleCoil(len, unit, func, startAddress);
                    break;
                //    //
                case "6":
                    ModbusTest2.Master.PresetSingleReg(len, unit, func, startAddress);
                    break;
                //    //
                case "7":
                    ModbusTest2.Master.ForceMultipleCoil(len, unit, func, startAddress);
                    break;
                //    //
                case "8":
                    ModbusTest2.Master.PresetMultipleReg(len, unit, func, startAddress);
                    break;
                    //}
            }


        }
    }
}

