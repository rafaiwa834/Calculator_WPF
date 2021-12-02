using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZadanieRekrutacyjne_Kalkulator.Commands;
using System.Windows.Input;
using System.Windows;
using System.Data;

namespace ZadanieRekrutacyjne_Kalkulator.ViewModels
{
    public class MainWindowViewModel :  INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> operations = new List<string>{"x","+","-","/"};
        private string _screenVal;
        private string _screenValUp;
        private string _screenSavedVal;
        private string _screenLastVal;
        private bool inValidData = false;
        private DataTable dataTable = new DataTable();
        public string ScreenValUp
        {
            get { return _screenValUp; }
            set
            {
                _screenValUp = value;
                OnPropertyChanged();
            }
        }
        public string ScreenVal 
        { 
            get { return _screenVal;} 
            set { _screenVal = value;
                 OnPropertyChanged(); } 
        }
        public string ScreenSavedVal
        { 
            get { return _screenSavedVal; } 
            set {
                _screenSavedVal = value;
                 OnPropertyChanged(); } 
        }
        public string ScreenLastVal
        { 
            get { return _screenLastVal; } 
            set {
                _screenLastVal = value;
                 OnPropertyChanged(); } 
        }
        public ICommand AddNumberCommand { get; set; }
        public ICommand OperationCommand { get; set; }
        public ICommand ClearScreenCommand { get; set; }
        public ICommand GetResultCommand { get; set; }
        public ICommand ChangeSignCommand { get; set; }
        public ICommand ClearInputScreenCommand { get; set; }
        public ICommand DeleteLastNumberCommand { get; set; }
        public ICommand SquareRootCommand { get; set; }
        public ICommand PowerCommand { get; set; }
        public ICommand InverseCommand { get; set; }
        public ICommand DisplaySavedValCommand { get; set; }
        public ICommand AddToSavedValCommand { get; set; }
        public ICommand SubWithSavedValCommand { get; set; }
        public ICommand SaveValueCommand { get; set; }
        public ICommand DeleteLastValCommand { get; set; }

        public MainWindowViewModel()
        {
            ScreenVal = "0";
            ScreenValUp = "";
            AddNumberCommand = new RelayCommand(AddNumber);
            OperationCommand = new RelayCommand(Operation, CanUse);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            ClearInputScreenCommand = new RelayCommand(ClearInputScreen);
            DeleteLastNumberCommand = new RelayCommand(DeleteLastNumber, CanUse);
            GetResultCommand = new RelayCommand(GetResult, CanUse);
            ChangeSignCommand = new RelayCommand(ChangeSign, CanUse);
            SquareRootCommand = new RelayCommand(SquareRoot, CanUse);
            PowerCommand = new RelayCommand(Power, CanUse);
            InverseCommand = new RelayCommand(Inverse, CanUse);
            DisplaySavedValCommand = new RelayCommand(DisplaySavedVal, CanUse);
            AddToSavedValCommand = new RelayCommand(AddToSavedVal, CanUse);
            SubWithSavedValCommand = new RelayCommand(SubFromSavedVal, CanUse);
            SaveValueCommand = new RelayCommand(SaveValue, CanUse);
            DeleteLastValCommand = new RelayCommand(DeleteLastVal);
        }

        private bool CanUse(object obj) => !inValidData;

        public void DeleteLastVal(object obj)
        {
            ScreenLastVal = string.Empty;
        }

        public void SubFromSavedVal(object obj)
        {
            var result = dataTable.Compute((ScreenSavedVal + "-" + ScreenVal).Replace(",","."), "");
            ScreenSavedVal = result.ToString();
        }

        public void AddToSavedVal(object obj)
        {
            var result = dataTable.Compute((ScreenSavedVal + "+" + ScreenVal).Replace(",", "."), "");
            ScreenSavedVal = result.ToString();
        }

        public void DisplaySavedVal(object obj)
        {
            ScreenVal = ScreenSavedVal;
        }
        public void SaveValue(object obj)
        {
             ScreenSavedVal = ScreenVal;
        }

