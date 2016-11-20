using CalculordApp.CalculordServiceReference;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Configuration;
using System.Globalization;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CalculordApp.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IMain, ICalculordCallback, IDisposable
    {
        private static CalculordClient _proxy;
        private IDialogCoordinator _dialogService;
        private string _mathExpression;

        public MainViewModel(IDialogCoordinator dialogService)
        {
            _dialogService = dialogService;
            _proxy = new CalculordClient(new InstanceContext(this));
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

        protected async Task WaitForConnection()
        {
            var ProgressAlert = await _dialogService.ShowProgressAsync(this, "LOADING", "Please wait...");
            ProgressAlert.SetIndeterminate(); 

            try
            {
               await Task.Run(() => _proxy.SetConnection(ConfigurationManager.AppSettings["ClientId"]));
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
                    Application.Current.Shutdown();
                }
            }
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
                && regex.IsMatch(MathExpression))
            {
                _proxy = new CalculordClient(new InstanceContext(this));
                _proxy.Calculate(MathExpression);
            }
            else
            {
                await _dialogService.ShowMessageAsync(this, "ERROR", "Invalid input!");
            }
        }

        public void Authorize(string id)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ClientId"].Value = id;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void Equals(double result)
        {
            MathExpression = result.ToString(new CultureInfo("en-US"));
        }

        public void Reject()
        {
            _dialogService.ShowMessageAsync(this, "ERROR", "Your calculation limit reached! Please buy a license!");
        }

        public void Dispose()
        {
            _proxy.Close();
        }
    }
}