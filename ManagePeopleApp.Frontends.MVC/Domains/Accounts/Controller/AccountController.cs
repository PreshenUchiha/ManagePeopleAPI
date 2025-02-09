using Microsoft.AspNetCore.Mvc;

namespace ManagePeopleApp.Frontends.MVC.Domains.Accounts.Controller;

using ManagePeople.Libraries.Shared;
using ManagePeopleApp.Frontends.MVC.Domains.Accounts.Interface;
using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;
using ManagePeopleApp.Frontends.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
public class AccountController(
     IPersonClientService personClientService,
     IAccountClientService accountClientService,
    ILogger<AccountController> logger) : Controller
{
    [HttpGet("~/Teams/{id:int}/Members")]
    public async Task<IActionResult> GetAccountsByTeamId(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        HttpContext.Session.SetString(Constants.SessionKey, id.ToString());

        ViewBag.DisableButton = "";
        ViewBag.Redirect = false;

        var person = await personClientService.GetPersonByPersonIdAsync(id);

        if (person is null)
        {
            return NotFound();
        }

        return View(new AccountModel { PersonId = id });
    }

    public async Task<IActionResult> GetTeamMemberByTeamMemberId([FromBody] ExtendedTeamMemberModel model)
    {
        var teamMemberDetails = await teamMemberClientService.GetTeamMemberByTeamMemberIdAsync(model.Id, model.TeamId);

        return teamMemberDetails is null
            ? NotFound()
            : Ok(teamMemberDetails);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeamMember(ExtendedTeamMemberModel model)
    {
        TempData.Remove("SuccessMessage");
        TempData.Remove("ErrorMessage");

        if (string.IsNullOrEmpty(HttpContext.Session.GetString(Constants.SessionKey)))
        {
            logger.LogWarning(
               "{Announcement}: The team entry was not found",
               "NULL");

            return NotFound();
        }

        model.TeamId = Convert.ToInt32(HttpContext.Session.GetString(Constants.SessionKey));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdMember = await teamMemberClientService.CreateTeamMemberAsync(model);

        DisplayMessage(createdMember, "create");

        logger.LogInformation(
            "Controller =>  {Announcement}: Attempt to create team member {TeamMember} from team {Team} was {Message}",
            "SUCCESS",
             model.Id,
             model.TeamId,
             createdMember ? "successful" : "unsuccessful");

        return RedirectToAction(nameof(GetTeamMembersByTeamId), new { id = model.TeamId });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTeamMember(ExtendedTeamMemberModel model)
    {
        TempData.Remove("SuccessMessage");
        TempData.Remove("ErrorMessage");

        if (!ModelState.IsValid)
        {
            return View(nameof(GetTeamMembersByTeamId), new { id = model.TeamId });
        }

        var updatedMember = await teamMemberClientService.UpdateTeamMemberAsync(model);

        DisplayMessage(updatedMember, "update");

        logger.LogInformation(
            "Controller =>  {Announcement}: Attempt to update team member {TeamMember} from team {Team} was {Message}",
            "SUCCESS",
            model.Id,
            model.TeamId,
            updatedMember ? "successful" : "unsuccessful");

        return RedirectToAction(nameof(GetTeamMembersByTeamId), new { id = model.TeamId });
    }

    public async Task<IActionResult> DeleteTeamMember([FromBody] ExtendedTeamMemberModel model)
    {
        var deleteTeamMember = await teamMemberClientService.DeleteTeamMemberAsync(model.Id, model.TeamId);

        if (!deleteTeamMember)
        {
            logger.LogWarning(
              "{Announcement}: Attempt to delete team membeR",
              "FAILED");
            return BadRequest();
        }

        logger.LogInformation(
            "Controller => {Announcement}: Attempt to delete team member {TeamMember} from team {Team} was successful",
            "SUCCESS",
            model.Id,
            model.TeamId);

        return Ok(new { message = deleteTeamMember });
    }

    private void DisplayMessage(bool value, string action)
    {
        if (value)
        {
            TempData["SuccessMessage"] = $"Attempt to {action} team member was successful";
        }
        else
        {
            TempData["ErrorMessage"] = $"Attempt to {action} team member was unsuccessful.";
        }
    }
}
