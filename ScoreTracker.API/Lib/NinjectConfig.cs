using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using ScoreTracker.API.Controllers;
using ScoreTracker.API.Model;
using ScoreTracker.API.Model.Repo;

namespace ScoreTracker.API.Lib
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Bind(typeof(IRepository<>)).To(typeof(DbRepository<>));
                kernel.Bind<IAuthRepository>().To<AuthRepository>();
                
                kernel.Bind<ICompetitionTableGenerator>().To<CompetitionTableGenerator>();
                //kernel.Bind<IRepository<Match>>().To<DbRepository<Match>>();
                return kernel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}