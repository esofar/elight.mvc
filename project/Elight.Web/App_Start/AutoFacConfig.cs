using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Compilation;

namespace Elight.Web
{
    public class AutoFacConfig
    {
        /// <summary>
        /// IOC组件容器。
        /// </summary>
        public static IContainer Container { get; set; }

        /// <summary>
        /// 获取组件。
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            try
            {
                if (Container != null)
                {
                    return Container.Resolve<T>();
                }
                return default(T);              
            }
            catch (Exception ex)
            {
                throw new Exception("IOC实例化出错。" + ex.Message);
            }
        }

        /// <summary>
        /// 注册服务。
        /// </summary>
        public static void RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            Assembly serviceAss = Assembly.Load("Elight.Service");
            Type[] serviceTypes = serviceAss.GetTypes();
            builder.RegisterTypes(serviceTypes).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            
            Assembly repositoryAss = Assembly.Load("Elight.Repository");
            Type[] repositoryTypes = repositoryAss.GetTypes();
            builder.RegisterTypes(repositoryTypes).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}