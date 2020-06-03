using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.IO;
using System.Net;
using System.Timers;
using System.Web.Script.Serialization;
using Topshelf;

namespace PredictCapacityUsingMathNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creat WindowService 
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.SetServiceName("MyForecastingCapacity");
                hostConfigurator.SetDisplayName("MyForecastingCapacity");
                hostConfigurator.SetDescription("Using MathNet to predict capacity");

                hostConfigurator.RunAsLocalSystem();

                hostConfigurator.Service<PracticeService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(() => new PracticeService());

                    serviceConfigurator.WhenStarted(service => service.OnStart());
                    serviceConfigurator.WhenStopped(service => service.OnStop());
                });
            });

        }
       
    }
}
