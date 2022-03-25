using System;
using System.Diagnostics;
using Gameball.PageModels;

namespace Gameball.Services
{
    public interface INavigationService
    {
        Task Close();
        Task NavigateToPage<TPage>() where TPage : Page;
        Task NavigateToPage<TPage, TParameter>(TParameter param) where TPage : Page;
    }

    public class NavigationService : INavigationService
    {
        #region Fields

        private readonly IServiceProvider _services;

        #endregion Fields


        #region Constructors

        public NavigationService(IServiceProvider services)
        {
            _services = services;
        } 

        #endregion Constructors


        #region Properties

        protected INavigation Navigation
        {
            get
            {
                var navigation = Application.Current?.MainPage?.Navigation;
                if (navigation is not null)
                    return navigation;
                else
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw new Exception();
                }
            }
        }

        #endregion Properties

        #region Public Methods

        public Task Close()
        {
            if (Navigation.NavigationStack.Count > 1)
                return Navigation.PopAsync();

            //todo maybe better change that to better approach 
            throw new InvalidOperationException("No pages to navigate back to!");
        }

        public async Task NavigateToPage<TPage>() where TPage : Page
        {
            var toPage = ResolvePage<TPage>();

            if (toPage is not null)
            {
                //Subscribe to the toPage's NavigatedTo event
                toPage.NavigatedTo += Page_NavigatedTo;

                //Get VM of the toPage
                var toViewModel = GetPageViewModelBase(toPage);

                //Call navigatingTo on VM, passing in the paramter
                if (toViewModel is not null)
                    await toViewModel.OnNavigatingTo();

                //Navigate to requested page
                await Navigation.PushAsync(toPage, true);

                //Subscribe to the toPage's NavigatedFrom event
                toPage.NavigatedFrom += Page_NavigatedFrom;
            }
            else
                throw new InvalidOperationException($"Unable to resolve type {typeof(TPage).FullName}");
        }

        public async Task NavigateToPage<TPage, TParameter>(TParameter param) where TPage : Page
        {
            var toPage = ResolvePage<TPage>();

            if (toPage is not null)
            {
                //Subscribe to the toPage's NavigatedTo event
                toPage.NavigatedTo += Page_NavigatedTo;

                //Get VM of the toPage
                var toViewModel = GetPageViewModelBase<TParameter>(toPage);

                //Call navigatingTo on VM, passing in the paramter
                if (toViewModel is not null)
                {
                    await toViewModel.OnNavigatingTo(param);
                    //support backward events compatibility
                    await toViewModel.OnNavigatingTo();
                }
                //Navigate to requested page
                await Navigation.PushAsync(toPage, true);

                //Subscribe to the toPage's NavigatedFrom event
                toPage.NavigatedFrom += Page_NavigatedFrom;
            }
            else
                throw new InvalidOperationException($"Unable to resolve type {typeof(TPage).FullName}");
        }

        #endregion Public Methods

        #region Private Methods

        private async void Page_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {
            //To determine forward navigation, we look at the 2nd to last item on the NavigationStack
            //If that entry equals the sender, it means we navigated forward from the sender to another page
            bool isForwardNavigation = Navigation.NavigationStack.Count > 1
                && Navigation.NavigationStack[^2] == sender;

            if (sender is Page thisPage)
            {
                if (!isForwardNavigation)
                {
                    thisPage.NavigatedTo -= Page_NavigatedTo;
                    thisPage.NavigatedFrom -= Page_NavigatedFrom;
                }

                await CallNavigatedFrom(thisPage, isForwardNavigation);
            }
        }


        private Task CallNavigatedFrom(Page page, bool isForward)
        {
            var fromViewModel = GetPageViewModelBase(page);

            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedFrom(isForward);
            return Task.CompletedTask;
        }

        private async void Page_NavigatedTo(object sender, NavigatedToEventArgs e)
          => await CallNavigatedTo(sender as Page);

        private Task CallNavigatedTo(Page page)
        {
            var fromViewModel = GetPageViewModelBase(page);

            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedTo();
            return Task.CompletedTask;
        }

        private BasePageModel GetPageViewModelBase(Page page)
           => page?.BindingContext as BasePageModel;

        private BasePageModel<TParam> GetPageViewModelBase<TParam>(Page page)
           => page?.BindingContext as BasePageModel<TParam>;

        private TPage ResolvePage<TPage>() where TPage : Page
            => _services.GetService<TPage>();

        #endregion Private Methods
    }
}

