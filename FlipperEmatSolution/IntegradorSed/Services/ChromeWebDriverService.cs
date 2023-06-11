using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Drawing;

namespace Emat.IntegracaoSedConsoleApp.Services
{
	public class ChromeWebDriverService
	{
		private readonly IWebDriver _webDriver;

		public ChromeWebDriverService()
		{
			//var chromeOptions = new ChromeOptions();
			//chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
			//chromeOptions.AddArgument("no-sandbox");

			//_webDriver = new ChromeDriver(chromeOptions);

			_webDriver = Create();

			////_webDriver.Manage().Window.Maximize();

			//_webDriver.Manage().Window.Position = new Point(0, 0);
			//_webDriver.Manage().Window.Size = new Size(960, 1080);
		}

		public static IWebDriver Create()
		{
			IWebDriver driver = new ChromeDriver();
			//driver.Manage().Window.Maximize();
			driver.Manage().Window.Position = new Point(0, 0);
			driver.Manage().Window.Size = new Size(960, 1080);

			return driver;
		}

		public static IWebDriver CreateHeadless()
		{
			ChromeOptions options = new ChromeOptions();

			options.AddArgument("headless");
			options.AddArgument("no-sandbox");
			options.AddArgument("window-size=1920x1080");

			IWebDriver driver = new ChromeDriver(options);

			return driver;
		}

		public void NavigateToUrl(string url)
		{
			try
			{
				_webDriver.Navigate().Refresh();
				_webDriver.Navigate().GoToUrl(url);
			}
			catch (Exception ex) { Log.Error(ex.Message); throw; }
		}
		public void SendKeysById(string id, string text)
		{
			try
			{
				//TODO:: Verificar se aqui irá ter a verificação dentro de um loop se o texto foi escrito...

				var clickableWebElement = WebElementClickable(id, TimeSpan.FromSeconds(10));

				clickableWebElement.SendKeys(text + Keys.Tab); //TODO> Verificar depois para melhorar esta ação "Tab" 
			}
			catch (NoSuchElementException ex)
			{
				Log.Error(ex.Message);
				throw;
			}
			catch (ElementClickInterceptedException ex)
			{
				Log.Error(ex.Message);
				throw;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}
		public IWebElement WebElementClickable(string elementId, TimeSpan timeSpan)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, timeSpan);

				var clickableWebElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(elementId)));

				return clickableWebElement;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public bool CheckElementText(IWebElement webElement, string text, TimeSpan timeout)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, timeout);

				var textIsPresent = wait.Until(ExpectedConditions.TextToBePresentInElement(webElement, text));

				return textIsPresent;

			}
			catch (Exception)
			{

				throw;
			}
		}
		public void ClickById(string id)
		{
			try
			{
				var clickableWebElement = WebElementClickable(id, TimeSpan.FromSeconds(10));

				clickableWebElement.Click();
			}
			catch (ElementClickInterceptedException ex)
			{
				Log.Error(ex.Message);
				throw;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}

		public void ClickLinkText(string linkText)
		{
			var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
			var webElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
			webElement.Click();
		}

		public void ExplicitWait(string elementId, TimeSpan timeout)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, timeout);

				wait.Until(e => e.FindElement(By.Id(elementId)));
			}
			catch (NoSuchElementException ex)
			{
				Log.Error(ex.Message);
				throw;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}
		public void SelectByText(string elementId, string optionText, string? elementCssSelector = null)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
				IWebElement webElement = wait.Until(e => e.FindElement(By.Id(elementId)));
				//IWebElement webElement = wait.Until(ExpectedConditions.ElementToBeSelected(By.Id(elementId)));

				if (elementCssSelector != null)
					wait.Until(e => e.FindElement(By.CssSelector(elementCssSelector)));

				var select = new SelectElement(webElement);
				IList<IWebElement> optionList = select.Options;
				select.SelectByText(optionText);
			}
			catch (NoSuchElementException ex)
			{
				Log.Error(ex.Message);
				throw;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}
		
		public void SelecionarOpcaoPeloTexto(string elementId, string optionText, TimeSpan timeSpan)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, timeSpan);
				var webElement = wait.Until(ExpectedConditions.ElementExists(By.Id(elementId)));
				if (webElement != null)
				{
					var select = new SelectElement(webElement);
					IList<IWebElement> optionList = select.Options;
					select.SelectByText(optionText);
				}				
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}

		public void ClicarPeloTexto(string texto, TimeSpan timeSpan)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, timeSpan);
				var webElement = wait.Until(ExpectedConditions.ElementExists(By.LinkText(texto)));
				if (webElement != null)
				{
					wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(texto)));
					webElement.Click();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}

		public void ClicarPeloId(string id, TimeSpan timeSpan)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, timeSpan);
				var webElement = wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
				if (webElement != null)
				{
					Thread.Sleep(500);
					wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(id)));
					webElement.Click();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}

		public void WaitElementIsVisible(string elementId, TimeSpan timeSpan )
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, timeSpan);
				var webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(elementId)));
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}

		public void ClickByCssSelector(string cssSelector, string text = "")
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));

				IWebElement webElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(cssSelector)));

				webElement.Click();
			}
			catch (ElementClickInterceptedException ex)
			{
				Log.Error(ex.Message);
				throw;
			}
			catch (NoSuchElementException ex)
			{
				Log.Error(ex.Message);
				throw;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}
		public void ImplicitWait(TimeSpan timeout)
		{
			try
			{
				_webDriver.Manage().Timeouts().ImplicitWait = timeout;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}
		}
		public bool ElementIsDisplayed(string elementId) => _webDriver.FindElement(By.Id(elementId)).Displayed;
		public WebDriverWait ExplicitWait(int timeSpanValue)
		{
			WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeSpanValue));

			return wait;
		}

		/// <summary>
		/// Troca para o próximo windowHandle caso maior que 1.
		/// </summary>
		public void SwitchWindowHandle(string targetTitle)
		{
			Log.Information("Switching Window Handle...");
			try
			{
				//var currentTitle = _webDriver.Title;
				//var currentWindowName = _webDriver.CurrentWindowHandle;
				//var windowsNames = _webDriver.WindowHandles;

				//foreach (var windowName in windowsNames)
				//{
				//	if (windowName != currentWindowName)
				//	{
				//		var switchedWindow = _webDriver.SwitchTo().Window(windowName);

				//		var currentUrl = switchedWindow.Url;

				//		currentTitle = switchedWindow.Title;

				//		if (currentTitle == targetTitle)
				//		{
				//			break;
				//		}
				//	}
				//}

				TimeSpan timeSpan = TimeSpan.FromSeconds(10);
				WebDriverWait waiter = new WebDriverWait(_webDriver, timeSpan);
				waiter.Until(drv => drv.WindowHandles.Count == 2);

				string handle = _webDriver.WindowHandles.Last();
				_webDriver.SwitchTo().Window(handle);

				waiter.Until(driver => driver.Title.Equals(targetTitle));

				Console.WriteLine("Foi....");
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void NewWindowHandle()
		{
			try
			{
				_webDriver.SwitchTo().NewWindow(WindowType.Tab);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public string ObterCodigoFontePagina()
		{
			return _webDriver.PageSource;
		}

		public IWebDriver GetWebDriver()
		{
			return _webDriver;
		} 
	}
}