        public void AddNumber(object obj)
        {
            var number = obj as string;
            if (inValidData)
            {
                ScreenVal = string.Empty;
                inValidData = false;
                if (number == ",")
                    number = "0,";
            }
            else if (ScreenVal == "0" && number != ",")
                ScreenVal = string.Empty;
            else if (number == "," && ScreenVal == "")
                number = "0,";
            else if (number == "," && ScreenVal.Contains(","))
                number = "";
            else if (number == "," && operations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
                number = "0,";

            if (ScreenValUp != null)
            {
                if (ScreenValUp.Contains("="))
                {
                    ScreenValUp = "";
                    ScreenVal = string.Empty;
                    if (number == ",")
                        number = "0,";
                }
            }

            ScreenVal += number;
        }

        public void Operation(object obj)
        {
            var operation = obj as string;
            object result;
            
            if (ScreenValUp != string.Empty )
            {
                if (ScreenValUp.Contains("="))
                {
                    ScreenValUp = ScreenVal + operation;
                }
                else
                {
                    if (float.Parse(ScreenVal) == 0 && ScreenValUp.Contains("/"))
                    {
                        ScreenValUp = string.Empty;
                        ScreenVal = "Nie dzielimy przez 0";
                        inValidData = true;
                    }
                    else
                    {
                        result = dataTable.Compute((ScreenValUp + ScreenVal).Replace(",", "."), "");
                        ScreenValUp = result.ToString() + operation;
                        ScreenLastVal = result.ToString();
                    }
                }
            }
            else if (!operations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)) && ScreenVal.Substring(ScreenVal.Length - 1) != ",")
            {
                ScreenValUp = ScreenVal + operation;
            }

            if(!inValidData)
                ScreenVal = "0";

        }
        public void ClearScreen(object obj)
        {
            ScreenVal = "0";
            ScreenValUp = "";
            inValidData = false;
        }
        public void ClearInputScreen(object obj)
        {
            ScreenVal = "0";
            if (ScreenValUp.Contains("="))
                ScreenValUp = string.Empty;
            inValidData = false;
        }

        public void DeleteLastNumber(object obj)
        {
            if (ScreenVal.Length > 2 || (ScreenVal.Length == 2 && !ScreenVal.Contains("-")))
                ScreenVal = ScreenVal.Remove(ScreenVal.Length - 1);
            else if ((ScreenVal.Length == 2 && ScreenVal.Contains("-")) || (ScreenVal.Length == 1 && !ScreenVal.Contains("0")))
                ScreenVal = "0";
           
        }

        public void ChangeSign(object obj)
        {
            if (!ScreenVal.Contains("-"))
            {
                ScreenVal = "-" + ScreenVal;
            }
            else
            {
                ScreenVal = ScreenVal.Substring(1);
            }
        }

        public void SquareRoot(object obj)
        {
            if (ScreenVal.Contains("-"))
            {
                ScreenVal = "Nieprawidłowe dane";
                inValidData = true;
                ScreenValUp = string.Empty;
            }
            else
            {
                var value = float.Parse(ScreenVal);
                ScreenVal = Math.Sqrt(value).ToString();
            }
        }

        public void Power(object obj)
        {
            var value = float.Parse(ScreenVal);
            ScreenVal = Math.Pow(value,2).ToString();
        }

        public void Inverse(object obj)
        {
            var value = float.Parse(ScreenVal);
            ScreenVal =(1/value).ToString();
        }

        public void GetResult(object obj)
        {
            if (ScreenValUp.Contains("/") && float.Parse(ScreenVal) == 0)
            {
                ScreenValUp = string.Empty;
                ScreenVal = "Nie dzielimy przez 0";
                inValidData = true;
            }
            else if (!ScreenValUp.Contains("="))
            {
                ScreenVal = ScreenValUp + ScreenVal;
                var result = dataTable.Compute(ScreenVal.Replace(",", "."), "");
                ScreenValUp = ScreenVal + "=";
                ScreenVal = result.ToString();
                ScreenLastVal = ScreenVal;
            }
        }


        private void OnPropertyChanged([CallerMemberName] string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

    }
}
