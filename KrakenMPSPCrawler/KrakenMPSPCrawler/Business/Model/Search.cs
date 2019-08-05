using System;

using KrakenMPSPCrawler.Business.Enum;

namespace KrakenMPSPCrawler.Business.Model
{
    public abstract class Search
    {
        public virtual SearchError Error { get; protected set; }

        public virtual void SetErrorMessage(string errorMessage)
        {
            if (Error != null)
            {
                Error.Message = errorMessage;
                return;
            }

            Error = new SearchError(errorMessage);
        }

        public virtual void SetErrorMessage(Type type, string errorMessage)
        {
            Error = new SearchError(type, errorMessage);
        }

        public abstract SearchStatus Completed();
    }
}