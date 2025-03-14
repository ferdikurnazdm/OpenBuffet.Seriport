using System;
using System.Collections.Generic;
using System.IO.Ports;
using OpenBuffet.Seriport.Core.Configurations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBuffet.Seriport.Core.Interfaces {
    /// <summary>
    /// this interface represent the seriport service
    /// </summary>
    public interface ISeriportService {
        /// <summary>
        /// this event trigger when occured serial error
        /// </summary>
        event EventHandler<SerialError> ErrorReceivedEvent;
        /// <summary>
        /// this event trigger when occured data received
        /// </summary>
        event EventHandler DataReceivedEvent;
        /// <summary>
        /// this property return
        /// Seriport configuration
        /// </summary>
        SeriportConfiguration GetSeriportConfiguration { get; }
        /// <summary>
        /// this method builds the seriport
        /// </summary>
        /// <param name="seriportConfigurator">seriport configuration</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        bool TryBuildSeriport(Action<SeriportConfiguration> seriportConfigurator, out Exception exception);
        /// <summary>
        /// this method get the seriport connection state
        /// </summary>
        /// <param name="state">seriport connection state</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        bool TryGetSeriPortState(out bool state, out Exception exception);
        /// <summary>
        /// this method close the seriport connection
        /// </summary>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        bool TryCloseSeriport(out Exception exception);
        /// <summary>
        /// this method open the seriport connection
        /// </summary>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return falses</returns>
        bool TryOpenSeriport(out Exception exception);
        /// <summary>
        /// this method read the seri port
        /// </summary>
        /// <param name="readedData">readed data on seriport</param>
        /// <param name="readedBytes">readed bytes count on seriport</param>
        /// <param name="timeTaken">readed data time duration on seriport</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        bool TryReadSeriport(out string readedData, out int readedBytes, out TimeSpan timeTaken, out Exception exception);
        /// <summary>
        /// this method write to seriport
        /// </summary>
        /// <param name="content">write content</param>
        /// <param name="sendedBytes">sended bytes count on seriport</param>
        /// <param name="timeTaken">sended data time duration on seriport</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        bool TryWriteSeriport(string content, out int sendedBytes, out TimeSpan timeTaken, out Exception exception);
    }
}
