using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gameball.Services;

namespace Gameball.PageModels;

public partial class MainPageModel : BasePageModel
{
    #region Fields

    private readonly INavigationService _navigationService;

    #endregion Fields

    #region Contstoctors

    public MainPageModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    #endregion Contstoctors

    #region Commands

    [ICommand]
    void OpenRefferal()
    {
        _ = _navigationService.NavigateToPage<ReferralPage>();
    }

    [ICommand]
    Task OpenPlusRewards()
    {
        return Task.CompletedTask;
    }

    [ICommand]
    Task OpenPlusRewardsLogged()
    {
        return Task.CompletedTask;
    }

    #endregion Commands



}


