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
        private readonly Action _errorhandler;
        private readonly Action _statusUpdated;

        public uint Amount { get => _decoderController.Amount; }
        public double TimeSeconds { get => _decoderController.TimeS; }
        public DeviceStatus Status { get => _decoderController.Status; }
        public ConnectionController(Action errorhandler,Action statusUpdated)
        {
            _connection = new ConnectionHandler(HandleData);
            _commandsController = new CommandsController(_connection.Send, ErrorHandler);
            _decoderController = new DecoderController(SendData,_connection.Send ,_commandsController.SendConfirmation, ErrorHandler);
            _errorhandler = errorhandler;
            _statusUpdated = statusUpdated;
        }
        public void Connect()
        {
            _connection.Connect();
        }
        private void HandleData(Queue<byte> bytes)
        {
            bytesToInterpreting = bytes;
            while (bytes.Count > 0)
                _decoderController.HandleByte(bytesToInterpreting.Dequeue());
            _statusUpdated();
        }
        public void SendCommand(SerialCommands command,object data=null)
        {
            _commandsController.SendCommand(command, data);
        }
        public byte SendData()
        {
            return bytesToInterpreting.Dequeue();
        }
        public void ErrorHandler()
        {
            Console.WriteLine("Not good");
            _errorhandler();
        }

    }
}
