﻿namespace HenneckePentan.ViewModels
{
    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using Catel.Services;
    using Models;
    using Services;
    using System.Threading.Tasks;

    public class PentanWindowViewModel : ViewModelBase
    {
        #region Fields
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IMessageService _messageService;
        private bool isConnected;
        #endregion
        public PentanWindowViewModel(/*Pentan pentan, IUIVisualizerService uiVisualizerService, IMessageService messageService*/)
        {
            //Argument.IsNotNull(() => pentan);
            //Argument.IsNotNull(() => uiVisualizerService);
            //Argument.IsNotNull(() => messageService);

            //Pentan = pentan;
            //_uiVisualizerService = uiVisualizerService;
            //_messageService = messageService;
            Connect = new Command(OnConnectExecute);
            isConnected = false;
        }

        public override string Title { get { return "Hennecke Pentan Recipe"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        #region Properties
        /// <summary>
            /// BtnConnectText the property value.
            /// </summary>
        public string BtnConnectText
        {
            get { return GetValue<string>(BtnConnectTextProperty); }
            set { SetValue(BtnConnectTextProperty, value); }
        }

        /// <summary>
        /// Register the BtnConnectText property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BtnConnectTextProperty = RegisterProperty("BtnConnectText", typeof(string), "Connect");

        /// <summary>
        /// Pentan property value.
        /// </summary>
        [Model]
        public Pentan Pentan
        {
            get { return GetValue<Pentan>(PentanProperty); }
            set { SetValue(PentanProperty, value); }
        }

        /// <summary>
        /// Register the Pentan property so it is known in the class.
        /// </summary>
        public static readonly PropertyData PentanProperty = RegisterProperty("Pentan", typeof(Pentan), null);
        /// <summary>
        /// Status bar property value.
        /// </summary>
        [ViewModelToModel("Pentan")]
        public string StatusBar
        {
            get { return GetValue<string>(StatusBarProperty); }
            set { SetValue(StatusBarProperty, value); }
        }

        /// <summary>
        /// Register the StatusBar property so it is known in the class.
        /// </summary>
        public static readonly PropertyData StatusBarProperty = RegisterProperty("StatusBar", typeof(string), "Default status bar definition");

        /// <summary>
        /// TestLabel property value.
        /// </summary>
        public string TestLabel
        {
            get { return GetValue<string>(TestLabelProperty); }
            set { SetValue(TestLabelProperty, value); }
        }

        /// <summary>
        /// Register the TestLabel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData TestLabelProperty = RegisterProperty("TestLabel", typeof(string), "Default test label definition");
        #endregion

        #region Commands
        /// <summary>
        /// Gets the Connect command.
        /// </summary>
        public Command Connect { get; private set; }

        /// <summary>
        /// Method to invoke when the Connect command is executed.
        /// </summary>
        private void OnConnectExecute()
        {
            PentanService pentanService = new PentanService();
            if (!isConnected)
            {
                StatusBar = pentanService.Connect();
                BtnConnectText = "Disconnect";
                isConnected = true;
            }
            else
            {
                pentanService.Disconnect();
                BtnConnectText = "Connect";
                StatusBar = "STATUS: Disconnected.";
                isConnected = false;
            }
        }

        #endregion



        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}