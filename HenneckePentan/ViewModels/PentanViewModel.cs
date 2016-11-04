using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace HenneckePentan.ViewModels
{
    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using Models;
    using S7.Net;
    using Services;
    using System.Windows.Media;
    public class PentanViewModel : ViewModelBase
    {
        PentanService pentanService;
        Timer monitorTimer;
        public PentanViewModel()
        {
            Connect = new Command(OnConnectExecute);
            Pid = new Command(OnPidExecute);
            CurrentRead = new Command(OnCurrentReadExecute);
            RecipeWrite = new Command(OnRecipeWriteExecute);
        }

        #region Injection
        /// <summary>
        /// Gets or sets the Pentan.
        /// </summary>
        [Model]
        public Pentan Pentan
        {
            get { return GetValue<Pentan>(PentanProperty); }
            private set { SetValue(PentanProperty, value); }
        }

        /// <summary>
        /// Register the Pentan property so it is known in the class.
        /// </summary>
        public static readonly PropertyData PentanProperty = RegisterProperty("Pentan", typeof(Pentan));
        #endregion

        #region Propertis
        /// <summary>
        /// Gets or sets the StatusBarText.
        /// </summary>
        [ViewModelToModel("Pentan")]
        public string StatusBarText
        {
            get { return GetValue<string>(StatusBarTextProperty); }
            set { SetValue(StatusBarTextProperty, value); }
        }

        /// <summary>
        /// Register the StatusBarText property so it is known in the class.
        /// </summary>
        public static readonly PropertyData StatusBarTextProperty = RegisterProperty("StatusBarText", typeof(string), "СТРОКА СТАТУСА");

        /// <summary>
        /// Gets or sets the BtnConnectText.
        /// </summary>
        [ViewModelToModel("Pentan")]
        public string BtnConnectText
        {
            get { return GetValue<string>(BtnConnectTextProperty); }
            set { SetValue(BtnConnectTextProperty, value); }
        }

        /// <summary>
            /// Gets or sets the BtnConnectColor.
            /// </summary>
        public string BtnConnectColor
        {
            get { return GetValue<string>(BtnConnectColorProperty); }
            set { SetValue(BtnConnectColorProperty, value); }
        }

        /// <summary>
        /// Register the BtnConnectColor property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BtnConnectColorProperty = RegisterProperty("BtnConnectColor", typeof(string), Colors.Aqua.ToString());

        /// <summary>
        /// Register the BtnConnectText property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BtnConnectTextProperty = RegisterProperty("BtnConnectText", typeof(string), "ПОДКЛЮЧИТЬ");

        /// <summary>
        /// Gets or sets the BtnPidText.
        /// </summary>
        [ViewModelToModel("Pentan")]
        public string BtnPidText
        {
            get { return GetValue<string>(BtnPidTextProperty); }
            set { SetValue(BtnPidTextProperty, value); }
        }

        /// <summary>
        /// Register the BtnPidText property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BtnPidTextProperty = RegisterProperty("BtnPidText", typeof(string), "ВКЛЮЧИТЬ");

        /// <summary>
            /// Gets or sets the BtnPidColor.
            /// </summary>
        public string BtnPidColor
        {
            get { return GetValue<string>(BtnPidColorProperty); }
            set { SetValue(BtnPidColorProperty, value); }
        }

        /// <summary>
        /// Register the BtnPidColor property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BtnPidColorProperty = RegisterProperty("BtnPidColor", typeof(string), Colors.Aqua.ToString());

        /// <summary>
        /// Gets or sets the CurrentValue.
        /// </summary>
        [ViewModelToModel("Pentan")]
        public string CurrentValue
        {
            get { return GetValue<string>(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        /// <summary>
        /// Register the CurrentValue property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CurrentValueProperty = RegisterProperty("CurrentValue", typeof(string));

        /// <summary>
        /// Gets or sets the RecipeValue.
        /// </summary>
        [ViewModelToModel("Pentan")]
        public string RecipeValue
        {
            get { return GetValue<string>(RecipeValueProperty); }
            set { SetValue(RecipeValueProperty, value); }
        }

        /// <summary>
        /// Register the RecipeValue property so it is known in the class.
        /// </summary>
        public static readonly PropertyData RecipeValueProperty = RegisterProperty("RecipeValue", typeof(string));

        /// <summary>
        /// Gets or sets the ReadPumpValue.
        /// </summary>
        public string ReadPumpValue
        {
            get { return GetValue<string>(ReadPumpValueProperty); }
            set { SetValue(ReadPumpValueProperty, value); }
        }

        /// <summary>
        /// Register the ReadPumpValue property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ReadPumpValueProperty = RegisterProperty("ReadPumpValue", typeof(string), null);

        #endregion

        #region Command
        /// <summary>
        /// Gets the Connect command.
        /// </summary>
        public Command Connect { get; private set; }

        /// <summary>
        /// Method to invoke when the Connect command is executed.
        /// </summary>
        private void OnConnectExecute()
        {
            if (pentanService == null)
                pentanService = new PentanService();

            if (!pentanService.ConnectionStatus())
            {
                StatusBarText = pentanService.Connect();
                if (pentanService.ConnectionStatus()) { OnConnect(); }
            }
            else
            {
                StatusBarText = pentanService.Disconnect();
                OnDisconnect();
            }
        }

        /// <summary>
        /// Gets the Pid command.
        /// </summary>
        public Command Pid { get; private set; }

        /// <summary>
        /// Method to invoke when the Pid command is executed.
        /// </summary>
        private void OnPidExecute()
        {
            if (pentanService.ConnectionStatus())
            {
                if (!pentanService.PidStatus())
                {
                    StatusBarText = pentanService.PidEnable();
                    BtnPidText = "ВЫКЛЮЧИТЬ";
                    BtnPidColor = Colors.LimeGreen.ToString();
                }
                else
                {
                    StatusBarText = pentanService.PidDisable();
                    BtnPidText = "ВКЛЮЧИТЬ";
                    BtnPidColor = Colors.Aqua.ToString();
                }
            }
        }

        /// <summary>
        /// Gets the CurrentRead command.
        /// </summary>
        public Command CurrentRead { get; private set; }

        /// <summary>
        /// Method to invoke when the CurrentRead command is executed.
        /// </summary>
        private void OnCurrentReadExecute()
        {
            if (pentanService != null)
                CurrentValue = pentanService.ReadCurrent();
        }

        /// <summary>
        /// Gets the RecipeWrite command.
        /// </summary>
        public Command RecipeWrite { get; private set; }

        /// <summary>
        /// Method to invoke when the RecipeWrite command is executed.
        /// </summary>
        private void OnRecipeWriteExecute()
        {
            if (pentanService != null)
                StatusBarText = pentanService.WriteRecipe(RecipeValue);
        }

        #endregion

        #region Methods
        /// <summary>
        /// On connected method
        /// </summary>
        private void OnConnect()
        {
            BtnPidText = pentanService.PidStatus() ? "ВЫКЛЮЧИТЬ" : "ВКЛЮЧИТЬ";
            BtnPidColor = pentanService.PidStatus() ? Colors.LimeGreen.ToString() : Colors.Aqua.ToString();

            BtnConnectText = "РАЗЪЕДИНИТЬ";
            BtnConnectColor = Colors.LimeGreen.ToString();

            if (monitorTimer == null)
            {
                monitorTimer = new Timer(500);
                monitorTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            }
            monitorTimer.Enabled = true;
        }
        /// <summary>
        /// On diconnect method
        /// </summary>
        private void OnDisconnect()
        {
            BtnConnectText = "ПОДКЛЮЧИТЬ";
            BtnConnectColor = Colors.Aqua.ToString();
            if (monitorTimer != null) { monitorTimer.Enabled = false; }
        }
        #endregion

        #region Events
        /// <summary>
        /// On timer elapsed event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            CurrentValue = pentanService.ReadCurrent();
            ReadPumpValue = pentanService.ReadPumpRate();
        }
        #endregion
    }
}
