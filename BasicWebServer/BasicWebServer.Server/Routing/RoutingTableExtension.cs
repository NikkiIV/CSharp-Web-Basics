using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using System;
using System.Reflection;

namespace BasicWebServer.Server.Routing
{
    public static class RoutingTableExtension
    {
        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, Response> controllerFunction) 
            where TController : Controller
            => routingTable.Map(
                Method.Get,
                path,
                request => controllerFunction(CreateController<TController>(request)));

        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, Response> controllerFunction) where TController : Controller
            => routingTable.Map(
                Method.Post,
                path,
                request => controllerFunction(CreateController<TController>(request)));

        public static IRoutingTable MapControllers(this IRoutingTable routingTable)
        {
            IEnumerable<MethodInfo> controllerActions = GetControllerActions();

            foreach (var controllerAction in controllerActions)
            {
                string controllerName = controllerAction
                    .DeclaringType
                    .Name
                    .Replace(nameof(Controller), string.Empty);

                string actionName = controllerAction.Name;
                string path = $"/{controllerName}/{actionName}";

                var responseFunction = GetResponseFunction(controllerAction);

                Method httpMethod = Method.Get;
                var actionMethodAttribute = controllerAction
                    .GetCustomAttribute<HttpMethodAttribute>();

                if (actionMethodAttribute != null)
                {
                    httpMethod = actionMethodAttribute.HttpMethod;
                }

                routingTable.Map(httpMethod, path, responseFunction);
            }

            return routingTable;
        }
        private static TController CreateController<TController>(Request request)
            => (TController)Activator.CreateInstance(typeof(TController), new[] { request });

        private static Controller CreateController(Type controllerType, Request request)
        {
            var controller = (Controller)Request.ServiceCollection.CreateInstance(controllerType);

            controllerType
                .GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(controller, request);

            return controller;
        }
    }
}
