using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Gameball.PageModels
{
	public abstract partial class BasePageModel : ObservableObject
	{
        public virtual Task OnNavigatingTo()
           => Task.CompletedTask;

        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;

        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;
    }

    public abstract partial class BasePageModel<TParam> : BasePageModel
    {
        public abstract Task OnNavigatingTo(TParam param);
    }
}
