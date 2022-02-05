using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Attributes
{
    public abstract class HttpMethodAttribute : Attribute
    {
        public Method HttpMethod { get; }

        protected HttpMethodAttribute(Method httpMethod)
            => HttpMethod = httpMethod;
    }
}
