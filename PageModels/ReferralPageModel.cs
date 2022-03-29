using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Gameball.Services;

namespace Gameball.PageModels
{
	public partial class ReferralPageModel : BasePageModel
	{
		#region Fields

		private readonly IAlertService _alertService;

		#endregion Fields

		#region Contstoctors

		public ReferralPageModel(
			IAlertService alertService)
		{
			_alertService = alertService;
			_ = _alertService.DisplayAllert("Referral Page", null);
		}

		#endregion Contstoctors

		#region Properties
		#endregion Properties

		#region Commands
		#endregion Commands

		#region Public Methods
		#endregion Public Methods

		#region Protected Methods
		#endregion Protected Methods

		#region Private Methods
		#endregion Private Methods
	}
}

