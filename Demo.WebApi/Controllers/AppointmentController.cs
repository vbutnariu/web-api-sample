using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pm.Common.Model.Appointments;
using Pm.Common.Model.Filter;
using Pm.Common.Model.Proband;
using Pm.Services.Core.Appointments;
using Pm.WebApi.Ergodat.Controllers;
using System;
using System.Collections.Generic;

namespace Pm.WebApi.Ergonomed.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AppointmentController : AppBaseController
	{
		private readonly IAppointmentService appointmentService;


		public ILogger logger { get; }

		public AppointmentController(ILogger<AuthController> logger, IAppointmentService appointmentService)
		{
			this.logger = logger;
			this.appointmentService = appointmentService;
		}

		[HttpGet]
		[Route("Companies")]
		public List<BetriebModel> GetCompanies()
		{
			return TryExecute(() => { return appointmentService.GetCompanies(); });
		}

		[HttpGet]
		[Route("Categories")]
		public List<TerminkategorieModel> GetCategories()
		{
			return TryExecute(() => { return appointmentService.GetAppointmentCategories(); });
		}

		[HttpGet]
		[Route("Areas")]
		public List<TerminBereichModel> GetAreas()
		{
			return TryExecute(() => { return appointmentService.GetAppointmentAreas(); });
		}

		[HttpGet]
		[Route("Filters")]
		public List<FilterModel> GetFilters()
		{
			return TryExecute(() => { return appointmentService.GetAppointmentFilter(); });
		}

		[HttpGet]
		[Route("Patients")]
		public List<ProbandModel> GetPatients(string searchText, DateTime? asOfDate, [FromQuery] List<int> companyIds = null)
		{
			return TryExecute(() => { return appointmentService.SearchProband(searchText, asOfDate, companyIds); });
		}

		[HttpGet]
		[Route("Patient/Appointments")]
		public List<TerminProbandModel> GetPatientAppointments(DateTime startDate, DateTime endDate)
		{
			return TryExecute(() => { return appointmentService.GetAppointments(startDate, endDate); });
		}

		[HttpGet]
		[Route("Patient/Info")]
		public List<GrundsatzInfoModel> GetGrundsatzInfoByProbandId(int patientId)
		{
			return TryExecute(() => { return appointmentService.GetGrundsatzInfoByProbandId(patientId); });
		}

		[HttpPut]
		public void UpdateAppointment(TerminTaskModel appointment)
		{
			TryExecute(() => { appointmentService.UpdateTerminTask(appointment); });
		}

		[HttpPost]
		public TerminTaskModel InsertAppointment(TerminTaskModel appointment)
		{
			return TryExecute(() => { return appointmentService.AddTerminTask(appointment); });
		}


		[HttpDelete]
		public void DeleteAppointment(TerminTaskModel appointment)
		{
			TryExecute(() => { appointmentService.DeleteTerminTask(appointment.Id); });
		}
	}
}
		 