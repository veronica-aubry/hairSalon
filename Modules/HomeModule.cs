using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Get["/allClients"] = _ => {
        List<Client> AllClients = Client.GetAll();
        return View["all_clients.cshtml", AllClients];
      };

      Post["/addStylist"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Get["/stylist/{id}/edit"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["edit_stylist.cshtml", SelectedStylist];
      };

      Patch["/stylist/{id}/edit"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Update(Request.Form["stylist-name"]);
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists ];
      };

      Get["/stylist/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["clients.cshtml", SelectedStylist];
      };

      Delete["/stylist/{id}/delete"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Delete();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Post["/stylist/{id}/addClient"] = parameters => {
        Client newClient = new Client(Request.Form["client-name"], parameters.id);
        newClient.Save();
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["clients.cshtml", SelectedStylist];
      };

      Get["/client/{id}/edit"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        return View["edit_client.cshtml", SelectedClient];
      };

      Delete["/client/{id}/delete"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
        SelectedClient.Delete();
        return View["clients.cshtml", SelectedStylist];
      };

      Patch["/client/{id}/edit"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        SelectedClient.Update(Request.Form["client-name"]);
        Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
        return View["clients.cshtml", SelectedStylist];
      };


    }
  }
}
