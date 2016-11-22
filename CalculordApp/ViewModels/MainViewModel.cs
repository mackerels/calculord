using CalculordApp.Model;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CalculordApp.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IMain
    {
        private IDialogCoordinator _dialogService;
        private string _mathExpression;
        private string _chumakForToday;
        private bool _isOpenFlyOut;

        public MainViewModel(IDialogCoordinator dialogService)
        {
            _dialogService = dialogService;
         
            CalculordModel.Instance.AuthorizationConfirmed += SetConfiguration;
            CalculordModel.Instance.ResultReceived += ShowResult;
            CalculordModel.Instance.CalculationRejected += CancelCalculation;
            CalculordModel.Instance.ChumakReceived += ShowChumak;

            ConfigurationManager.AppSettings["ClientId"] = ""; //use for test, create new client
        }

        public string MathExpression
        {
            get { return _mathExpression; }
            set
            {
                _mathExpression = value;
                NotifyOfPropertyChange(() => MathExpression);
            }
        }

        public string ChumakForToday
        {
            get { return _chumakForToday; }
            set
            {
                _chumakForToday = value;
                NotifyOfPropertyChange(() => ChumakForToday);
            }
        }

        public bool IsOpenFlyOut
        {
            get { return _isOpenFlyOut; }
            set
            {
                _isOpenFlyOut = value;
                NotifyOfPropertyChange(() => IsOpenFlyOut);
            }
        }

        private void SetConfiguration(string id)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ClientId"].Value = id;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void ShowResult(double result)
        {
            MathExpression = result.ToString(new CultureInfo("en-US"));
        }

        private async void CancelCalculation(string msg)
        {
            await _dialogService.ShowMessageAsync(this, "ERROR", msg);
           
            await Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, new System.Action(ShutdownApplication));
        }

        private void ShowChumak(string img)
        {
            ChumakForToday = img;
            DisplayChumak();
        }

        private async Task WaitForConnection()
        {
            var ProgressAlert = await _dialogService.ShowProgressAsync(this, "LOADING", "Please wait...");
            ProgressAlert.SetIndeterminate(); 

            try
            {
               await Task.Run(() => CalculordModel.Instance.SetConnection(ConfigurationManager.AppSettings["ClientId"]));
               await ProgressAlert.CloseAsync();
               await _dialogService.ShowMessageAsync(this, "HELLO!", "Welcome to Calculord Service!");
            }
            catch
            {
               await ProgressAlert.CloseAsync();

               MessageDialogResult res = 
                    await _dialogService.ShowMessageAsync(this, "ERROR", 
                    "Connection failed! Please try again!", MessageDialogStyle.AffirmativeAndNegative);

                if (res == MessageDialogResult.Affirmative)
                {
                    SetConnection();
                }
                else
                {
                    ShutdownApplication();
                }
            }
        }

        private void ShutdownApplication()
        {
            Application.Current.Shutdown();
        }

        public async void SetConnection()
        {
            await WaitForConnection();
        }

        public void UpdateExpression(string symbol)
        {
            MathExpression += symbol;
        }

        public void Clear()
        {
            MathExpression = string.Empty;
        }

        public async void Calculate()
        {
            var regex = new Regex(@"(?x)
                ^
                (?> (?<p> \( )* (?>-?\d+(?:\.\d+)?) (?<-p> \) )* )
                (?>(?:
                    [-+*/]
                    (?> (?<p> \( )* (?>-?\d+(?:\.\d+)?) (?<-p> \) )* )
                )*)
                (?(p)(?!))
                $
            ");
          
            if (!string.IsNullOrEmpty(MathExpression) 
                && (regex.IsMatch(MathExpression)
                || MathExpression == "chumak"))
            {
                CalculordModel.Instance.Calculate(MathExpression, ConfigurationManager.AppSettings["ClientId"]);
            }
            else
            {
                await _dialogService.ShowMessageAsync(this, "ERROR", "Invalid input!");
            }
        }

        public void DisplayChumak()
        {
            IsOpenFlyOut = true;
        }
    }
}