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
    /// Testy pre komponent ApproveModal
    /// </summary>
    public class TApproveModal : TestContext
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
            var cut = RenderComponent<ApproveModal>(par => par.Add(p => p.Header, str));
            Assert.NotNull(cut);

            var headLabel = cut.Find("#staticBackdropLabel");
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
            var cut = RenderComponent<ApproveModal>();
            Assert.NotNull(cut);

            foreach (var item in str)
            {
                cut.SetParametersAndRender(parameters => parameters.Add(p => p.Header, item));
                Assert.Equal(cut.Find("#staticBackdropLabel").TextContent, item);
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
            var cut = RenderComponent<ApproveModal>();
            Assert.NotNull(cut);

            foreach (var item in str)
            {
                cut.SetParametersAndRender(parameters => parameters.Add(p => p.Text, item));
                Assert.Equal(cut.Find("#modalATextLabel").TextContent, samestr);
            }
        }
        #endregion
        #region Group 3
        /// <summary>
        /// 1.Vytvorenie komponentu
        /// 2.Kliknutie na button Áno
        /// 3.Overenie spustenia funkcie na kliknutie
        /// </summary>
        [Fact]
        public async void Test_OnClickAno()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            var triggered = false;
            var cut = RenderComponent<ApproveModal>(p => p.Add(p => p.FuncOnSucc, () => { triggered = true; }));
            Assert.NotNull(cut);

            await cut.Instance.OpenModal();
            cut.Find("button.btn-primary").Click();
            Assert.True(triggered);
        }
        /// <summary>
        /// 1.Vytvorenie komponentu
        /// 2.Kliknutie na button Nie
        /// 3.Overenie spustenia funkcie na kliknutie
        /// </summary>
        [Fact]
        public async void Test_OnClickNie()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            var triggered = false;
            var cut = RenderComponent<ApproveModal>(p => p.Add(p => p.FuncOnDiss, () => { triggered = true; }));
            Assert.NotNull(cut);

            await cut.Instance.OpenModal();
            cut.Find("button.btn-secondary").Click();
            Assert.True(triggered);
        }
        #endregion
    }
}
