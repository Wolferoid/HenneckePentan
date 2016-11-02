using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenneckePentan.Models
{
    using Catel.Data;

    public class Pentan: ModelBase
    {
        Plc plc;
        bool isEnabled;
        bool isConnected;

        //public Pentan()
        //{
        //    //StatusBarText = "STATUS TEXT WILL BE HERE";
        //    //BtnConnectText = "Connect";
        //    //BtnPidText = "Enable";
        //    isEnabled = false;
        //    isConnected = false;
        //}

        #region Properties
        /// <summary>
        /// Gets or sets the StatusBarText value.
        /// </summary>
        public string StatusBarText
        {
            get { return GetValue<string>(StatusBarTextProperty); }
            set { SetValue(StatusBarTextProperty, value); }
        }

        /// <summary>
        /// Register the StatusBarText property so it is known in the class.
        /// </summary>
        public static readonly PropertyData StatusBarTextProperty = RegisterProperty("StatusBarText", typeof(string), "Здесь будут статусы");

        /// <summary>
            /// Gets or sets the BtnConnectText value.
            /// </summary>
        public string BtnConnectText
        {
            get { return GetValue<string>(BtnConnectTextProperty); }
            set { SetValue(BtnConnectTextProperty, value); }
        }

        /// <summary>
        /// Register the BtnConnectText property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BtnConnectTextProperty = RegisterProperty("BtnConnectText", typeof(string), "Соединить");

        /// <summary>
            /// Gets or sets the BtnPidText value.
            /// </summary>
        public string BtnPidText
        {
            get { return GetValue<string>(BtnPidTextProperty); }
            set { SetValue(BtnPidTextProperty, value); }
        }

        /// <summary>
        /// Register the BtnPidText property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BtnPidTextProperty = RegisterProperty("BtnPidText", typeof(string), "Вкл.");

        /// <summary>
            /// Gets or sets the CurrentValue value.
            /// </summary>
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
        /// Gets or sets the RecipeValue value.
        /// </summary>
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

        #region Methods
        public void Connect()
        {
            if (!isConnected)
            {
                isConnected = true;
                BtnConnectText = "Disconnect";
            }
            else
            {
                isConnected = false;
                BtnConnectText = "Connect";
            }
        }
        public void RecipeWrite() { }
        public void CurrentRead() { }
        public void Pid()
        {
            if (!isEnabled)
            {
                isEnabled = true;
                BtnPidText = "Disable";
            }
            else
            {
                isEnabled = false;
                BtnPidText = "Enable";
            }
        }

        #endregion

    }
}
