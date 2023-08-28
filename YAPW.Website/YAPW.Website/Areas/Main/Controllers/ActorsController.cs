using Ditech.Portal.NET.App_Base;
using Ditech.Portal.NET.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YAPW.Models;
using YAPW.Website.Controllers;

namespace YAPW.Website.Areas.Main.Controllers;

[Route("[area]/[controller]/[action]")]
[Area("Main")]
public class ActorsController : BaseController
{
    public AppBase appBase = new AppBase();
    private readonly IHttpClientFactory _clientFactory;

    public ActorsController(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [HttpGet]
    [AjaxOnly]
    public async Task<IActionResult> Index(string ActorNumber)
    {
        try
        {
            var actors = await ExecuteServiceRequest<List<ActorDataModel>>(HttpMethod.Get, $"actors");

            return PartialView(actors);
        }
        catch (Exception ex)
        {
            return BadRequest("Error occured" + ex.Message);
        }
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //[AjaxOnly]
    //public async Task<IActionResult> Actor(ActorGetModel ActorGetModel)
    //{
    //    try
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }
    //        var ActorRequestResult = await appBase.GetElementFromApiAsync<ActorViewModel>($"operations/Actors/erp/compact/{ActorGetModel.ErpNumber}?typeId={ActorGetModel.ActorType}", _clientFactory);
    //        if (ActorRequestResult.statusCode != HttpStatusCode.OK)
    //        {
    //            return BadRequest(ActorRequestResult.rawResponse);
    //        }
    //        var test = ActorRequestResult.returnedElement.Tanks.ActorBy(t => t.CurrentStatus);
    //        var customer = await ExecuteServiceRequest<Customer>(HttpMethod.Get, "logistics/customer/" + ActorRequestResult.returnedElement.CustomerId);
    //        ViewBag.CustomerAddress = customer.Addresses?.FirstOrDefault().Address.FullAddress;
    //        var TankStatusList = new List<TankStatuses>();
    //        if (ActorRequestResult.returnedElement.Tanks.Count() > 0)
    //        {
    //            TankStatusList.Add(new TankStatuses { TankCount = 0, status = ActorRequestResult.returnedElement.Tanks.FirstOrDefault().CurrentStatus });
    //            foreach (var tank in test)
    //            {
    //                var tankCurentStatus = tank.CurrentStatus;
    //                var exist = false;

    //                foreach (var statusList in TankStatusList)
    //                {
    //                    if (statusList.status == tankCurentStatus)
    //                    {
    //                        exist = true;
    //                        statusList.TankCount++;
    //                    }
    //                }
    //                if (!exist)
    //                {
    //                    TankStatusList.Add(new TankStatuses { status = tankCurentStatus, TankCount = 1 });
    //                }
    //            }
    //        }
    //        ViewBag.Tanks = ActorRequestResult.returnedElement.Tanks.Count;
    //        var Actor = ActorRequestResult.returnedElement;
    //        Actor.TanksStatus = TankStatusList;

    //        return View("Actor", Actor);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest("Error occured" + ex.Message);
    //    }
    //}
}
