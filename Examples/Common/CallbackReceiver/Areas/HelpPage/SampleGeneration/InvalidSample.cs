using System;

namespace CallbackReceiver.Areas.HelpPage
{
    /// <summary>
    /// This represents an invalid sample on the help page. There's a display template named InvalidSample associated with this class.
    /// </summary>
    public class InvalidSample
    {
        /// <summary />
        public InvalidSample(string errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }
            ErrorMessage = errorMessage;
        }

        /// <summary />
        public string ErrorMessage { get; private set; }

        /// <summary />
        public override bool Equals(object obj)
        {
            var other = obj as InvalidSample;
            return other != null && ErrorMessage == other.ErrorMessage;
        }

        /// <summary />
        public override int GetHashCode()
        {
            return ErrorMessage.GetHashCode();
        }

        /// <summary />
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}