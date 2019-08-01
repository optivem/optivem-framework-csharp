﻿using Optivem.Framework.Infrastructure.Selenium;
using Optivem.Template.Web.UI.Client.Interface.Pages;
using System;

namespace Optivem.Template.Web.UI.Client.Pages
{
    public class CreateCustomerPage : Page, ICreateCustomerPage
    {
        // TODO: VC: Base should be configurable due to localhost vs test etc.

        public CreateCustomerPage(Driver driver, bool navigateTo = false) 
            : base(driver, "https://localhost:44379/customers/create", navigateTo)
        {
        }

        private Button CreateButton => Finder.FindButton(FindBy.Id("create-button"));

        private TextBox FirstNameTextBox => Finder.FindTextBox(FindBy.Id("first-name"));

        private TextBox LastNameTextBox => Finder.FindTextBox(FindBy.Id("last-name"));

        private Element ErrorMessageElement => Finder.FindElement(FindBy.Id("error"));

        public void ClickCreate()
        {
            CreateButton.Click();
        }

        public void InputFirstName(string firstName)
        {
            FirstNameTextBox.InputText(firstName);
        }

        public void InputLastName(string lastName)
        {
            LastNameTextBox.InputText(lastName);
        }

        public string ReadErrorMessage()
        {
            return ErrorMessageElement.Text;
        }
    }
}