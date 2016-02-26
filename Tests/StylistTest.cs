using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }


    [Fact]
    public void Test_StylistsEmptyAtFirst()
    {
      int result = Stylist.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {

      Stylist firstStylist = new Stylist("Veronica Alley");
      Stylist secondStylist = new Stylist("Veronica Alley");


      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Test_GetStylists_RetrievesAllStylists()
    {
      Stylist firstStylist = new Stylist("Veronica Alley");
      firstStylist.Save();
      Stylist secondStylist = new Stylist("Brittnie Salazar");
      secondStylist.Save();

      List<Stylist> testStylistList = new List<Stylist> {firstStylist, secondStylist};
      List<Stylist> resultStylistList = Stylist.GetAll();

      Assert.Equal(testStylistList, resultStylistList);
    }

    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      Stylist testStylist = new Stylist("Veronica Alley");
      testStylist.Save();


      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};


      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Update_UpdatesStylistInDatabase()
    {
      string name = "Veronica Alley";
      Stylist testStylist = new Stylist(name);
      testStylist.Save();
      string newName = "Veronica Aubry Alley";

      testStylist.Update(newName);

      string result = testStylist.GetName();

      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {

      Stylist testStylist = new Stylist("Veronica Alley");
      testStylist.Save();


      Stylist foundStylist = Stylist.Find(testStylist.GetId());


      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Test_Delete_DeletesStylistFromDatabase()
    {

      string name1 = "Veronica Alley";
      Stylist testStylist1 = new Stylist(name1);
      testStylist1.Save();

      string name2 = "Brittnie Salazar";
      Stylist testStylist2 = new Stylist(name2);
      testStylist2.Save();

      // Client testClient1 = new Client("Alison Vu", testStylist1.GetId());
      // testClient1.Save();
      // Client testClient2 = new Client("Alexis Faris", testStylist2.GetId());
      // testClient2.Save();

      testStylist1.Delete();
      List<Stylist> resultStylist = Stylist.GetAll();
      List<Stylist> testStylistList = new List<Stylist> {testStylist2};

      // List<Client> resultClients = Client.GetAll();
      // List<Client> testClientList = new List<Client> {testClient2};

      Assert.Equal(testStylistList, resultStylist);
      // Assert.Equal(testClientList, resultClients);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
