using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.VM
{
    class SetSettingsVM: BindableBase, IDialogAware
    {
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        private uint _amount;
        public uint Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }
        private double _seconds;
        public double Seconds
        {
            get { return _seconds; }
            set { SetProperty(ref _seconds, value); }
        }
        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Seconds= parameters.GetValue<double>("time");
            Amount = parameters.GetValue<uint>("amount");
        }

        private string _title = "Notification";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public event Action<IDialogResult> RequestClose;

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "ok")
                result = ButtonResult.OK;
            else if (parameter?.ToLower() == "cancel")
                result = ButtonResult.Cancel;
            var x = new DialogParameters();
            x.Add("amount", Amount);
            x.Add("time", Seconds);
            RaiseRequestClose(new DialogResult(result,x));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }
    }
}
