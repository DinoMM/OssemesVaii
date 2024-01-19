using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using OSsemes.Shared.UniversalComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.UniversalComponents
{
    /// <summary>
    /// Testy pre komponent FastFormModal
    /// </summary>
    public class TFastFormModal : TestContext
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
            var cut = RenderComponent<FastFormModal>(par => par
            .Add(p => p.Header, str)
            .Add(p => p.Inputs, new List<(string, string)>() { ("Nazov1", "string") }));
            Assert.NotNull(cut);

            var headLabel = cut.Find("#exampleHModalLabel");
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
            var cut = RenderComponent<FastFormModal>(par => par.Add(p => p.Inputs, new List<(string, string)>() { ("Nazov1", "string") }));
            Assert.NotNull(cut);

            foreach (var item in str)
            {
                cut.SetParametersAndRender(parameters => parameters.Add(p => p.Header, item));
                Assert.Equal(cut.Find("#exampleHModalLabel").TextContent, item);
            }
        }
        #endregion
        #region Group 2
        /// <summary>
        /// 1.Vytvorenie komponentu
        /// 2.Kliknutie na button OK
        /// 3.Overenie spustenia funkcie na kliknutie
        /// </summary>
        [Fact]
        public async void Test_OnClickPoslat()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            var triggered = false;
            var cut = RenderComponent<FastFormModal>(p => p
            .Add(p => p.FuncOnSucc, () => { triggered = true; })
            .Add(p => p.Inputs, new List<(string, string)>() { ("Nazov1", "string") }));
            Assert.NotNull(cut);

            await cut.Instance.OpenModal();
            cut.Find("button.btn-primary").Click();
            Assert.True(triggered);
        }
        #endregion
        #region Group 3
        /// <summary>
        /// 1.Vytvorenie komponentu, pridanie poľa s typami inputov, ich názvy sú ROZDIELNE
        /// 2.Overenie nájdených labelov a inputov so správnym id a typom, použité sú:
        ///     - string
        ///     - number
        ///     - textarea
        ///     - bool
        /// </summary>
        [Fact]
        public async void Test_Inputs1()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            List<(string, string)> insertedList = new List<(string, string)>() { ("Nazov1", "string"), ("Nazov2", "number"), ("Nazov3", "textArea"), ("Nazov4", "bool"), ("Nazov5", "email") };
            var cut = RenderComponent<FastFormModal>(p => p.Add(p => p.Inputs, insertedList));
            Assert.NotNull(cut);
            
            await cut.Instance.OpenModal();
            foreach (var item in insertedList)
            {
                string buildIdLabel = "";
                string buildIdInput = "";
                string buildInputType = "";
                switch (item.Item2)
                {
                    case "number":
                        buildIdLabel = "labelnumber" + insertedList.IndexOf(item);
                        buildIdInput = "inputnumber" + insertedList.IndexOf(item);
                        buildInputType = "number";
                        break;
                    case "string":
                        buildIdLabel = "labelstring" + insertedList.IndexOf(item);
                        buildIdInput = "inputstring" + insertedList.IndexOf(item);
                        buildInputType = "text";
                        break;
                    case "textArea":
                        buildIdLabel = "labeltextarea" + insertedList.IndexOf(item);
                        buildIdInput = "inputtextarea" + insertedList.IndexOf(item);
                        buildInputType = "textarea";
                        break;
                    case "email":
                        buildIdLabel = "labelemail" + insertedList.IndexOf(item);
                        buildIdInput = "inputemail" + insertedList.IndexOf(item);
                        buildInputType = "email";
                        break;
                    case "bool":
                        buildIdLabel = "labelbool" + insertedList.IndexOf(item);
                        buildIdInput = "inputbool" + insertedList.IndexOf(item);
                        buildInputType = "checkbox";
                        break;
                    default: break;
                }
                Assert.Equal(cut.Find("#" + buildIdLabel).TextContent, item.Item1);
                Assert.NotNull(cut.Find("#" + buildIdInput));
                var typeElement = cut.Find("#" + buildIdInput).GetAttribute("type");
                if (typeElement != null)
                {
                    Assert.Equal(typeElement, buildInputType);
                }
                else {
                    Assert.NotNull(cut.Find($"textarea#{buildIdInput}"));
                }
            }
        }
        /// <summary>
        /// 1.Vytvorenie komponentu, pridanie poľa s typami inputov, ich názvy sú ROVNAKĚ
        /// 2.Overenie nájdených labelov a inputov so správnym id a typom, použité sú:
        ///     - string
        ///     - number
        ///     - textarea
        ///     - bool
        /// </summary>
        [Fact]
        public async void Test_Inputs2()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            List<(string, string)> insertedList = new List<(string, string)>() { ("Nazov1", "string"), ("Nazov1", "number"), ("Nazov1", "textArea"), ("Nazov1", "bool") };
            var cut = RenderComponent<FastFormModal>(p => p.Add(p => p.Inputs, insertedList));
            Assert.NotNull(cut);

            await cut.Instance.OpenModal();
            foreach (var item in insertedList)
            {
                string buildIdLabel = "";
                string buildIdInput = "";
                string buildInputType = "";
                switch (item.Item2)
                {
                    case "number":
                        buildIdLabel = "labelnumber" + insertedList.IndexOf(item);
                        buildIdInput = "inputnumber" + insertedList.IndexOf(item);
                        buildInputType = "number";
                        break;
                    case "string":
                        buildIdLabel = "labelstring" + insertedList.IndexOf(item);
                        buildIdInput = "inputstring" + insertedList.IndexOf(item);
                        buildInputType = "text";
                        break;
                    case "textArea":
                        buildIdLabel = "labeltextarea" + insertedList.IndexOf(item);
                        buildIdInput = "inputtextarea" + insertedList.IndexOf(item);
                        buildInputType = "textarea";
                        break;
                    case "bool":
                        buildIdLabel = "labelbool" + insertedList.IndexOf(item);
                        buildIdInput = "inputbool" + insertedList.IndexOf(item);
                        buildInputType = "checkbox";
                        break;
                    default: break;
                }
                Assert.Equal(cut.Find("#" + buildIdLabel).TextContent, item.Item1);
                Assert.NotNull(cut.Find("#" + buildIdInput));
                var typeElement = cut.Find("#" + buildIdInput).GetAttribute("type");
                if (typeElement != null)
                {
                    Assert.Equal(typeElement, buildInputType);
                }
                else
                {
                    Assert.NotNull(cut.Find($"textarea#{buildIdInput}"));
                }
            }
        }
        /// <summary>
        /// 1.Vytvorenie komponentu, pridanie poľa s typami inputov, ich názvy sú PRAZDNY string
        /// 2.Overenie nájdených labelov a inputov so správnym id a typom, použité sú:
        ///     - string
        ///     - number
        ///     - textarea
        ///     - bool
        /// </summary>
        [Fact]
        public async void Test_Inputs3()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            List<(string, string)> insertedList = new List<(string, string)>() { ("", "string"), ("", "number"), ("", "textArea"), ("", "bool") };
            var cut = RenderComponent<FastFormModal>(p => p.Add(p => p.Inputs, insertedList));
            Assert.NotNull(cut);

            await cut.Instance.OpenModal();
            foreach (var item in insertedList)
            {
                string buildIdLabel = "";
                string buildIdInput = "";
                string buildInputType = "";
                switch (item.Item2)
                {
                    case "number":
                        buildIdLabel = "labelnumber" + insertedList.IndexOf(item);
                        buildIdInput = "inputnumber" + insertedList.IndexOf(item);
                        buildInputType = "number";
                        break;
                    case "string":
                        buildIdLabel = "labelstring" + insertedList.IndexOf(item);
                        buildIdInput = "inputstring" + insertedList.IndexOf(item);
                        buildInputType = "text";
                        break;
                    case "textArea":
                        buildIdLabel = "labeltextarea" + insertedList.IndexOf(item);
                        buildIdInput = "inputtextarea" + insertedList.IndexOf(item);
                        buildInputType = "textarea";
                        break;
                    case "bool":
                        buildIdLabel = "labelbool" + insertedList.IndexOf(item);
                        buildIdInput = "inputbool" + insertedList.IndexOf(item);
                        buildInputType = "checkbox";
                        break;
                    default: break;
                }
                Assert.Equal(cut.Find("#" + buildIdLabel).TextContent, item.Item1);
                Assert.NotNull(cut.Find("#" + buildIdInput));
                var typeElement = cut.Find("#" + buildIdInput).GetAttribute("type");
                if (typeElement != null)
                {
                    Assert.Equal(typeElement, buildInputType);
                }
                else
                {
                    Assert.NotNull(cut.Find($"textarea#{buildIdInput}"));
                }
            }
        }
        #endregion
        /// <summary>
        /// 1.Vytvorenie komponentu, pridanie poľa s typami inputov, Názvy rôzne a zlé typy, ošetrenie prázdnej funkcie
        /// 2.Overenie nájdených labelov a inputov so správnym id a typom, použité sú:
        ///     - string
        ///     - number
        ///     - textarea
        ///     - bool
        /// </summary>
        [Fact]
        public async void Test_Inputs4()
        {
            var jsRuntimeMock = new Mock<IJSRuntime>();     //inject pre funkcnost javascriptu
            Services.AddSingleton(jsRuntimeMock.Object);

            var triggered = false;
            List<(string, string)> insertedList = new List<(string, string)>() { ("Nazov1", "xxx"), ("", ""), ("Nazov1", "Ako Sa"), ("Nazov4", "neviem\nco\nRobim") };
            var cut = RenderComponent<FastFormModal>(p => p
            .Add(p => p.Inputs, insertedList)
            .Add(p => p.FuncOnSucc, () => { triggered = true; })
            );
            Assert.NotNull(cut);

            await cut.Instance.OpenModal();
            Assert.True(cut.Instance.FuncOnSucc.Equals(EventCallback.Empty));
            foreach (var item in insertedList)
            {
                string buildIdLabel = "";
                string buildIdInput = "";
                string buildInputType = "";
                switch (item.Item2)
                {
                    case "number":
                        buildIdLabel = "labelnumber" + insertedList.IndexOf(item);
                        buildIdInput = "inputnumber" + insertedList.IndexOf(item);
                        buildInputType = "number";
                        break;
                    case "string":
                        buildIdLabel = "labelstring" + insertedList.IndexOf(item);
                        buildIdInput = "inputstring" + insertedList.IndexOf(item);
                        buildInputType = "text";
                        break;
                    case "textArea":
                        buildIdLabel = "labeltextarea" + insertedList.IndexOf(item);
                        buildIdInput = "inputtextarea" + insertedList.IndexOf(item);
                        buildInputType = "textarea";
                        break;
                    case "bool":
                        buildIdLabel = "labelbool" + insertedList.IndexOf(item);
                        buildIdInput = "inputbool" + insertedList.IndexOf(item);
                        buildInputType = "checkbox";
                        break;
                    default:
                        buildIdLabel = "labelerror" + insertedList.IndexOf(item);
                        break;
                }
                var wrongstr = cut.Find("#" + buildIdLabel).TextContent.Trim();
                Assert.Equal("Chyba", wrongstr.Split(" ")[0]);
            }
        }
    }
}
