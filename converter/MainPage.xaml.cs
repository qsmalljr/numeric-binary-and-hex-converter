using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace converter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            errorMessage.Text = String.Empty;
            convert();
        }

        //this will call proper converter method based on combobox selections
        private void convert()
        {
            switch (inputSelection.SelectedIndex)
            {
                //decimal
                case 0:
                    switch (outputSelection.SelectedIndex)
                    {
                        //decimal to decimal
                        case 0:
                            output.Text = input.Text;
                            break;
                        //decimal to binary
                        case 1:
                            decimalToBinary(input.Text);
                            break;
                        //decimal to hex
                        case 2:
                            decimalToHex(input.Text);
                            break;
                    }
                    break;
                //binary
                case 1:
                    switch (outputSelection.SelectedIndex)
                    {
                        //binary to decimal
                        case 0:
                            binaryToDecimal(input.Text);
                            break;
                        //binary to binary
                        case 1:
                            output.Text = input.Text;
                            break;
                        //binary to hex
                        case 2:
                            binaryToHex(input.Text);
                            break;
                    }
                    break;
                //hex
                case 2:
                    switch (outputSelection.SelectedIndex)
                    {
                        //hex to decimal
                        case 0:
                            hexToDecimal(input.Text);
                            break;
                        //hex to binary
                        case 1:
                            hexToBinary(input.Text);
                            break;
                        case 2:
                            output.Text = input.Text;
                            break;
                    }
                    break;
            }
        }

        //checks to make sure the string is only 1's and 0's
        //converts to an int and updates output box
        private void binaryToDecimal(String inputString)
        {
            //1 and 0 checker
            foreach (char letter in inputString)
            {
                if(letter != '0' && letter != '1')
                {
                    errorMessage.Text = "Improper binary format: Please check your input.";
                    return;
                }
            }

            //convert
            output.Text = Convert.ToInt32(inputString, 2).ToString();

        }

        //check to make sure it is numeric
        //then convert and update output box
        private void decimalToBinary(String inputString)
        {
            int dec;
            if(!int.TryParse(inputString, out dec))
            {
                errorMessage.Text = "Improper decimal format: Please check your input.";
                return;
            }

            output.Text = Convert.ToString(dec, 2);

        }


        //check to make sure in hexidecimal format
        //converts to decimal and updates output box
        private void hexToDecimal(String inputString)
        {
            int result;
            bool valid = int.TryParse(inputString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);

            if (!valid)
            {
                valid = inputString.Length > 2 && inputString.Substring(0, 2).ToLowerInvariant() == "0x" && int.TryParse(inputString.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
                if (valid)
                {
                    output.Text = result.ToString();
                    return;
                }
                else
                {
                    errorMessage.Text = "Improper hex format: Please check input";
                    return;
                }
            }
            else
            {
                output.Text = result.ToString();
                return;
            }
        }

        //check for proper decimal format first
        //convert from decimal number to hex number
        private void decimalToHex(String inputString)
        {
            int dec;
            if (int.TryParse(inputString, out dec))
            {
                output.Text = "0x" + dec.ToString("X");
                return;
            }
            else
            {
                errorMessage.Text = "Improper decimal format: Please check input";
            }
        }

        //check for proper binary format, convert to decimal then hex
        private void binaryToHex(String inputString)
        {
            //1 and 0 checker
            foreach (char letter in inputString)
            {
                if (letter != '0' && letter != '1')
                {
                    errorMessage.Text = "Improper binary format: Please check your input.";
                    return;
                }
            }

            int dec = Convert.ToInt32(inputString, 2);

            output.Text = "0x" + dec.ToString("X");
        }

        //check for proper hex format, convert hex to decimal to binary
        private void hexToBinary(String inputString)
        {
            int dec;
            //this checks for hex without 0x at beginning
            bool valid = int.TryParse(inputString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dec);

            if (!valid)
            {
                //if 0x is at beginning, this will return true
                valid = inputString.Length > 2 && inputString.Substring(0, 2).ToLowerInvariant() == "0x" && int.TryParse(inputString.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dec);
                if (!valid)
                {
                    errorMessage.Text = "Improper hex format: Please check input";
                    return;
                }
            }

            //convert decimal to binary
            output.Text = Convert.ToString(dec, 2);

        }
    }
}
