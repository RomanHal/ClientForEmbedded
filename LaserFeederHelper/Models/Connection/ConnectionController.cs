using LaserFeederHelper.Models.Connection.Commands;
using LaserFeederHelper.Models.Connection.Interpreters;
using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection
{
    public class ConnectionController
    {
        ConnectionHandler _connection; 
        CommandsController _commandsController;
        DecoderController _decoderController;
        Queue<byte> bytesToInterpreting=new Queue<byte>();
        private readonly Action errorhandler;

        public uint Amount { get => _decoderController.Amount; }
        public double TimeSeconds { get => _decoderController.TimeS; }
        public DeviceStatus Status { get => _decoderController.Status; }
        public ConnectionController(Action errorhandler)
        {
            _connection = new ConnectionHandler(HandleData);
            _commandsController = new CommandsController(_connection.Send, ErrorHandler);
            _decoderController = new DecoderController(SendData,_connection.Send ,_commandsController.SendConfirmation, ErrorHandler);
            this.errorhandler = errorhandler;
        }
        public void Connect()
        {
            _connection.Connect();
        }
        private void HandleData(Queue<byte> bytes)
        {
            while (bytes.Count > 0)
                _decoderController.HandleByte(bytes.Dequeue());
        }
        public void SendCommand(SerialCommands command,object data)
        {
            _commandsController.SendCommand(command, data);
        }
        public byte SendData()
        {
            while(bytesToInterpreting.Count==0) Thread.Sleep(1000);
            return bytesToInterpreting.Dequeue();
        }
        public void ErrorHandler()
        {
            Console.WriteLine("Not good");
            errorhandler();
        }

    }
}
