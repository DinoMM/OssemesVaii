using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.JSInterop;
using OSsemes.Shared.UniversalComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Components.UniversalComponents
{
    /// <summary>
    /// Testy pre komponent InfoModal
    /// </summary>
    public class TInfoModal : TestContext
    {

        #region Group 1
        /// <summary>
        /// 1.Vytvorenie komponentu, Správne pridelenenie do Header
        /// 2.Nájdenie Header labela z komponentu
        /// 3.Porovnanie správnej hodnoty
        /// </summary>
        [Fact]
        public void Test_Header1()
        {
            var str = "TestString";
            var cut = RenderComponent<InfoModal>(par => par.Add(p => p.Header, str));
            Assert.NotNull(cut);

            var headLabel = cut.Find("#exampleModalLabel");
            Assert.NotNull(headLabel);

            Assert.Equal(headLabel.TextContent, str);
        }

        /// <summary>
        /// 1.Vytvorenie komponentu
        /// 2.Menenie vypisovanej hodnoty a porovnanie správnej hodnoty
        /// </summary>
        [Fact]
        public void Test_Header2()
        {
            var str = new string[] { "TestString1", "TestString2", "TestString3" };
            var cut = RenderComponent<InfoModal>();
            Assert.NotNull(cut);

            foreach (var item in str)
            {
                cut.SetParametersAndRender(parameters => parameters.Add(p => p.Header, item));
                Assert.Equal(cut.Find("#exampleModalLabel").TextContent, item);
            }
        }
        #endregion
        #region Group 2
        /// <summary>
        /// 1.Vytvorenie komponentu
        /// 2.Menenie vypisovanej hodnoty a porovnanie správnej hodnoty.
        ///   Ošetrenie spravného vypísaného stringu aj s html značkami
        /// </summary>
        [Fact]
        public void Test_Text1()
        {
            var str = new string[] { "TestString", "<p>TestString</p>", "TestString<br>" };
            var samestr = str[0];
            var cut = RenderComponent<InfoModal>();
            Assert.NotNull(cut);

            foreach (var item in str)
            {
                cut.SetParametersAndRender(parameters => parameters.Add(p => p.Text, item));
                Assert.Equal(cut.Find("#modalTextLabel").TextContent, samestr);
            }
        }
        #endregion
        #region Group 3
        /// <summary>
        /// 1.Vytvorenie komponentu
        /// 2.Kliknutie na button OK
        /// 3.Overenie spustenia funkcie na kliknutie
        /// </summary>
        [Fact]
        public async void Test_OnClickOK()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            var triggered = false;
            var cut = RenderComponent<InfoModal>(p => p.Add(p => p.FuncAction, () => { triggered = true; }));
            Assert.NotNull(cut);

            await cut.Instance.OpenModal();
            cut.Find("button").Click();
            Assert.True(triggered);
        }
        #endregion
    }

}

