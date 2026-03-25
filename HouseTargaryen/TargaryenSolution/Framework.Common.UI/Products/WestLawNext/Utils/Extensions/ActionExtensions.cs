namespace Framework.Common.UI.Products.WestLawNext.Utils.Extensions
{
    using System;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The action extensions.
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// Do action with element until condition will fulfilled.
        /// </summary>
        /// <param name="action">
        /// The action with first element.
        /// </param>
        /// <param name="condition">
        /// The expected condition, after action.
        /// </param>
        /// <param name="numberOfAttempts">
        /// The number Of Attempts.
        /// </param>
        /// <exception cref="InvalidElementStateException">
        /// Exception throw, when conditions unattainable, after actions 
        /// </exception>
        public static void DoUntilConditionWillBecomeTrue(
            Action action,
            Func<bool> condition,
            int numberOfAttempts = 10)
        {
            bool @continue = true;

            for (int i = 0; @continue && i < numberOfAttempts; i++)
            {
                action();
                DriverExtensions.WaitForPageLoad();
                DriverExtensions.WaitForJavaScript();

                @continue = !condition();
            }

            if (@continue)
            {
                throw new InvalidElementStateException("Action with the element is not working");
            }
        }
    }
}