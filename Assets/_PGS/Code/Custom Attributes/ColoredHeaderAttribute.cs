using UnityEngine;

namespace PGS
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
    public class ColoredHeaderAttribute : HeaderAttribute
    {
        private const int DEFAULT_INSPECTOR_TEXT_SIZE = 12;

        public readonly string textColor;
        public readonly int textSize;
        public readonly bool useBold;

		/// <summary>
		/// Create a new HeaderAttribute in the Inspector
		/// </summary>
		/// <param name="header">The header text</param>
		/// <param name="textColor">The hex value for the header text color (excluding the #)</param>
		/// <param name="textSize">The font size for the header</param>
		/// <param name="useBold">Should the header font be bolded?</param>
		public ColoredHeaderAttribute(string header, string textColor, int textSize, bool useBold) : this(header, textColor, textSize)
        {
            this.useBold = useBold;
        }

		/// <summary>
		/// Create a new HeaderAttribute in the Inspector
		/// </summary>
		/// <param name="header">The header text</param>
		/// <param name="textColor">The hex value for the header text color (excluding the #)</param>
		/// <param name="textSize">The font size for the header</param>
		public ColoredHeaderAttribute(string header, string textColor, int textSize) : this(header, textColor)
        {
            this.textSize = textSize;
            this.useBold = false;
        }

		/// <summary>
		/// Create a new HeaderAttribute in the Inspector
		/// </summary>
		/// <param name="header">The header text</param>
		/// <param name="textColor">The hex value for the header text color (excluding the #)</param>
		/// <param name="useBold">Should the header font be bolded?</param>
		public ColoredHeaderAttribute(string header, string textColor, bool useBold = false) : this(header, useBold)
        {
			this.textColor = RemoveHex(textColor);
        }

		/// <summary>
		/// Create a new HeaderAttribute in the Inspector
		/// </summary>
		/// <param name="header">The header text</param>
		/// <param name="textSize">The font size for the header</param>
		/// <param name="useBold">Should the header font be bolded?</param>
		public ColoredHeaderAttribute(string header, int textSize, bool useBold = false) : this(header, useBold)
        {
            this.textSize = textSize;
        }

		/// <summary>
		/// Create a new HeaderAttribute in the Inspector
		/// </summary>
		/// <param name="header">The header text</param>
		/// <param name="useBold">Should the header font be bolded?</param>
		public ColoredHeaderAttribute(string header, bool useBold = false) : base(header)
        {
            this.textColor = string.Empty;
            this.textSize = DEFAULT_INSPECTOR_TEXT_SIZE;
            this.useBold = useBold;
        }

		private string RemoveHex(string colorString)
		{
			if (colorString.StartsWith("#")) //If the user added a # before the color string, remove it (we only want the 6 digits)
			{
				return colorString.Substring(1);
			}

			return colorString;
		}
    }
}
