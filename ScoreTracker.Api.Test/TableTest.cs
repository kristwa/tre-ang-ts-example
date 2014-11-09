using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using ScoreTracker.API.Controllers;
using ScoreTracker.API.Lib;
using ScoreTracker.API.Model;

namespace ScoreTracker.Api.Test
{
    [TestFixture]
    public class TableTest
    {
        private TablesController controller;

        [SetUp]
        public void Setup()
        {
            var mockRepo = new MockRepository<Group>();
            controller = new TablesController(mockRepo, new CompetitionTableGenerator());
        }

        [Test(Description = "4 teams on table")]
        public void TeamsOnTheTableShouldBe4()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            var response = controller.GetTable(1);
            var contentResult = response as OkNegotiatedContentResult<List<TableItem>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(contentResult.Content.Count() == 4);
        }

        [Test]
        public void Group1WinnerShouldBeUruguay()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetTable(1);
            var contentResult = response as OkNegotiatedContentResult<List<TableItem>>;

            var winner = contentResult.Content.FirstOrDefault().Team.TeamName;

            Assert.AreEqual(winner, "Uruguay");
        }
    }
}
