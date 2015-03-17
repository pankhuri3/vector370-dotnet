using System;

namespace CallbackReceiver.Areas.HelpPage
{
    /// <summary>
    /// This represents a preformatted text sample on the help page. There's a display template named TextSample associated with this class.
    /// </summary>
    public class TextSample
    {
        /// <summary />
        public TextSample(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            Text = text;
        }

        /// <summary />
        public string Text { get; private set; }

        /// <summary />
        public override bool Equals(object obj)
        {
            var other = obj as TextSample;
            return other != null && Text == other.Text;
        }

        /// <summary />
        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        /// <summary />
        public override string ToString()
        {
            return Text;
        }
    }
}