using LaserFeederHelper.Models;
using LaserFeederHelper.Models.Connection;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;

namespace LaserFeederHelper.VM
{
    public class MainWindowVM:Prism.Mvvm.BindableBase
    {
        System.Timers.Timer _refresher = new System.Timers.Timer(1000);

        private readonly IDialogService _dialogService;

        private ConnectionController _connectionController;
        private WindowFocuser _focuser;
        public DelegateCommand ConnectCommand { get; set; }

        public DelegateCommand StopCommand { get; set; }
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand SetSettingsCommand { get; set; }
        public DelegateCommand MyCommand { get; set; }
        private uint _amount;
        public DelegateCommand FocusNotepadCommand{get;set;}
        public DelegateCommand ExitCommand{get;set; }
        public uint Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }
        private double _time;
        public double Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }
        public MainWindowVM(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _connectionController = new ConnectionController(ConnectionErrorHandler);
            _focuser = new WindowFocuser();
            ConnectCommand = new DelegateCommand(()=> { _connectionController.Connect(); _refresher.Start(); });
            FocusNotepadCommand = new DelegateCommand(()=>_focuser.FocusAndWrite("notepad++","message"));
            SetSettingsCommand = new DelegateCommand(SetSettings);
            ExitCommand = new DelegateCommand(Exit);
            _refresher.AutoReset = true;
            _refresher.Elapsed += Refresher_Elapsed;
        }

        public void ConnectionErrorHandler()
        {

        }

        public void Exit()
        {
            string message = "Are you sure?";
            _dialogService.ShowDialog("YesNoDialog", new DialogParameters($"message={message}"), r => 
            {
                if(r.Result==ButtonResult.Yes)
                    Environment.Exit(0);
            });
        }
        public void SetSettings()
        {
            var parameters = new DialogParameters();
            parameters.Add("amount", Amount);
            parameters.Add("time", Time);
            _dialogService.ShowDialog("SetSettings", parameters, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    _connectionController.SendCommand(Resources.SerialCommands.SetSettings,(r.Parameters.GetValue<uint>("amount"), r.Parameters.GetValue<double>("time")));
                }
            });
        }
        private void Refresher_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _connectionController.SendCommand(Resources.SerialCommands.GetSettings, null);
            _connectionController.SendCommand(Resources.SerialCommands.GetStatus, null);
        }
    }
}
