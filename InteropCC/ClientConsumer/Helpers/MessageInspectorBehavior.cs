using System;
using System.IO;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ClientConsumer.Helpers
{
    public class MessageInspectorBehavior : IClientMessageInspector, IEndpointBehavior
    {
        public event EventHandler OnMessageInspected;



        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (OnMessageInspected != null)
            {
                // Notify the subscribers of the inpected message.
                OnMessageInspected(this, new MessageInspectorArgs { Message = reply.ToString(), MessageInspectionType = eMessageInspectionType.Response });
                LogInput(reply.ToString());
            }
        }
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            if (OnMessageInspected != null)
            {
                // Notify the subscribers of the inpected message.
                OnMessageInspected(this, new MessageInspectorArgs { Message = request.ToString(), MessageInspectionType = eMessageInspectionType.Response });
                LogOutput(request.ToString(), request.Version.ToString());
            }
            return null;
        }
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            // Do nothing.
        }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // Add the message inspector to the as part of the service behaviour.
            clientRuntime.MessageInspectors.Add(this);
        }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // Do nothing.
        }
        public void Validate(ServiceEndpoint endpoint)
        {
            // Do nothing.
        }
        public void LogInput(string message)
        {
            using (StreamWriter writer = new StreamWriter("D:\\Soap\\test.log", true))
            {
                writer.WriteLine("");
                writer.WriteLine("-------------- Soap Response at " + DateTime.Now);
                writer.WriteLine(message);
            }
        }
        public void LogOutput(string message, string version)
        {
            using (StreamWriter writer = new StreamWriter("D:\\Soap\\test.log", true))
            {
                writer.WriteLine("");
                writer.WriteLine("-------------- Soap Request at " + DateTime.Now);
                writer.WriteLine();
                writer.WriteLine("--- Soap Version: " + version);
                writer.WriteLine();
                writer.WriteLine(message);
            }
        }
    }

    public enum eMessageInspectionType { Request = 0, Response = 1 };

    public class MessageInspectorArgs : EventArgs
    {

        ///<summary>

        /// Type of the message inpected.

        /// </summary>

        public eMessageInspectionType MessageInspectionType { get; internal set; }



        ///<summary>

        /// Inspected raw message.

        /// </summary>

        public string Message { get; internal set; }

    }
}