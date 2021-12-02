using System;
using Xunit;
using ZadanieRekrutacyjne_Kalkulator.ViewModels;

namespace ZadanieRekrutacyjne_Kalkulator.Tests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void DeleteLastVal()
        {
            //arrange
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenLastVal = "23423";

            //action
            mainWindowView.DeleteLastVal(string.Empty);

            //assert
            Assert.Equal("", mainWindowView.ScreenLastVal);
        }

        [Theory]
        [InlineData("9" ,"3" ,"6" )]
        [InlineData("9" ,"19" ,"-10" )]
        [InlineData("-7" ,"23" ,"-30" )]
        public void SubFromSavedVal_Positive(string saveVal, string val, string result)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenSavedVal = saveVal;
            mainWindowView.ScreenVal = val;

            mainWindowView.SubFromSavedVal(string.Empty);

            Assert.Equal(result, mainWindowView.ScreenSavedVal);
        }
        
        [Theory]
        [InlineData("9" ,"3" ,"2" )]
        [InlineData("9" ,"19" ,"5" )]
        [InlineData("-7" ,"23" ,"-9" )]
        public void SubFromSavedVal_Negative(string saveVal, string val, string result)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenSavedVal = saveVal;
            mainWindowView.ScreenVal = val;

            mainWindowView.SubFromSavedVal(string.Empty);

            Assert.NotEqual(result, mainWindowView.ScreenSavedVal);
        }
        
        [Theory]
        [InlineData("9" ,"3" ,"12" )]
        [InlineData("9" ,"19" ,"28" )]
        [InlineData("-7" ,"23" ,"16" )]
        public void AddToSavedVal_Positive(string saveVal, string val, string result)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenSavedVal = saveVal;
            mainWindowView.ScreenVal = val;

            mainWindowView.AddToSavedVal(string.Empty);

            Assert.Equal(result, mainWindowView.ScreenSavedVal);
        }
        [Theory]
        [InlineData("9" ,"3" ,"99" )]
        [InlineData("9" ,"19" ,"0" )]
        [InlineData("-7" ,"23" ,"3" )]
        public void AddToSavedVal_Negative(string saveVal, string val, string result)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenSavedVal = saveVal;
            mainWindowView.ScreenVal = val;

            mainWindowView.AddToSavedVal(string.Empty);

            Assert.NotEqual(result, mainWindowView.ScreenSavedVal);
        }

        [Fact]
        public void DisplaySavedVal()
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenSavedVal = "634";

            mainWindowView.DisplaySavedVal(string.Empty);

            Assert.Equal(mainWindowView.ScreenSavedVal, mainWindowView.ScreenVal);
        }

        [Theory]
        [InlineData("92", "3", "923")]
        [InlineData("0", ",", "0,")]
        [InlineData("0", "2", "2")]
        [InlineData("", ",", "0,")]
        [InlineData("0,", ",", "0,")]
        [InlineData("0,2342", ",", "0,2342")]
        public void AddNumber(string screenVal, string number, string result)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;

            mainWindowView.AddNumber(number);

            Assert.Equal(result, mainWindowView.ScreenVal);
        }

        [Theory]
        [InlineData("92", "", "+", "0", "92+")]
        [InlineData("0", "42*", "/", "0", "0/")]
        public void Operation(string screenVal, string screenUpVal, string number, string resultScreenVal, string resultScreenUpVal)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;
            mainWindowView.ScreenValUp = screenUpVal;

            mainWindowView.Operation(number);

            Assert.Equal(resultScreenUpVal, mainWindowView.ScreenValUp);
            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
        }
        
        [Fact]
        public void ClearScreen()
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = "53453";
            mainWindowView.ScreenValUp = "2341234";

            mainWindowView.ClearScreen(string.Empty);

            Assert.Equal("0", mainWindowView.ScreenVal);
            Assert.Equal(string.Empty, mainWindowView.ScreenValUp);
        }
        
        [Theory]
        [InlineData("23", "21+2=","0","")]
        [InlineData("52", "42*","0","42*")]
        public void ClearInputScreen(string screenVal, string screenValUp, string resultScreenVal, string resultScreenValUp)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;
            mainWindowView.ScreenValUp = screenValUp;

            mainWindowView.ClearInputScreen(string.Empty);

            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
            Assert.Equal(resultScreenValUp, mainWindowView.ScreenValUp);
        }
        [Theory]
        [InlineData("423", "42")]
        [InlineData("8", "0")]
        [InlineData("0,7", "0,")]
        public void DeleteLastNumber(string screenVal, string resultScreenVal)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;

            mainWindowView.DeleteLastNumber(string.Empty);

            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
        }
        [Theory]
        [InlineData("-423", "423")]
        [InlineData("8", "-8")]
        [InlineData("-0,7", "0,7")]
        public void ChangeSign(string screenVal, string resultScreenVal)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;

            mainWindowView.ChangeSign(string.Empty);

            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
        }
        [Theory]
        [InlineData("-423", "Nieprawid³owe dane")]
        [InlineData("16", "4")]
        [InlineData("0,25", "0,5")]
        public void SquareRoot(string screenVal, string resultScreenVal)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;

            mainWindowView.SquareRoot(string.Empty);

            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
        }
        [Theory]
        [InlineData("4", "16")]
        [InlineData("-1", "1")]
        public void Power(string screenVal, string resultScreenVal)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;

            mainWindowView.Power(string.Empty);

            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
        }
        [Theory]
        [InlineData("4", "0,25")]
        [InlineData("-1", "-1")]
        [InlineData("-5", "-0,2")]
        public void Inverse(string screenVal, string resultScreenVal)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;

            mainWindowView.Inverse(string.Empty);

            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
        }
        [Theory]
        [InlineData("10", "0,25*","2,50", "0,25*10=")]
        [InlineData("0", "10/", "Nie dzielimy przez 0", "")]
        [InlineData("17", "10+", "27", "10+17=")]
        public void GetResult(string screenVal, string scrennValUp, string resultScreenVal, string resultScreenValUp)
        {
            MainWindowViewModel mainWindowView = new MainWindowViewModel();
            mainWindowView.ScreenVal = screenVal;
            mainWindowView.ScreenValUp = scrennValUp;

            mainWindowView.GetResult(string.Empty);

            Assert.Equal(resultScreenVal, mainWindowView.ScreenVal);
            Assert.Equal(resultScreenValUp, mainWindowView.ScreenValUp);
        }


    }
}
