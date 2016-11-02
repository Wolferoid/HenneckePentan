namespace HenneckePentan.ViewModels
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
        //private readonly IUIVisualizerService _uiVisualizerService;
        //private readonly IMessageService _messageService;
        #endregion
        public PentanWindowViewModel()
        {
            Test = new Command(OnTestExecute);
        }

        //public override string Title { get { return "Hennecke Pentan Recipe"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

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

        #region Properties
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
        public static readonly PropertyData StatusBarTextProperty = RegisterProperty("StatusBarText", typeof(string));

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
        public static readonly PropertyData BtnConnectTextProperty = RegisterProperty("BtnConnectText", typeof(string));

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
        public static readonly PropertyData BtnPidTextProperty = RegisterProperty("BtnPidText", typeof(string));

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

        #region Commands
        /// <summary>
        /// Gets the Test command.
        /// </summary>
        public Command Test { get; private set; }

        /// <summary>
        /// Method to invoke when the Test command is executed.
        /// </summary>
        private void OnTestExecute()
        {

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
