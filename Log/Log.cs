using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Xml;

namespace Log
{
    
    public class Log
    {

        private static Log log = new Log();

        private List<TextWriter> outputStreams;
        
        private bool enabled = true;
        
        private Log()
        {
             outputStreams = new List<TextWriter>();
        }

        public static void enableLog()
        {
            log.enabled = true;
        }

        public static void disableLog()
        {
            log.enabled = false;
        }

        public void addOutputStream(TextWriter outStream)
        {
            outputStreams.Add(outStream);
        }

        public void logMessage(String message, params object[] args)
        {
            if (enabled)
            {
                foreach (var writer in outputStreams)
                {
                    writer.Write(message + Environment.NewLine, args);
                }
            }
        }

        public void logLine(String message, params object[] args)
        {
            if (enabled)
            {
                foreach (var writer in outputStreams)
                {
                    writer.Write(message + Environment.NewLine, args);
                }
            }
        }

        public static Log getLog()
        {
            return log;
        }
    }
}