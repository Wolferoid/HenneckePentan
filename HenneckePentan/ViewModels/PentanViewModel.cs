using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenneckePentan.ViewModels
{
    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using Models;
    using S7.Net;
    using Services;
    public class PentanViewModel : ViewModelBase
    {
        bool isConnected;
        bool isEnabled;
        PentanService pentanService;
        public PentanViewModel()
        {
            isConnected = false;
            isEnabled = false;
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
            if(pentanService == null)
                pentanService = new PentanService();
            
            if (!isConnected)
            {
                StatusBarText = pentanService.Connect();
                isConnected = true;
                BtnConnectText = "РАЗЪЕДИНИТЬ";
            }
            else
            {
                StatusBarText = pentanService.Disconnect();
                isConnected = false;
                BtnConnectText = "ПОДКЛЮЧИТЬ";
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
            if (isConnected)
            {
                if (!isEnabled)
                {
                    StatusBarText = pentanService.PidEnable();
                    isEnabled = true;
                    BtnPidText = "ВЫКЛЮЧИТЬ";
                }
                else
                {
                    StatusBarText = pentanService.PidDisable();
                    isEnabled = false;
                    BtnPidText = "ВКЛЮЧИТЬ";
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
            // TODO: Handle command logic here
        }
        #endregion
    }
}
