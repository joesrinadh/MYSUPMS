using System.ServiceModel;

namespace SUPMS.Infrastructure.Utilities
{
    /// <summary>
    /// Communication Protocol
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// Net TCP
        /// </summary>
        NetTcp,
        /// <summary>
        /// Local in process service
        /// </summary>
        InProcess,
        /// <summary>
        /// Basic HTTP
        /// </summary>
        BasicHttp,
        /// <summary>
        /// WS HTTP
        /// </summary>
        WsHttp,
        /// <summary>
        /// XML Formatted REST over HTTP
        /// </summary>
        RestHttpXml,
        /// <summary>
        /// JSON Formatted REST over HTTP
        /// </summary>
        RestHttpJson
    }

    /// <summary>
    /// Message size
    /// </summary>
    public enum MessageSize
    {
        /// <summary>
        /// Normal (default message size as defined by WCF)
        /// </summary>
        Normal,
        /// <summary>
        /// Large (up to 100MB)
        /// </summary>
        Large,
        /// <summary>
        /// Medium (up to 10MB) - this is the default
        /// </summary>
        Medium,
        /// <summary>
        /// For internal use only
        /// </summary>
        Undefined
    }

    /// <summary>
    /// Generic helper methods used internally by various service components
    /// </summary>
    internal static class ServiceHelper
    {
        /// <summary>
        /// Configures NetTcpBinding message size
        /// </summary>
        /// <param name="messageSize">Message size to configure the binding for</param>
        /// <param name="binding">NetTcpBinding to configure</param>
        public static void ConfigureMessageSizeOnNetTcpBinding(MessageSize messageSize, NetTcpBinding binding)
        {
            if (messageSize == MessageSize.Medium)
            {
                binding.MaxBufferSize = 1024 * 1024 * 10; // 10MB
                binding.MaxBufferPoolSize = binding.MaxBufferSize;
                binding.MaxReceivedMessageSize = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxArrayLength = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxStringContentLength = binding.MaxBufferSize;
            }
            else if (messageSize == MessageSize.Large)
            {
                binding.MaxBufferSize = 1024 * 1024 * 100; // 100MB
                binding.MaxBufferPoolSize = binding.MaxBufferSize;
                binding.MaxReceivedMessageSize = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxArrayLength = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxStringContentLength = binding.MaxBufferSize;
            }
        }

        /// <summary>
        /// Configures Web HTTP Binding (REST) message size
        /// </summary>
        /// <param name="messageSize">Message size to configure the binding for</param>
        /// <param name="binding">BasicHttpBinding to configure</param>
        public static void ConfigureMessageSizeOnWebHttpBinding(MessageSize messageSize, WebHttpBinding binding)
        {
            if (messageSize == MessageSize.Medium)
            {
                binding.MaxBufferSize = 1024 * 1024 * 10; // 10MB
                binding.MaxBufferPoolSize = binding.MaxBufferSize;
                binding.MaxReceivedMessageSize = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxArrayLength = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxStringContentLength = binding.MaxBufferSize;
            }
            else if (messageSize == MessageSize.Large)
            {
                binding.MaxBufferSize = 1024 * 1024 * 100; // 100MB
                binding.MaxBufferPoolSize = binding.MaxBufferSize;
                binding.MaxReceivedMessageSize = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxArrayLength = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxStringContentLength = binding.MaxBufferSize;
            }
        }

        /// <summary>
        /// Configures Basic HTTP Binding message size
        /// </summary>
        /// <param name="messageSize">Message size to configure the binding for</param>
        /// <param name="binding">BasicHttpBinding to configure</param>
        public static void ConfigureMessageSizeOnBasicHttpBinding(MessageSize messageSize, BasicHttpBinding binding)
        {
            if (messageSize == MessageSize.Medium)
            {
                binding.MaxBufferSize = 1024 * 1024 * 10; // 10MB
                binding.MaxBufferPoolSize = binding.MaxBufferSize;
                binding.MaxReceivedMessageSize = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxArrayLength = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxStringContentLength = binding.MaxBufferSize;
            }
            else if (messageSize == MessageSize.Large)
            {
                binding.MaxBufferSize = 1024 * 1024 * 100; // 100MB
                binding.MaxBufferPoolSize = binding.MaxBufferSize;
                binding.MaxReceivedMessageSize = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxArrayLength = binding.MaxBufferSize;
                binding.ReaderQuotas.MaxStringContentLength = binding.MaxBufferSize;
            }
        }

        /// <summary>
        /// Configures WS HTTP Binding message size
        /// </summary>
        /// <param name="messageSize">Message size to configure the binding for</param>
        /// <param name="binding">BasicHttpBinding to configure</param>
        public static void ConfigureMessageSizeOnWsHttpBinding(MessageSize messageSize, WSHttpBinding binding)
        {
            if (messageSize == MessageSize.Medium)
            {
                binding.MaxBufferPoolSize = 1024 * 1024 * 10; // 10MB
                binding.MaxReceivedMessageSize = binding.MaxBufferPoolSize;
                binding.ReaderQuotas.MaxArrayLength = (int)binding.MaxBufferPoolSize;
                binding.ReaderQuotas.MaxStringContentLength = (int)binding.MaxBufferPoolSize;
            }
            else if (messageSize == MessageSize.Large)
            {
                binding.MaxBufferPoolSize = 1024 * 1024 * 100; // 100MB
                binding.MaxReceivedMessageSize = binding.MaxBufferPoolSize;
                binding.ReaderQuotas.MaxArrayLength = (int)binding.MaxBufferPoolSize;
                binding.ReaderQuotas.MaxStringContentLength = (int)binding.MaxBufferPoolSize;
            }
        }
    }
}
