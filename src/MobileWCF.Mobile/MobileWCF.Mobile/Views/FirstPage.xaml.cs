using MobileWCF.Contracts;
using System;
using System.Diagnostics;
using System.ServiceModel;
using Xamarin.Forms;

namespace MobileWCF.Mobile.Views
{
    public partial class FirstPage : ContentPage
    {
        public FirstPage()
        {
            InitializeComponent();
            b1.Clicked += OnButtonClicked;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string strAddress = "http://192.168.106.164:9003/CalculatorService";
                BasicHttpBinding httpBinding = new BasicHttpBinding();
                EndpointAddress address = new EndpointAddress(strAddress);

                ChannelFactory<ICalculatorService> channel =
                    new ChannelFactory<ICalculatorService>(httpBinding, address);

                var calculator = channel.CreateChannel(address);
                var num1 = Convert.ToInt32(eNum1.Text);
                var num2 = Convert.ToInt32(eNum2.Text);
                calculator.BeginGetSum(num1, num2, Callback, calculator);
            }
            catch (Exception ex)
            {
                lResult.Text = ex.Message;
                Debug.WriteLine(ex);
            }
        }

        private void Callback(IAsyncResult result)
        {
            var msg = "";
            var res = result.AsyncState as ICalculatorService;
            if (res != null)
                msg = res.EndGetSum(result);

            Device.BeginInvokeOnMainThread(() => lResult.Text = msg);
        }
    }
}
