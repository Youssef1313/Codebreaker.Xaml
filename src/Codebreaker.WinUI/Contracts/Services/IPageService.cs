namespace CodeBreaker.WinUI.Contracts.Services;

public interface IPageService
{
    /// <summary>
    /// Get the page type by specifying the type name of the corresponding viewmodel.
    /// </summary>
    /// <param name="viewModelKey">The type name of the corresponding viewmodel</param>
    /// <returns>The page type.</returns>
    /// <exception cref="ArgumentException">Thrown if the page type is not found.</exception>
    Type GetPageTypeByViewModel(string viewModelKey);

    /// <summary>
    /// Get the page type by specifying the type of the corresponding viewmodel.
    /// </summary>
    /// <param name="viewModelType">Type tyoe of the corresponding viewmodel.</param>
    /// <returns>The page tyoe.</returns>
    /// <exception cref="ArgumentException">Thrown if the page type is not found.</exception>
    Type GetPageTypeByViewModel(Type viewModelType);

    /// <summary>
    /// Get the page type by specifying the name of the page itself.
    /// </summary>
    /// <param name="pageTypeName">The name of the page.</param>
    /// <returns>The page type.</returns>
    /// <exception cref="ArgumentException">Thrown if the page type is not found.</exception>
    Type GetPageTypeByPageName(string pageTypeName);
}
